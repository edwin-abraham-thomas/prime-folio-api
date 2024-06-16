using core.Models;
using Models;
using Models.Requests;

namespace Interfaces.Services
{
    public interface IUserService
    {
        Task<Response<User?>> InsertUserAsync(UserCreateRequest user, CancellationToken cancellationToken);
        Task<Response<User?>> UpdateUserAsync(UserUpdateRequest user, CancellationToken cancellationToken);
        Task<Response<User?>> GetUserAsync(string userId, CancellationToken cancellationToken);
        Task<Response<bool>> DeleteUserAsync(string userId, CancellationToken cancellationToken);
    }
}
