using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Polly;
using System.Net;

namespace Relias.{{cookiecutter.solution_name}}.Cqrs.Infra.RetryPolicies
{
    /// <summary>
    /// Cosmos DB retry policy to retry on specific errors
    /// Review https://docs.microsoft.com/en-us/azure/cosmos-db/sql/conceptual-resilient-sdk-applications for guidance
    /// </summary>
    public static class CosmosDbRetryPolicy
    {
        private const int MAX_RETRY_COUNT = 3;
        private const int RETRY_TIME_MULTIPLIER = 3;

        private static readonly HttpStatusCode[] CosmosRetryStatusCodes =        
        { 
            HttpStatusCode.RequestTimeout,      // 408
            HttpStatusCode.Gone,                // 410
            HttpStatusCode.ServiceUnavailable   // 503
        }; 

        /// <summary>
        /// Policy that retries on MS Graph transient errors
        /// </summary>
        public static AsyncPolicy Get(ILogger logger)
        {
            return Policy
                .Handle<CosmosException>(ex => CosmosRetryStatusCodes.Contains(ex.StatusCode))
                .WaitAndRetryAsync(
                    retryCount: MAX_RETRY_COUNT, 
                    sleepDurationProvider: (retryAttempt) => TimeSpan.FromSeconds(retryAttempt * RETRY_TIME_MULTIPLIER),
                    onRetry: (exception, waitDuration) =>
                    {
                        logger.LogWarning(message: "Retry after {TotalSeconds} seconds, call failed with error {Message} : {StackTrace}", waitDuration.TotalSeconds, exception?.Message, exception?.StackTrace);
                    });
        }
    }
}
