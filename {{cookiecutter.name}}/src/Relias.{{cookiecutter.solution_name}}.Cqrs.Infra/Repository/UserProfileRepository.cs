using Microsoft.Azure.CosmosRepository;
using Microsoft.Extensions.Logging;
using Polly;
using Relias.UserProfile.Common.Exceptions;
using Relias.UserProfile.Cqrs.Infra.RetryPolicies;

namespace Relias.{{cookiecutter.solution_name}}.Cqrs.Infra.Repository
{
    /// <summary>
    /// User Profile repo implementation
    /// </summary>
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IRepository<Dao.UserProfile> _repository;
        private readonly AsyncPolicy _retryPolicy;

        /// <summary>
        /// Default constructor with injected dependencies
        /// </summary>
        public UserProfileRepository(IRepository<Dao.UserProfile> repository, ILogger<UserProfileRepository> logger)
        {
            _repository = repository;
            _retryPolicy = CosmosDbRetryPolicy.Get(logger);
        }

        /// <summary>
        /// Returns user profile for a given user id
        /// </summary>
        public async Task<Dao.UserProfile?> GetAsync(string userId)
        {
            string query = $"SELECT * FROM c WHERE c.userId=\"{userId}\"";
            
            var userProfiles = await _retryPolicy.ExecuteAsync<IEnumerable<Dao.UserProfile?>>(async () => await _repository.GetByQueryAsync(query)
            );

            return userProfiles?.FirstOrDefault();
        }

        /// <summary>
        /// Creates a user profile in the DB
        /// </summary>
        public async Task<Dao.UserProfile> CreateAsync(Dao.UserProfile userProfile)
        {
            return await _retryPolicy.ExecuteAsync(async () => await _repository.CreateAsync(userProfile));
        }

        /// <summary>
        /// Updates user profile in the DB
        /// </summary>
        public async Task<Dao.UserProfile> UpdateAsync(Dao.UserProfile userProfile)
        {
            return await _retryPolicy.ExecuteAsync(async () => await _repository.UpdateAsync(userProfile));
        }

        /// <summary>
        /// Deletes user profile from the DB
        /// </summary>
        public async Task DeleteAsync(string userId)
        {
            var userProfile = await GetAsync(userId);
            if (userProfile == null)
            {
                throw new ItemNotFoundException(userId);
            }

            await _retryPolicy.ExecuteAsync(async () => await _repository.DeleteAsync(userProfile));
        }
    }
}
