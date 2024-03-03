using System.Net.Http.Headers;

namespace FormResponse.Api.ThirdParties
{
    public class HttpApiClient : IHttpApiClient
    {
        protected readonly HttpClient _client;
        protected readonly string _baseUrl;
        private static readonly string apiKey = "sk_prod_TfMbARhdgues5AuIosvvdAC9WsA5kXiZlW8HZPaRDlIbCpSpLsXBeZO7dCVZQwHAY3P4VSBPiiC33poZ1tdUj2ljOzdTCCOSpUZ_3912"; // Replace with your actual API key

        public HttpApiClient(string baseUrl)
        {
            _client = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            return await _client.GetAsync(string.Concat(_baseUrl, url));
        }
    }

}
