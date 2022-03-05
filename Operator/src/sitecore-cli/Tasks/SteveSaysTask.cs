using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System.Threading.Tasks;
using Web3.Operator.Cli.Commands;

namespace Web3.Operator.Cli.Tasks
{
    public class SteveSaysTask
    {
        private readonly ILogger<SteveSaysTask> _logger;

        public SteveSaysTask(ILogger<SteveSaysTask> logger)
        {
            _logger = logger;
        }

        public async Task Execute(SteveSaysArgs args)
        {
            ColorLogExtensions.LogConsole(_logger, LogLevel.Warning, "Sitecore will: https://corporatebs-generator.sameerkumar.website/");
        }
    }
}