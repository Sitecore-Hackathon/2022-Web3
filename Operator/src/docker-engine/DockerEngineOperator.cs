using Core;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.IO.Pipelines;
using Web3.Operator.Engines.Core;

namespace Web3.Operator.Engines.DockerEngine
{
    public class DockerEngineOperator : IOperatorEngine
    {
        private const string OperatorNameLabel = "sitecoreoperator.name";
        private const string OperatorUserLabel = "sitecoreoperator.user";

        private readonly OperatorConfiguration _configuration;
        private readonly DockerClient _client;

        public DockerEngineOperator(OperatorConfiguration configuration, DockerClientConfiguration clientConfiguration)
        {
            _configuration = configuration;
            _client = clientConfiguration.CreateClient();
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
                            {   OperatorNameLabel, true }
                        }
                    }
                }
            });

            var result = response.Select(c =>
            {
                string? instanceName = c.Labels.TryGetValue(OperatorNameLabel, out var value) ? value : string.Empty;

                var name = c.Names.First().TrimStart('/');
                var rule = c.Labels.TryGetValue($"traefik.http.routers.{name}.rule", out value) ? value : string.Empty;
                var hostName = rule?.Length > 8 ? rule[6..^2] : string.Empty;
                return new InstanceDetails(
                    instanceName,
                    hostName,
                    c.Image,
                    c.State,
                    c.Status
                    );
            }).ToList();
            return result;
        }

        public async Task<string> StartInstance(InstanceOptions options)
        {
            var instanceName = options.InstanceName ?? throw new ArgumentException("Instance name must be specified");
            var cleanName = System.Text.RegularExpressions.Regex.Replace(instanceName.ToLower(), "[^a-z0-9\\-_]", "");
            var name = _configuration.InstanceNamePattern.Replace("{0}", cleanName); ;
            var hostName = _configuration.HostNamePattern.Replace("{0}", cleanName);

            await EnsureImage();
            var result = await _client.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = _configuration.InstanceImage,
                Name = name,
                Labels = new Dictionary<string, string>
                {
                    { OperatorNameLabel, options.InstanceName },
                    { OperatorUserLabel, options.User ?? string.Empty },
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
                }
            });
            var id = result.ID;
            await _client.Containers.StartContainerAsync(id, new ContainerStartParameters());

            var scheme = _configuration.HostNameTls ? "https://" : "http://";
            return scheme + hostName;
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
                    await outputStream.FlushAsync();
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
                            {   $"{OperatorNameLabel}={options.InstanceName}", true }
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