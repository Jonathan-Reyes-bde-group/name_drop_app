using ProjectNameDrop.Helpers;
using ProjectNameDrop.Models.Response;
using ProjectNameDrop.Services.Interfaces;

namespace ProjectNameDrop.Services
{
    public class NationalizeServices(ILogger<NationalizeServices> _logger, IHttpClientFactory httpClientFactory) : INationalizeServices
    {
        public async Task<NationalizeGetResponse> GetPossibleNationalAsync(string name)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("Nationalize");
                var retryPolicy = RetryPolicyHelper.RetryAnyHttpRequest();
                var response = await retryPolicy.ExecuteAsync(async () =>
                {
                    var url = $"?name={Uri.EscapeDataString(name)}";
                    return await httpClient.GetAsync(url);
                });
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadFromJsonAsync<NationalizeGetResponse>();
                return content ?? throw new InvalidOperationException("Response content is null");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting possible national for name: {Name}", name);
                throw;
            }
        }
    }
}
