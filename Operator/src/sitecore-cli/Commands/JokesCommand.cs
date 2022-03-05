using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System.CommandLine;

namespace DadJokeCli.Commands
{
    internal class JokesCommand : Command, ISubcommand
    {
        public JokesCommand(string name, string description = null) : base(name, description)
        {
        }
    }
}