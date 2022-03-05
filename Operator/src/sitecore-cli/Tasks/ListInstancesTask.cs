using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System.Linq;
using System.Threading.Tasks;
using Web3.Operator.Cli.Clients;
using Web3.Operator.Cli.Commands;
using Web3.Operator.Cli.Models;

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
            var result = await _client.List();
            _logger.LogConsoleInformation("Instances:");

            var values = new[] { 
                new InstanceDetails { HostName = "HOSTNAME", InstanceName = "INSTANCENAME", Url = "URL", State = "STATE" } 
            }.Union(result);
            var widthName = values.Max(x => x.InstanceName.Length);
            var widthState = values.Max(x => x.State.Length);

            foreach (var itm in values)
            {
                _logger.LogConsoleInformation($"{itm.InstanceName.PadRight(widthName)}  {itm.State.PadRight(widthState)}  {itm.Url}");
            }
        }
    }
}