using Sitecore.DevEx.Client.Tasks;

namespace Web3.Operator.Cli.Commands
{
    public class InitInstanceArgs: TaskOptionsBase
    {
        public string OperatorBaseUrl { get; set; }
 
        public override void Validate()
        {
            Require(nameof(OperatorBaseUrl));
        }
    }
}