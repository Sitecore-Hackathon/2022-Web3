using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System.Threading.Tasks;
using Web3.Operator.Cli.Clients;
using Web3.Operator.Cli.Commands;

namespace Web3.Operator.Cli.Tasks
{
    public class SteveSaysTask
    {
        private readonly ILogger<SteveSaysTask> _logger;
        private readonly CorporateBsGeneratorClient _corporateBsGeneratorClient;

        public SteveSaysTask(ILogger<SteveSaysTask> logger, CorporateBsGeneratorClient corporateBsGeneratorClient)
        {
            _logger = logger;
            _corporateBsGeneratorClient = corporateBsGeneratorClient;
        }

        public async Task Execute(SteveSaysArgs args)
        {

            ColorLogExtensions.LogConsole(_logger, LogLevel.Warning, await _corporateBsGeneratorClient.GetMessageFromTheCeo());
        }
    }
}