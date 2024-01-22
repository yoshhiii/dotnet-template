namespace Relias.UserProfile.Cqrs.Infra.Repository
{
    /// <summary>
    /// User Profile repo definition
    /// </summary>
    public interface IUserProfileRepository
    {
        Task<Dao.UserProfile?> GetAsync(string userId);

        Task<Dao.UserProfile> CreateAsync(Dao.UserProfile userProfile);

        Task<Dao.UserProfile> UpdateAsync(Dao.UserProfile userProfile);

        Task DeleteAsync(string userId);
    }
}
