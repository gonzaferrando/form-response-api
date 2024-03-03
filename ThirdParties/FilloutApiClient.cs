namespace FormResponse.Api.ThirdParties
{
    public class FilloutApiClient : HttpApiClient
    {
        public FilloutApiClient(string baseUrl) : base(baseUrl) { }

        public async Task GetDataAsync()
        {
            var response = await GetAsync("/");
            // Handle response and deserialize data
            // ...
        }
    }
}
