using AutoMapper;
using core.Models;
using Interfaces.Repositories;
using Interfaces.Services;
using Models;
using Models.Requests;
using static core.Constants;

namespace Services
{
    public class ContentService : IContentService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ContentService> _logger;

        public ContentService(IUserRepository userRepository, IMapper mapper, ILogger<ContentService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<Content>> GetContentAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(userId, cancellationToken);
            if (user == null)
            {
                return Response<Content>.Failure("Requested user not found", CommonErrorCodes.NOT_FOUND);
            }
            var contentModel = _mapper.Map<Content>(user.Content);
            return Response<Content>.Success(contentModel);
        }

        public async Task<Response<Content>> UpdateContentAsync(ContentUpdateRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                return Response<Content>.Failure("Requested user not found", CommonErrorCodes.NOT_FOUND);
            }

            var contentEntity = _mapper.Map<entities.Content>(request.Content);

            if (contentEntity == null)
            {
                return Response<Content>.Failure("Failed to update content", CommonErrorCodes.INTERNAL);
            }

            user.Content = contentEntity;

            try
            {
                await _userRepository.UpdateUserAsync(user, cancellationToken);

                return Response<Content>.Success(_mapper.Map<Content>(contentEntity));
            }
            catch (Exception ex)
            {
                return Response<Content>.Failure("Failed to update content", CommonErrorCodes.INTERNAL, ex);
            }
        }
    }
}
