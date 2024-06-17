using core.Utilities;
using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Requests;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<User?>> PostAsync([FromBody] UserCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.InsertUserAsync(request, cancellationToken);

            return ResultMapper.ConvertToActionResult(response);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<User?>> GetAsync([FromRoute] string userId, CancellationToken cancellationToken)
        {
            //var response = await _userService.GetUserAsync(userId, cancellationToken);

            //return ResultMapper.ConvertToActionResult(response);
            return Ok(new User
            {
                _id = userId,
                FirstName = userId,
                LastName = userId,
                Email = userId,
            });
        }

        [HttpPut]
        public async Task<ActionResult<User?>> PutAsync([FromBody] UserUpdateRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.UpdateUserAsync(request, cancellationToken);

            return ResultMapper.ConvertToActionResult(response);
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute] string userId, CancellationToken cancellationToken)
        {
            var response = await _userService.DeleteUserAsync(userId, cancellationToken);

            return ResultMapper.ConvertToActionResult(response);
        }
    }
}
