using Microsoft.AspNetCore.Mvc;
//using NoteApp.Api.Data;
using NoteApp.Api.Models;
using NoteApp.Api.Services;
using NoteApp.Api.Services.Interfaces;

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
        
    }
}
