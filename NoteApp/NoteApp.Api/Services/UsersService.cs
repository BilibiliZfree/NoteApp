using NoteApp.Api.Data;
using NoteApp.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NoteApp.Services;
using NoteApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

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
            var Blogs = await _appContext.Blogs.ToListAsync();
            foreach (var item in Blogs)
            {
                if (item.UserEntityID == id)
                    User.Blogs.Add(item);
            }
            return new ApiResponse($"找到序号为{id}的用户{User.UserName}", true, User);
        }

        public async Task<ApiResponse> GetsResponseAsync()
        {
            var Users = await _appContext.Users.ToListAsync();
            var Blogs = await _appContext.Blogs.ToListAsync();
            if (Users is null || Users.Count == 0)
                return new ApiResponse("数据库中并没有用户.");
            foreach (var user in Users)
            {
                if(user != null)
                {
                    foreach (var blog in Blogs)
                    {
                        if (blog.UserEntityID == user.ID)
                        {
                            user.Blogs.Add(blog);
                        }
                    }
                }
            }
            return new ApiResponse($"数据库中有{Users.Count}个用户.{Blogs.Count}篇文章.", true, Users);
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task<ApiResponse> PostResponseAsync(UserEntity e)
        {
            try
            {
                if (e == null)
                    return new ApiResponse("传入用户不能为空.");
                //判断用户是否重复
                //if (_appContext.Users.Where(o => o.UserName == e.UserName).Count()>0)
                if (_appContext.Users.Where(o => o.UserName == e.UserName).Any())
                    return new ApiResponse("用户已经存在.");
                if (e.Password == null)
                    return new ApiResponse("输入密码不能为空.");
                if(e.Password != null)
                    e.Password = GetHashSHA256(e.Password);
                _appContext.Users.Add(e);
                if (await _appContext.SaveChangesAsync() > 0)
                    return new ApiResponse("添加用户成功！", true, e);
                return new ApiResponse("添加用户失败!");
            }
            catch (Exception ex)
            {
                return new ApiResponse("发生错误，添加用户失败!\r\n" + ex);
            }
        }

        /// <summary>
        /// 用户信息修改
        /// 还未添加验证
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task<ApiResponse> PutResponseAsync(UserEntity e)
        {
            if (e == null) return new ApiResponse("传入用户为空.");

            _appContext.Users.Update(e);
            if (await _appContext.SaveChangesAsync() > 0)
                return new ApiResponse("用户已成功修改.", true, e);
            else
                return new ApiResponse("修改用户信息失败.");
        }

        public async Task<ApiResponse> LoginResponseAsync(string username,string password)
        {
            if (string.IsNullOrEmpty(username)) return new ApiResponse("用户名为空!");
            if (string.IsNullOrEmpty(password)) return new ApiResponse("密码为空!");
            var User = await _appContext.Users.FirstOrDefaultAsync(o => o.UserName == username);
            if (User == null)
                return new ApiResponse("找不到该用户.");
            if (User.Password == GetHashSHA256(password))
            {
                User.Password = null;
                return new ApiResponse("登录成功", true, User);
            }
            return new ApiResponse("密码错误.");
        }

        public async Task<ApiResponse> LoginResponseAsync(int id, string password)
        {
            if (string.IsNullOrEmpty(password)) return new ApiResponse("密码为空!");
            var User = await _appContext.Users.FindAsync(id);
            if (User == null)
                return new ApiResponse("找不到该用户.");
            if (User.Password == GetHashSHA256(password))
            {
                User.Password = null;
                return new ApiResponse("登录成功", true, User);
            }
            return new ApiResponse("密码错误.");
        }

        public async Task<ApiResponse> ChangePasswordByUserNameAsync(string username,string oldPassword,string newPassword)
        {
            if (string.IsNullOrEmpty(username)) return new ApiResponse("用户名为空!");
            if (string.IsNullOrEmpty(oldPassword)) return new ApiResponse("旧密码为空!");
            if (string.IsNullOrEmpty(newPassword)) return new ApiResponse("新密码为空!");
            var User = await _appContext.Users.FirstOrDefaultAsync(o => o.UserName == username);
            if (User == null)
                return new ApiResponse("找不到该用户.");
            if (User.Password == GetHashSHA256(oldPassword))
            {
                User.Password = GetHashSHA256(newPassword);
                _appContext.Users.Update(User);
                if (await _appContext.SaveChangesAsync() > 0)
                    return new ApiResponse("用户已成功修改.", true);
            }
            return new ApiResponse("旧密码错误，修改密码失败.");
        }

        public string GetHashSHA256(string s)
        {
            return new CryptogramService().HashSHA256(s);
        }

        public Task<ApiResponse> GetsResponseAsync(string arg)
        {
            throw new NotImplementedException();
        }
    }
}
