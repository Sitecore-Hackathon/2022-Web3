using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System;
using System.Threading.Tasks;
using Web3.Operator.Cli.Tasks;

namespace Web3.Operator.Cli.Commands
{
    public class LogsCommand : SubcommandBase<LogsTask, LogsArgs>
    {
        public LogsCommand(IServiceProvider container) : base("logs", "Show logs from instance", container)
        {
            AddOption(ArgOptions.InstanceName);
        }

        protected override async Task<int> Handle(LogsTask task, LogsArgs args)
        {
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}