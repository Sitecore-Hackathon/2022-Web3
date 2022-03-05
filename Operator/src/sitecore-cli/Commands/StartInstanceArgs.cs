using Sitecore.DevEx.Client.Tasks;

namespace Web3.Operator.Cli.Commands
{
    public class StartInstanceArgs : TaskOptionsBase
    {
        public string SitecoreAdminPassword { get; set; }

        public override void Validate()
        {
            Require(nameof(SitecoreAdminPassword));
        }
    }
}