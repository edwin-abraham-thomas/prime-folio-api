using core.Models;
using Models.Requests;

namespace Interfaces.Services
{
    public interface IContentService
    {
        Task<Response<Models.Content>> CreateContentAsync(ContentCreateRequest request, CancellationToken cancellationToken);
        Task<Response<Models.Content>> GetContentAsync(string userId, CancellationToken cancellationToken);
        Task<Response<Models.Content>> UpdateContentAsync(ContentUpdateRequest request, CancellationToken cancellationToken);
        Task<Response<bool>> DeleteContentAsync(string userId, CancellationToken cancellationToken);
    }
}
