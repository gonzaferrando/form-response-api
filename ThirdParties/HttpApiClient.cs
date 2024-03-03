using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FormResponse.Api.ThirdParties
{
    public class HttpApiClient : IHttpApiClient
    {
        protected readonly HttpClient _client;
        protected readonly string _baseUrl;
        private static readonly string apiKey = "sk_prod_TfMbARhdgues5AuIosvvdAC9WsA5kXiZlW8HZPaRDlIbCpSpLsXBeZO7dCVZQwHAY3P4VSBPiiC33poZ1tdUj2ljOzdTCCOSpUZ_3912";

        public HttpApiClient(string baseUrl)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            _baseUrl = baseUrl;
        }

        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var response = await _client.GetAsync(string.Concat(_baseUrl, url));
            var rrrppp = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(rrrppp);
        }
    }

}
