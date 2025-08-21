using ProjectNameDrop.Helpers;
using ProjectNameDrop.Models.Response;
using ProjectNameDrop.Services.Interfaces;

namespace ProjectNameDrop.Services
{
    public class AgifyServices(ILogger<AgifyServices> _logger, IHttpClientFactory httpClientFactory) : IAgifyServices
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Agify");
        public async Task<AgifyGetResponse> GetPossibleAgeAsync(string name)
        {
            try
            {
                var retryPolicy = RetryPolicyHelper.RetryAnyHttpRequest();
                var response = await retryPolicy.ExecuteAsync(async () =>
                {
                    var url = $"?name={Uri.EscapeDataString(name)}";
                    return await _httpClient.GetAsync(url);
                });

                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadFromJsonAsync<AgifyGetResponse>();

                return content ?? throw new InvalidOperationException("Response content is null");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting possible age for name: {Name}", name);
                throw;
            }

        }
    }
}
