using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System.Threading.Tasks;
using Web3.Operator.Cli.Commands;

namespace Web3.Operator.Cli.Tasks
{
    public class ListInstancesTask
    {
        private readonly ILogger<ListInstancesTask> _logger;

        public ListInstancesTask(ILogger<ListInstancesTask> logger)
        {
            _logger = logger;
        }

        public async Task Execute(ListInstancesArgs args)
        {
            ColorLogExtensions.LogConsole(_logger, LogLevel.Warning, "LIST");
        }
    }
}