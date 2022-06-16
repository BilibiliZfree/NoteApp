using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApp.Api.Data;
using NoteApp.Api.Models;
using NoteApp.Api.Services;
using NoteApp.Api.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteAppController : ControllerBase
    {
        private readonly NoteAppContext _appContext;

        public NoteAppController(NoteAppContext appContext)
        {
            _appContext = appContext;
        }



        // GET: api/<NoteAppController>
        [HttpGet]
        [Route("GetUsers")]
        public async Task<ApiResponse> GetUsersAsync()
        {
            var context = await _appContext.Users.ToListAsync();
            if (context is null || context.Count == 0)
                return new ApiResponse("数据库中并没有用户.");
            return new ApiResponse($"数据库中有{context.Count}个用户.", true, context);
        }

        // GET api/<NoteAppController>/5
        [HttpGet]
        [Route("GetUserById{id}")]
        public async Task<ApiResponse> GetUser(int id)
        {
            var user = await _appContext.Users.FindAsync(id);
            if (user == null) return new ApiResponse($"找不到序号为{id}的用户.");
            return new ApiResponse($"找到序号为{id}的用户{user.UserName}", true, user);
        }

        // POST api/<NoteAppController>
        [HttpPost]
        [Route("AddUser")]
        public async Task<ApiResponse> PostUserAsync(UserEntity newUser)
        {
            if (newUser == null)
                return new ApiResponse("传入用户不能为空.");
            _appContext.Users.Add(newUser);
            await _appContext.SaveChangesAsync();
            return new ApiResponse("添加用户成功！", true, newUser);
        }

        // PUT api/<NoteAppController>/5
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ApiResponse> PutUserAsync(UserEntity newUser)
        {
            if (newUser == null) return new ApiResponse("传入用户不能为空.");
            _appContext.Users.Update(newUser);
            await _appContext.SaveChangesAsync();
            return new ApiResponse("用户已成功修改！", true, newUser);
        }

        // DELETE api/<NoteAppController>/5
        [HttpDelete]
        [Route("DeleteUser{id}")]
        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var User = await _appContext.Users.FindAsync(id);
            if (User == null) return new ApiResponse($"找不到Id为{id}的用户.");
            _appContext.Users.Remove(User);
            await _appContext.SaveChangesAsync();
            return new ApiResponse("用户已删除！", true);
        }
    }
}
