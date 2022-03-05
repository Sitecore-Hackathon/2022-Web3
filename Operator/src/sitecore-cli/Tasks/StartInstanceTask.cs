using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System.Threading.Tasks;
using Web3.Operator.Cli.Clients;
using Web3.Operator.Cli.Commands;

namespace Web3.Operator.Cli.Tasks
{
    public class StartInstanceTask
    {
        private readonly ILogger<StartInstanceTask> _logger;
        private readonly IOperatorClient _client;

        public StartInstanceTask(ILogger<StartInstanceTask> logger, IOperatorClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Execute(StartInstanceArgs args)
        {
            args.Validate();

            _logger.LogConsoleInformation("Starting instance...");
            var result = await _client.StartNewInstance(args.InstanceName, args.SitecoreAdminPassword);
            if(result != null)
            {
                _logger.LogConsoleInformation($"{result.State} {result.Url}", System.ConsoleColor.Green);
            }
        }
    }
}