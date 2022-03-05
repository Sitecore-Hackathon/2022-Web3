using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System;
using System.Threading.Tasks;
using Web3.Operator.Cli.Tasks;

namespace Web3.Operator.Cli.Commands
{
    public class InitInstanceCommand : SubcommandBase<InitInstanceTask, InitInstanceArgs>
    {
        public InitInstanceCommand(IServiceProvider container) : base("init", "Initiates the operator", container)
        {
            AddOption(ArgOptions.OperatorBaseUrl);
        }

        protected override async Task<int> Handle(InitInstanceTask task, InitInstanceArgs args)
        {
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}