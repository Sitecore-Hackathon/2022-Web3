using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System.CommandLine;

namespace Web3.Operator.Cli.Commands
{
    public class SteveCommand : Command, ISubcommand
    {
        public SteveCommand(string name, string description = null) : base(name, description)
        {
        }
    }
}
