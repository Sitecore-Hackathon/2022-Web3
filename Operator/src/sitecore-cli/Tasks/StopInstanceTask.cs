using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System.Threading.Tasks;
using Web3.Operator.Cli.Commands;

namespace Web3.Operator.Cli.Tasks
{
    public class StopInstanceTask
    {
        private readonly ILogger<StopInstanceTask> _logger;

        public StopInstanceTask(ILogger<StopInstanceTask> logger)
        {
            _logger = logger;
        }

        public async Task Execute(StopInstanceArgs args)
        {
            ColorLogExtensions.LogConsole(_logger, LogLevel.Warning, args.InstanceName);
        }
    }
}