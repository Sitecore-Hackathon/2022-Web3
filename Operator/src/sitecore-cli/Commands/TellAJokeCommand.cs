using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System;
using System.Threading.Tasks;

namespace Web3.Operator.Cli
{
    public class TellAJokeCommand : SubcommandBase<TellAJokeTask, TellAJokeArgs>
    {
        public TellAJokeCommand(IServiceProvider container) : base("tell", "Tells an important joke", container)
        {
            AddOption(ArgOptions.SitecoreJoke);
        }

        protected override async Task<int> Handle(TellAJokeTask task, TellAJokeArgs args)
        {
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}
