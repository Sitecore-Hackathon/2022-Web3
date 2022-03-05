using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sitecore.DevEx.Client.Logging;
using Sitecore.DevEx.Client.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Web3.Operator.Cli.Models;
using Web3.Operator.Cli.Services;

namespace Web3.Operator.Cli.Clients
{
    public interface IOperatorClient
    {
        Task<ICollection<InstanceDetails>> List();
        Task<StartedResult> StartNewInstance(string instanceName, string sitecoreAdminPassword);
        Task StopInstance(string instanceName);
        IAsyncEnumerable<string> Logs(string instanceName, CancellationToken token);
    }

    internal class OperatorClient : IOperatorClient
    {
        public OperatorClient(IUserConfigService userConfigService, ILogger<OperatorClient> logger)
        {
            _baseUrl = userConfigService.GetOperatorBaseUrl();
            if (string.IsNullOrWhiteSpace(_baseUrl))
            {
                throw new TaskValidationException("You should learn to call init first. RTFM.");
            }

            _logger = logger;
        }

        public readonly string _baseUrl;
        private readonly ILogger<OperatorClient> _logger;

        private HttpClient Init()
        {
            var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            _logger.LogConsoleInformation($"using {_baseUrl} as operator!");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            return client;
        }

        public async Task<StartedResult> StartNewInstance(string instanceName, string sitecoreAdminPassword)
        {
            var uri = "/start" +
                $"?instanceName={HttpUtility.UrlEncode(instanceName)}" +
                $"&sitecoreAdminPassword={HttpUtility.UrlEncode(sitecoreAdminPassword)}" +
                $"&user={HttpUtility.UrlEncode(Environment.UserName)}";
            using var client = Init();
            using var response = await client.PostAsync(uri, new StringContent(string.Empty));
            if(!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Could not create: {response.StatusCode}");
                return null;
            }

            var str = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<StartedResult>(str);
            return result;
        }

        public async Task StopInstance(string instanceName)
        {
            var uri = $"/stop/?instanceName={HttpUtility.UrlEncode(instanceName)}";
            using var client = Init();
            using var response = await client.PostAsync(uri, new StringContent(string.Empty));
        }

        public async Task<ICollection<InstanceDetails>> List()
        {
            var uri = "/list";
            using var client = Init();
            using var response = await client.GetAsync(uri);
            var str = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ICollection<InstanceDetails>>(str);
            return result;
        }

        public IAsyncEnumerable<string> Logs(string instanceName, CancellationToken token)
        {
            var uri = $"/logs/{HttpUtility.UrlPathEncode(instanceName)}";

            async IAsyncEnumerable<string> Read()
            {
                using var client = Init();
                using var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, token);
                using var body = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(body);
                while (!reader.EndOfStream && !token.IsCancellationRequested)
                {
                    yield return reader.ReadLine();
                }
            }

            return Read();
        }

    }
}
