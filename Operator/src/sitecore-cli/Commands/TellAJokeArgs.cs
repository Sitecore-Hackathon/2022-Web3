using Sitecore.DevEx.Client.Tasks;

namespace Web3.Operator.Cli.Commands
{
    public class TellAJokeArgs : TaskOptionsBase
    {
        public bool SitecoreJoke { get; set; }
        public override void Validate()
        {
        }
    }
}