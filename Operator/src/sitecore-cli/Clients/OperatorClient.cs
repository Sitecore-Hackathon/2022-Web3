﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Web3.Operator.Cli.Clients
{
    public interface IOperatorClient
    {
        Task<string> StartNewInstance(string instanceName, string sitecoreAdminPassword);
        Task StopInstance(string instanceName);

        IAsyncEnumerable<string> Logs(string instanceName, CancellationToken token);
    }

    internal class OperatorClient : IOperatorClient
    {
        public const string BaseUrl = "http://operator-127-0-0-1.nip.io/";

        private HttpClient Init()
        {
            var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
            client.DefaultRequestHeaders.UserAgent.TryParseAdd($"Speedo: https://github.com/Sitecore-Hackathon/2021-Anonymous-Sitecoreholics");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            return client;
        }

        public async Task<string> StartNewInstance(string instanceName, string sitecoreAdminPassword)
        {
            var uri = "/start" +
                $"?instanceName={HttpUtility.UrlEncode(instanceName)}" +
                $"&sitecoreAdminPassword={HttpUtility.UrlEncode(sitecoreAdminPassword)}" +
                $"&user={HttpUtility.UrlEncode(Environment.UserName)}";
            using var client = Init();
            using var response = await client.PostAsJsonAsync(uri, new { });
            var url = await response.Content.ReadAsStringAsync();
            return url;
        }

        public async Task StopInstance(string instanceName)
        {
            var uri = $"/stop/?instanceName={HttpUtility.UrlEncode(instanceName)}";
            using var client = Init();
            using var response = await client.PostAsJsonAsync(uri, new { });
        }

        public IAsyncEnumerable<string> Logs(string instanceName, CancellationToken token)
        {
            var uri = $"/logs/{HttpUtility.UrlPathEncode(instanceName)}";

            async IAsyncEnumerable<string> Read()
            {
                using var client = Init();
                using var response = await client.GetAsync(uri, token);
                using var body = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(body);
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }

            return Read();
        }

    }
}