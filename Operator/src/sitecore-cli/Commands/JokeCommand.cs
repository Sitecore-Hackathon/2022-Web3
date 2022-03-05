using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System.CommandLine;

namespace Web3.Operator.Cli
{
    internal class JokeCommand : Command, ISubcommand
    {
        public JokeCommand(string name, string description = null) : base(name, description)
        {
        }
    }
}