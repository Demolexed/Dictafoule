using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DictaFoule.Mobile.iOS.API
{
    public class HttpClientService
    {
        #region constante

        private const string apiDev = "https://dev-api-omink-dictafoule.azurewebsites.net/v1";

        #endregion

        private readonly static HttpClient httpClient = new HttpClient();

        public HttpClientService()
        {
            httpClient.BaseAddress = new Uri(apiDev);
        }

        public async Task<string> PostService(string url, StringContent content)
        {
           var response = await httpClient.PostAsync(url, content);
           var message = await response.Content.ReadAsStringAsync();
           return message;
        }

        public async Task<string> GetService(string url)
        {
            var response = await httpClient.GetAsync(url);
            var message = await response.Content.ReadAsStringAsync();
            return message;
        }
    }
}
