using Microsoft.AspNetCore.Mvc;
//using NoteApp.Api.Data;
using NoteApp.Api.Services;
using NoteApp.Api.Services.Interfaces;
using NoteApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;

        public UsersController(UsersService service)
        {
            _service = service;
        }

        // GET: api/Users/GetUsers
        [HttpGet]
        [Tags("用户管理")]
        public async Task<ApiResponse> GetUsersAsync()
        {
            return await _service.GetsResponseAsync();
        }
        // Get api/Users/GetUser
        [HttpGet]
        [Tags("用户管理")]
        public async Task<ApiResponse> GetUserAsync(int id)
        {
            return await _service.GetResponseAsync(id);
        }

        // Post api/Users/PostUser
        [HttpPost]
        [Tags("用户管理")]
        public async Task<ApiResponse> PostUserAsync(UserEntity newUser)
        {
            return await _service.PostResponseAsync(newUser);
        }

        // Put api/Users/PutUser
        [HttpPut]
        [Tags("用户管理")]
        public async Task<ApiResponse> PutUserAsync(UserEntity newUser)
        {
            return await _service.PutResponseAsync(newUser);
        }

        // Delete api/Users/DeleteUser
        [HttpDelete]
        [Tags("用户管理")]
        public async Task<ApiResponse> DeleteUserAsync(int id)
        {
            return await _service.DeleteResponseAsync(id);
        }


        //登录 api/Users/LoginUserById
        [HttpGet]
        [Tags("用户登录")]
        public async Task<ApiResponse> LoginUserByIdAsync(int id, string password)
        {
            return await _service.LoginResponseAsync(id, password);
        }

        //登录 api/Users/LoginUserByUserName
        [HttpGet]
        [Tags("用户登录")]
        public async Task<ApiResponse> LoginUserByUsernameAsync(UserEntity user)
        {
            if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
                return await _service.LoginResponseAsync(user.UserName, user.Password);
            else
                return new ApiResponse("输入用户信息错误");
        }
        //api/Users/ChangePasswordByUserName
        [HttpPost]
        [Tags("用户资料修改")]
        public async Task<ApiResponse> ChangePasswordByUserNameAsync(string username, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
                return new ApiResponse("输入用户信息错误");
            return await _service.ChangePasswordByUserNameAsync(username, oldPassword,newPassword);
        }
    }
}
