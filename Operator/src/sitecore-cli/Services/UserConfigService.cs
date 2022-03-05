using Newtonsoft.Json.Serialization;
using System.IO;
using System.Threading.Tasks;

namespace Web3.Operator.Cli.Services
{
    public class UserConfigRecord
    {
        public string OperatorBaseUrl { get; set; }
    }

    public class UserConfigService : IUserConfigService
    {
        private readonly string _sitecoreUserFile = @"\.sitecore\web3.json.user";

        public string GetOperatorBaseUrl()
        {
            try
            {
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<UserConfigRecord>(GetConfig());
                return obj?.OperatorBaseUrl;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task UpdateOperatorBaseUrl(string value)
        {
            var obj = new UserConfigRecord
            {
                OperatorBaseUrl = value
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });
            await SaveConfig(json);
        }

        private string GetConfig()
        {
            const string defaultValue = "{}";
            string path = Directory.GetCurrentDirectory() + _sitecoreUserFile;
            try
            {
                if (!File.Exists(path))
                {
                    return defaultValue;
                }

                using var reader = new StreamReader(path);
                return reader.ReadToEnd();
            }
            catch
            {
                return defaultValue;
            }
        }

        private async Task SaveConfig(string value)
        {
            string path = Directory.GetCurrentDirectory() + _sitecoreUserFile;
            try
            {
                using var writer = new StreamWriter(path);
                await writer.WriteAsync(value);
            }
            catch
            {
                return;
            }
        }
    }
}
