using Core;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.IO.Pipelines;
using Web3.Operator.Engines.Core;

namespace Web3.Operator.Engines.DockerEngine
{
    public class DockerEngineOperator : IOperatorEngine
    {
        private readonly OperatorConfiguration _configuration;
        private readonly DockerClient _client;

        public DockerEngineOperator(OperatorConfiguration configuration, DockerEngineConfiguration engineConfig)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if(engineConfig == null) throw new ArgumentNullException(nameof(engineConfig));
            if(engineConfig.Url == null) throw new ArgumentException("Engine url required", nameof(engineConfig));
            
            var clientConfig = new DockerClientConfiguration(new Uri(engineConfig.Url));
            _client = clientConfig.CreateClient();
        }

        public async Task<ICollection<InstanceDetails>> List()
        {
            var response = await _client.Containers.ListContainersAsync(new ContainersListParameters
            {
                Filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    {
                        "label",
                        new Dictionary<string, bool>
                        {
                            {  _configuration.OperatorNameLabel, true }
                        }
                    }
                }
            });

            var result = response.Select(c =>
            {
                string? instanceName = c.Labels.TryGetValue(_configuration.OperatorNameLabel, out var value) ? value : string.Empty;

                var name = c.Names.First().TrimStart('/');
                var rule = c.Labels.TryGetValue($"traefik.http.routers.{name}.rule", out value) ? value : string.Empty;
                var hostName = rule?.Length > 8 ? rule[6..^2] : string.Empty;
                var scheme = _configuration.HostNameTls ? "https://" : "http://";
                var url = scheme + hostName;
                return new InstanceDetails(
                    instanceName,
                    hostName,
                    url,
                    c.Image,
                    c.State,
                    c.Status
                    );
            }).ToList();
            return result;
        }

        public async Task<StartedResult> StartInstance(InstanceOptions options)
        {
            var instanceName = options.InstanceName ?? throw new ArgumentException("Instance name must be specified");
            var cleanName = System.Text.RegularExpressions.Regex.Replace(instanceName.ToLower(), "[^a-z0-9\\-_]", "");
            var name = _configuration.InstanceNamePattern.Replace("{0}", cleanName); ;
            var hostName = _configuration.HostNamePattern.Replace("{0}", cleanName);
            var scheme = _configuration.HostNameTls ? "https://" : "http://";
            var url = scheme + hostName;

            await EnsureImage();

            var existingId = await GetInstanceId(options);
            if(existingId == null || existingId.Any())
            {
                return new StartedResult(url, "AlreadyRunning");
            }

            var result = await _client.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = _configuration.InstanceImage,
                Name = name,
                Labels = new Dictionary<string, string>
                {
                    { _configuration.OperatorNameLabel, options.InstanceName },
                    { _configuration.OperatorUserLabel, options.User ?? string.Empty },
                    { "traefik.enable", "true" },
                    { $"traefik.http.routers.{name}.entrypoints", _configuration.TraefikEntrypoint },
                    { $"traefik.http.routers.{name}.tls", $"{_configuration.HostNameTls.ToString().ToLower()}" },
                    { $"traefik.http.routers.{name}.rule", $"Host(`{hostName}`)" },

                },
                Env = new List<string>
                {
                    $"SITECORE_ADMIN_PASSWORD={options.SitecoreAdminPassword}",
                },
                HostConfig = new HostConfig
                {
                    Memory = Convert.ToInt64(options.MemoryMB) * 1024 * 1024,
                    CPUCount = options.CPUCount,
                },
                NetworkingConfig = new NetworkingConfig
                {
                    EndpointsConfig = new Dictionary<string, EndpointSettings>
                    {
                        { _configuration.DockerNetworkName, new EndpointSettings() }
                    }
                }
            });
            var id = result.ID;
            await _client.Containers.StartContainerAsync(id, new ContainerStartParameters());

            return new StartedResult(url, "Created");
        }

        public async Task StopInstance(InstanceOptions options)
        {
            var ids = await this.GetInstanceId(options);

            var tasks = ids.Select(async id =>
            {
                await _client.Containers.StopContainerAsync(id, new ContainerStopParameters
                {
                    WaitBeforeKillSeconds = 60
                });
                await _client.Containers.RemoveContainerAsync(id, new ContainerRemoveParameters
                {
                    Force = true
                });
            });
            await Task.WhenAll(tasks);
        }

        public async Task<IAsyncEnumerable<byte>> InstanceLogs(InstanceOptions options, PipeWriter outputStream, CancellationToken token)
        {
            var id = (await GetInstanceId(options))?.FirstOrDefault();

            var stream = string.IsNullOrEmpty(id) ? null : await _client.Containers.GetContainerLogsAsync(id, true, new ContainerLogsParameters
            {
                Follow = true,
                ShowStderr = true,
                ShowStdout = true
            }, token);

            async IAsyncEnumerable<byte> ReadLogs()
            {
                if (stream == null)
                {
                    yield break;
                }

                var msg = $"Here comes logs for instance {options.InstanceName} id {id}\n\n";
                var bytes = System.Text.Encoding.UTF8.GetBytes(msg);
                await outputStream.WriteAsync(new ReadOnlyMemory<byte>(bytes, 0, bytes.Length), token);


                var buffer = new byte[4096];
                while (!token.IsCancellationRequested)
                {
                    var result = await stream.ReadOutputAsync(buffer, 0, buffer.Length, token);
                    if (result.EOF) { yield break; }
                    await outputStream.WriteAsync(new ReadOnlyMemory<byte>(buffer, 0, result.Count), token);
                    await outputStream.FlushAsync(token);
                }
            }

            return ReadLogs();
        }

        private async Task<ICollection<string>> GetInstanceId(InstanceOptions options)
        {
            var response = await _client.Containers.ListContainersAsync(new Docker.DotNet.Models.ContainersListParameters
            {
                Filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    {
                        "label",
                        new Dictionary<string, bool>
                        {
                            {   $"{_configuration.OperatorNameLabel}={options.InstanceName}", true }
                        }
                    }
                }
            });

            return response.Select(container => container.ID).ToList();
        }

        private async Task EnsureImage()
        {
            var image = _configuration.InstanceImage.Split(':');
            await _client.Images.CreateImageAsync(new ImagesCreateParameters
            {
                FromImage = image[0],
                Tag = image.Skip(1).FirstOrDefault() ?? "latest",
            }, null, new Progress<JSONMessage>());
        }
    }
}