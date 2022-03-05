using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System;
using System.Threading.Tasks;
using Web3.Operator.Cli.Tasks;

namespace Web3.Operator.Cli.Commands
{
    public class StopInstanceCommand : SubcommandBase<StopInstanceTask, StopInstanceArgs>
    {
        public StopInstanceCommand(IServiceProvider container) : base("stop", "Stops an instance", container)
        {
            AddAlias("blockchain");
            AddOption(ArgOptions.InstanceName);
        }

        protected override async Task<int> Handle(StopInstanceTask task, StopInstanceArgs args)
        {
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}