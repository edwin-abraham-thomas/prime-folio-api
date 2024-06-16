using entities;

namespace Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task InsertUserAsync(User user, CancellationToken cancellationToken);
        Task<User?> UpdateUserAsync(User user, CancellationToken cancellationToken);
        Task<User?> GetUserAsync(string userId, CancellationToken cancellationToken);
        Task<bool> DeleteUserAsync(string userId, CancellationToken cancellationToken);
    }
}
