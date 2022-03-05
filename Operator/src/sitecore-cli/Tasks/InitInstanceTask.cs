using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System.Linq;
using System.Threading.Tasks;
using Web3.Operator.Cli.Clients;
using Web3.Operator.Cli.Commands;
using Web3.Operator.Cli.Services;

namespace Web3.Operator.Cli.Tasks
{
    public class InitInstanceTask
    {
        private readonly ILogger<InitInstanceTask> _logger;
        private readonly IUserConfigService _userConfigService;

        public InitInstanceTask(ILogger<InitInstanceTask> logger, IUserConfigService userConfigService)
        {
            _logger = logger;
            _userConfigService = userConfigService;
        }

        public async Task Execute(InitInstanceArgs args)
        {
            args.Validate();
            _userConfigService.UpdateOperatorBaseUrl(args.OperatorBaseUrl);
            ColorLogExtensions.LogConsole(_logger, LogLevel.Information, "Base Url was set", System.ConsoleColor.Green);
        }
    }
}