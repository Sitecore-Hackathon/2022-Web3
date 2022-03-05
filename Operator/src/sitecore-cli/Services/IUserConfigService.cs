namespace Web3.Operator.Cli.Services
{
    public interface IUserConfigService
    {
        string GetOperatorBaseUrl();
        void UpdateOperatorBaseUrl(string value);
    }
}
