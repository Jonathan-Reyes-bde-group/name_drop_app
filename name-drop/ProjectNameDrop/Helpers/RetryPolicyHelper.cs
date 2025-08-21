using Polly;

namespace ProjectNameDrop.Helpers
{
    /// <summary>
    /// Resilient helper class for retry policies.
    /// </summary>
    public static class RetryPolicyHelper
    {
        private static ILogger _logger;

        public static void Initialize(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(typeof(RetryPolicyHelper));
        }
        public static IAsyncPolicy RetryAnyHttpRequest(int maxRetries = 3)
        {
            return Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(maxRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, delay, retryAttempt, context) =>
                {
                    _logger.LogError(
                        $"HTTP Transient error occurred. Retry attempt {retryAttempt}/{maxRetries}. " +
                        $"Exception: {exception.Message} - {exception.InnerException?.Message}");
                });
        }
    }
}
