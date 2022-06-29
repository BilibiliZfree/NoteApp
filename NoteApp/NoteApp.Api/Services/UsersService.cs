using NoteApp.Api.Data;
using NoteApp.Api.Models;
using NoteApp.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace NoteApp.Api.Services
{
    public class UsersService : GenericInterface<ApiResponse, UserEntity>
    {
        public readonly NoteAppContext _appContext;

        public UsersService(NoteAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<ApiResponse> DeleteResponseAsync(int id)
        {
            var User = await _appContext.Users.FindAsync(id);
            if (User == null) return new ApiResponse($"找不到Id为{id}的用户.");
            _appContext.Users.Remove(User);
            await _appContext.SaveChangesAsync();
            return new ApiResponse("用户已删除！", true);
        }

        public async Task<ApiResponse> GetResponseAsync(int id)
        {
            var User = await _appContext.Users.FindAsync(id);
            if (User == null) return new ApiResponse($"找不到序号为{id}的用户.");
            return new ApiResponse($"找到序号为{id}的用户{User.UserName}", true, User);
        }

        public async Task<ApiResponse> GetsResponseAsync()
        {
            var context = await _appContext.Users.ToListAsync();
            if (context is null || context.Count == 0)
                return new ApiResponse("数据库中并没有用户.");
            return new ApiResponse($"数据库中有{context.Count}个用户.", true, context);
        }

        public async Task<ApiResponse> PostResponseAsync(UserEntity e)
        {
            try
            {
                if (e == null)
                    return new ApiResponse("传入用户不能为空.");
                _appContext.Users.Add(e);
                if (await _appContext.SaveChangesAsync() > 0)
                    return new ApiResponse("添加用户成功！", true, e);
                return new ApiResponse("添加用户失败！");
            }
            catch (Exception)
            {

                return new ApiResponse("发生错误，添加用户失败");
            }
        }

        public async Task<ApiResponse> PutResponseAsync(UserEntity e)
        {
            if (e == null) return new ApiResponse("传入用户不能为空.");
            _appContext.Users.Update(e);
            await _appContext.SaveChangesAsync();
            return new ApiResponse("用户已成功修改！", true, e);
        }
    }
}
