using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System.Threading.Tasks;
using Web3.Operator.Cli.Clients;
using Web3.Operator.Cli.Commands;

namespace Web3.Operator.Cli.Tasks
{
    public class StopInstanceTask
    {
        private readonly ILogger<StopInstanceTask> _logger;
        private readonly IOperatorClient _client;

        public StopInstanceTask(ILogger<StopInstanceTask> logger, IOperatorClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Execute(StopInstanceArgs args)
        {
            await _client.StopInstance(args.InstanceName);
            _logger.LogConsoleInformation("Done", System.ConsoleColor.Green);
        }
    }
}