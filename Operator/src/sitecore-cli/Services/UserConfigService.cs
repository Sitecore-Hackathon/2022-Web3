using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Web3.Operator.Cli.Services
{
    public class UserConfigService : IUserConfigService
    {
        private readonly string _sitecoreUserFile = @"\.sitecore\user.json";

        public string GetOperatorBaseUrl()
        {
            try
            {
                var root = JObject.Parse(GetConfig());
                if (root == null)
                    return string.Empty;

                var web3 = root.Children<JProperty>().FirstOrDefault(x => x.Name == "web3");
                if (web3 == null)
                    return string.Empty;

                var inner = web3.Children<JObject>().FirstOrDefault();
                var operatorBaseUrl = inner.Children<JProperty>().FirstOrDefault(x => x.Name == "operatorBaseUrl");
               
                return (string)operatorBaseUrl.Value;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void UpdateOperatorBaseUrl(string value)
        {
            var root = JObject.Parse(GetConfig());
            var web3 = root.Children<JProperty>().FirstOrDefault(x => x.Name == "web3");

            if (web3 != null)
            {
                root.Remove("web3");
            }

            JProperty prop = new JProperty("web3");
            JObject config = new JObject();
            JProperty operatorBaseUrl = new JProperty("operatorBaseUrl", value);
            config.Add(operatorBaseUrl);
            prop.Value = config;
            root.Add(prop);

            SaveConfig(root.ToString());
        }

        private string GetConfig()
        {
            string path = Directory.GetCurrentDirectory() + _sitecoreUserFile;
            try
            {
                using (var reader = new StreamReader(path))
                {
                    return reader.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        private void SaveConfig(string value)
        {
            string path = Directory.GetCurrentDirectory() + _sitecoreUserFile;
            try
            {
                using (var writer = new StreamWriter(path))
                {
                    writer.Write(value);
                }
            }
            catch
            {
                return;
            }
        }
    }
}
