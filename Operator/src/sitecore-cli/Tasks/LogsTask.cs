using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Web3.Operator.Cli.Clients;
using Web3.Operator.Cli.Commands;

namespace Web3.Operator.Cli.Tasks
{
    public class LogsTask
    {
        private readonly ILogger<LogsTask> _logger;
        private readonly IOperatorClient _client;

        public LogsTask(ILogger<LogsTask> logger, IOperatorClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Execute(LogsArgs args)
        {
            args.Validate();

            var token = new CancellationToken();
            var enumerator = _client.Logs(args.InstanceName, token);
            await foreach(var item in enumerator)
            {
                _logger.LogConsoleInformation(item, System.ConsoleColor.Gray);
            }
        }
    }
}