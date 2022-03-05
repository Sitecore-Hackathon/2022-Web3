using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System;
using System.Threading.Tasks;
using Web3.Operator.Cli.Tasks;

namespace Web3.Operator.Cli.Commands
{
    public class ListInstancesCommand : SubcommandBase<ListInstancesTask, ListInstancesArgs>
    {
        public ListInstancesCommand(IServiceProvider container) : base("list", "Lists running instance", container)
        {
            AddAlias("wallet");
            AddOption(ArgOptions.SitecoreAdminPassword);
        }

        protected override async Task<int> Handle(ListInstancesTask task, ListInstancesArgs args)
        {
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}