using AutoMapper;
using core;
using core.Models;
using Interfaces.Repositories;
using Interfaces.Services;
using Models;
using Models.Requests;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<User?>> InsertUserAsync(UserCreateRequest user, CancellationToken cancellationToken)
        {
            var userEntity = _mapper.Map<entities.User>(user);
            try
            {
                await _userRepository.InsertUserAsync(userEntity, cancellationToken);

                return Response<User?>.Success(_mapper.Map<User>(user));
            }
            catch (Exception ex)
            {
                return Response<User?>.Failure(ex.Message, Constants.CommonErrorCodes.INTERNAL, ex);
            }
        }

        public async Task<Response<User?>> UpdateUserAsync(UserUpdateRequest user, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserAsync(user.UserId, cancellationToken);
            if (existingUser == null)
            {
                return Response<User?>.Failure("Requested user not found", Constants.CommonErrorCodes.NOT_FOUND);
            }

            var updateRequest = _mapper.Map<entities.User>(user);
            var updateResponse = await _userRepository.UpdateUserAsync(updateRequest, cancellationToken);

            return Response<User?>.Success(_mapper.Map<User>(updateResponse));
        }

        public async Task<Response<User?>> GetUserAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(userId, cancellationToken);

            if (user == null)
            {
                return Response<User?>.Failure("Requested user not found", Constants.CommonErrorCodes.NOT_FOUND);
            }

            return Response<User?>.Success(_mapper.Map<User>(user));
        }

        public async Task<Response<bool>> DeleteUserAsync(string userId, CancellationToken cancellationToken)
        {
            var response = await _userRepository.DeleteUserAsync(userId, cancellationToken);

            return Response<bool>.Success(response);
        }
    }
}
