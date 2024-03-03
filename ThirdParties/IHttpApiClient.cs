namespace FormResponse.Api.ThirdParties
{
    public interface IHttpApiClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
