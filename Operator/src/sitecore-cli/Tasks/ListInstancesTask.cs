using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System.Threading.Tasks;
using Web3.Operator.Cli.Clients;
using Web3.Operator.Cli.Commands;

namespace Web3.Operator.Cli.Tasks
{
    public class ListInstancesTask
    {
        private readonly ILogger<ListInstancesTask> _logger;
        private readonly IOperatorClient _client;

        public ListInstancesTask(ILogger<ListInstancesTask> logger, IOperatorClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Execute(ListInstancesArgs args)
        {
            args.Validate();

            ColorLogExtensions.LogConsole(_logger, LogLevel.Warning, "LIST");
        }
    }
}