using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System;
using System.Threading.Tasks;
using Web3.Operator.Cli.Tasks;

namespace Web3.Operator.Cli.Commands
{
    public class StartInstanceCommand : SubcommandBase<StartInstanceTask, StartInstanceArgs>
    {
        public StartInstanceCommand(IServiceProvider container) : base("start", "Starts an instance", container)
        {
            AddAlias("nft");
            AddOption(ArgOptions.SitecoreAdminPassword);
        }

        protected override async Task<int> Handle(StartInstanceTask task, StartInstanceArgs args)
        {
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}