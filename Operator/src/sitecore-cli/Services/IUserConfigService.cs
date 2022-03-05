using System.Threading.Tasks;

namespace Web3.Operator.Cli.Services
{
    public interface IUserConfigService
    {
        string GetOperatorBaseUrl();
        Task UpdateOperatorBaseUrl(string value);
    }
}
