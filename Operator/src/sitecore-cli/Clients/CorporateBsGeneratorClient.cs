using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Web3.Operator.Cli.Models;

namespace Web3.Operator.Cli.Clients
{
    public class CorporateBsGeneratorClient
    {
        private HttpClient _httpClient;

        public CorporateBsGeneratorClient()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> GetMessageFromTheCeo()
        {
            var response = await _httpClient.GetStringAsync("https://corporatebs-generator.sameerkumar.website/");
            var doCoolStuff = JsonConvert.DeserializeObject<CorporateBsModel>(response);
            return $"Sitecore will {doCoolStuff.Phrase.ToLower()} very soon. I promise!";
        }
    }
}
