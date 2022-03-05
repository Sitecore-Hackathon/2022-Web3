using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System.CommandLine;

namespace Web3.Operator.Cli
{
    internal class JokesCommand : Command, ISubcommand
    {
        public JokesCommand(string name, string description = null) : base(name, description)
        {
        }
    }
}