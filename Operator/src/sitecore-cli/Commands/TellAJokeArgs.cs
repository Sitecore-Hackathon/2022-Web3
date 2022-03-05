using Sitecore.DevEx.Client.Tasks;

namespace DadJokeCli.Commands
{
    public class TellAJokeArgs : TaskOptionsBase
    {
        public bool SitecoreJoke { get; set; }
        public override void Validate()
        {
        }
    }
}