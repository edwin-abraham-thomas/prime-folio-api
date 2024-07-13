using AutoMapper;
using core;
using core.Models;
using Interfaces.Repositories;
using Interfaces.Services;
using Models;
using Models.Requests;
using MongoDB.Driver;
using static core.Constants;

namespace Services
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<ContentService> _logger;

        public ContentService(IContentRepository contentRepository, IUserService userService, IMapper mapper, ILogger<ContentService> logger)
        {
            _contentRepository = contentRepository;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<Content>> CreateContentAsync(ContentCreateRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var userResponse = await _userService.GetUserAsync(request.UserId, cancellationToken);
            if (userResponse.Result == null) return Response<Content>.Failure("User does not exist");

            var content = _mapper.Map<entities.Content>(request);

            try
            {
                await _contentRepository.InsertAsync(content, cancellationToken);
                return Response<Content>.Success(_mapper.Map<Content>(request));
            }
            catch (Exception ex)
            {
                return Response<Content>.Failure(ex.Message, Constants.CommonErrorCodes.INTERNAL, ex);
            }

        }

        public async Task<Response<Content>> GetContentAsync(string userId, CancellationToken cancellationToken)
        {
            var content = await _contentRepository.FindOneAsync(c => c.UserId == userId, cancellationToken);
            if (content == null)
            {
                return Response<Content>.Failure("Requested content not found", CommonErrorCodes.NOT_FOUND);
            }
            var contentModel = _mapper.Map<Content>(content);
            return Response<Content>.Success(contentModel);
        }

        public async Task<Response<Content>> UpdateContentAsync(ContentUpdateRequest request, CancellationToken cancellationToken)
        {
            var content = await _contentRepository.FindOneAsync(c => c.UserId == request.UserId, cancellationToken);
            if (content == null)
            {
                return Response<Content>.Failure("Requested content not found", CommonErrorCodes.NOT_FOUND);
            }

            var contentEntityUpdate = _mapper.Map<entities.Content>(request);

            if (contentEntityUpdate == null)
            {
                return Response<Content>.Failure("Failed to update content", CommonErrorCodes.INTERNAL);
            }

            try
            {
                var documentIdentifierFilterDefinition = Builders<entities.Content>.Filter.Eq(c => c.UserId, request.UserId);
                await _contentRepository.UpdateAsync(contentEntityUpdate, documentIdentifierFilterDefinition, cancellationToken);

                return Response<Content>.Success(_mapper.Map<Content>(contentEntityUpdate));
            }
            catch (Exception ex)
            {
                return Response<Content>.Failure("Failed to update content", CommonErrorCodes.INTERNAL, ex);
            }
        }

        public async Task<Response<bool>> DeleteContentAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var filterDef = Builders<entities.Content>.Filter.Eq(c => c.UserId, userId);
                var isSuccess = await _contentRepository.DeleteAsync(filterDef, cancellationToken);

                if (isSuccess)
                {
                    return Response<bool>.Success(isSuccess);
                }
                else
                {
                    return Response<bool>.Failure("Failed to delete content", CommonErrorCodes.INTERNAL);
                }
            }
            catch (Exception ex)
            {
                return Response<bool>.Failure("Failed to delete content", CommonErrorCodes.INTERNAL, ex);
            }
        }

    }
}
