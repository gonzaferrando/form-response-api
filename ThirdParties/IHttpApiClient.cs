namespace FormResponse.Api.ThirdParties
{
    public interface IHttpApiClient
    {
        Task<T> GetAsync<T>(string url);
    }
}
