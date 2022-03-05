using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System.Threading.Tasks;
using Web3.Operator.Cli.Commands;

namespace Web3.Operator.Cli.Tasks
{
    public class StartInstanceTask
    {
        private readonly ILogger<StartInstanceTask> _logger;

        public StartInstanceTask(ILogger<StartInstanceTask> logger)
        {
            _logger = logger;
        }

        public async Task Execute(StartInstanceArgs args)
        {
            ColorLogExtensions.LogConsole(_logger, LogLevel.Warning, args.SitecoreAdminPassword);
        }
    }
}