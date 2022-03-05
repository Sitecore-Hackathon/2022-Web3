using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System;
using System.Threading.Tasks;
using Web3.Operator.Cli.Tasks;

namespace Web3.Operator.Cli.Commands
{
    public class SteveSaysCommand : SubcommandBase<SteveSaysTask, SteveSaysArgs>
    {
        public SteveSaysCommand(IServiceProvider container) : base("says", "Get informed on Sitecore's roadmap by the CEO", container)
        {
            AddAlias("keynote");
        }

        protected override async Task<int> Handle(SteveSaysTask task, SteveSaysArgs args)
        {
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}