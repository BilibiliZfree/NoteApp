using Microsoft.AspNetCore.Mvc;
using NoteApp.Api.Services;
using NoteApp.Models;

namespace NoteApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpDelete]
        [Tags("用户管理")]
        public async Task<ApiResponse> DeleteUserByIdAsync([FromQuery] int id) => await _userService.DeleteResponseAsync(id);

        [HttpGet]
        [Tags("用户管理")]
        public async Task<ApiResponse> GetUserByIdAsync([FromQuery] int id) => await _userService.GetResponseAsync(id);

        [HttpGet]
        [Tags("用户管理")]
        public async Task<ApiResponse> GetUsersAsync() => await _userService.GetsResponseAsync();

        [HttpGet]
        [Tags("用户管理")]
        public async Task<ApiResponse> SearchUsersAsync([FromQuery] string data, [FromQuery] string key) => await _userService.GetsResponseAsync(data,key);

        [HttpGet]
        [Tags("用户管理")]
        public async Task<ApiResponse> LoginByIDAsync([FromQuery] int id, [FromQuery] string password) => await _userService.LoginAsync(id, password);
        [HttpGet]
        [Tags("用户管理")]
        public async Task<ApiResponse> LoginByUserNameAsync([FromQuery] string username, [FromQuery] string password) => await _userService.LoginAsync(username, password);

        [HttpPost]
        [Tags("用户管理")]
        public async Task<ApiResponse> RegisterUserAsync([FromBody] UserEntity user) => await _userService.PostResponseAsync(user);

        [HttpPut]
        [Tags("用户管理")]
        public async Task<ApiResponse> ModifyUserAsync([FromBody] UserEntity user) => await _userService.PutResponseAsync(user);
    }
}
