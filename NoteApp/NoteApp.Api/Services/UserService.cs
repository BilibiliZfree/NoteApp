using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NoteApp.Api.Data;
using NoteApp.Api.Services.Interfaces;
using NoteApp.Models;
using NoteApp.Services;
using RestSharp;

namespace NoteApp.Api.Services
{
    public class UserService : GenericInterface<ApiResponse, UserEntity>
    {
        private readonly NoteAppContext _noteAppContext;

        public UserService(NoteAppContext noteAppContext)
        {
            _noteAppContext = noteAppContext;
        }
        #region 主函数区

        /// <summary>
        /// 通过ID删除用户
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns><typeparamref name="ApiResponse"/></returns>
        public async Task<ApiResponse> DeleteResponseAsync(int id)
        {
            try
            {
                UserEntity user = await FindEntityByIdAsync(id);
                
                if (user != null)
                {
                    _noteAppContext.Users.Remove(user);
                    if (await _noteAppContext.SaveChangesAsync() >0)
                    {
                        return new ApiResponse("成功删除该用户", true);
                    }
                    else
                        throw new Exception("数据删除失败.");
                }
                else
                    throw new Exception($"搜索不到ID为{id}的用户.");
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-UserService-DeleteResponseAsync-{ex.Message}");
            }
        }

        /// <summary>
        /// 通过用户ID查找用户
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns><typeparamref name="ApiResponse"/></returns>
        public async Task<ApiResponse> GetResponseAsync(int id)
        {
            try
            {
                UserEntity user = await FindEntityByIdAsync(id);
                if (user != null)
                {
                    user.Password = null;
                    return new ApiResponse("成功搜索到用户", true, user);
                }
                else
                    throw new Exception($"搜索不到ID为{id}的用户.");
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-UserService-GetResponseAsync-{ex.Message}");
            }
        }

        public async Task<ApiResponse> GetsResponseAsync()
        {
            try
            {
                if (!_noteAppContext.Users.Any())
                    throw new Exception("数据库用并没有用户.");
                ICollection<UserEntity> users = await _noteAppContext.Users.ToListAsync();
                foreach (var user in users)
                {
                    user.Password = null;
                }
                return new ApiResponse($"数据库中有{users.Count}个用户.", true, users);
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-UserService-GetsResponseAsync-{ex.Message}");
            }
        }

        public async Task<ApiResponse> GetsResponseAsync(string arg, string key)
        {
            try
            {
                return new ApiResponse(true, await SearchEntityAsync(arg, key));
            }
            catch (Exception ex)
            {

                return new ApiResponse($"Api-UserService-GetsResponseAsync-{ex.Message}");
            }
            
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="e">用户UserEntity实体</param>
        /// <returns><typeparamref name="ApiResponse"/></returns>
        public async Task<ApiResponse> PostResponseAsync(UserEntity e)
        {
            try
            {
                if(MemberIsNotNull(e,256) && await DataIsNotDuplicateAsync(e,256) && MemberIsNotNull(e,3))
                {
                    e.Password = GetHashSHA256(e);
                    await _noteAppContext.Users.AddAsync(e);
                    if (await _noteAppContext.SaveChangesAsync() > 0)
                    {
                        e.Password = null;
                        return new ApiResponse("成功创建新用户.", true, e);
                    }
                    else
                        throw new Exception("保存数据失败.");
                }else
                    throw new Exception("数据可能为空或已被注册.");
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-UserService-PostResponseAsync-{ex.Message}.");
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="e">用户UserEntity实体</param>
        /// <returns><typeparamref name="ApiResponse"/></returns>
        public async Task<ApiResponse> PutResponseAsync(UserEntity e)
        {
            try
            {
                if (e is null)
                {
                    throw new Exception("传入用户为空!");
                }
                else if (MemberIsNotNull(e, 256))
                {
                    switch (await FindAnotherAsync(e))
                    {
                        case 1:
                            throw new Exception("用户名已被占用.");
                        case 2:
                            throw new Exception("手机号码已被占用.");
                        case 3:
                            throw new Exception("电子邮箱已被占用.");
                        default:
                            break;
                    }
                    
                    if (e.Password is not null)
                        e.Password = GetHashSHA256(e.Password);
                    _noteAppContext.Users.Update(e);
                    if (await _noteAppContext.SaveChangesAsync() > 0)
                    {
                        e.Password = null;
                        return new ApiResponse("用户信息已成功修改.", true, e);
                    }
                    else
                    {
                        throw new Exception("保存数据失败!");
                    }
                }
                else
                {
                    throw new Exception("修改用户失败!");
                }
            }
            catch (Exception ex)
            {

                return new ApiResponse($"Api-UserService-PutResponseAsync-{ex.Message}.");
            }
        }
        /// <summary>
        /// 使用账号密码登录
        /// </summary>
        /// <param name="id">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<ApiResponse> LoginAsync(int id, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    throw new Exception("输入的账号不能为空.");
                }
                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception("输入的密码不能为空.");
                }
                UserEntity user = await _noteAppContext.Users.FindAsync(id);
                if (user is not null)
                {
                    if (user.Password == GetHashSHA256(password))
                    {
                        user.Password = null;
                        return new ApiResponse("登录成功", true, user);
                    }
                    else
                        throw new Exception("登录失败.");
                }
                else
                    throw new Exception("登录失败.");
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-UserService-LoginAsync-{ex.Message}.");
                //throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 使用用户名密码登录
        /// </summary>
        /// <param name="id">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<ApiResponse> LoginAsync(string userName, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new Exception("输入的账号不能为空.");
                }
                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception("输入的密码不能为空.");
                }
                if (await FindAnyAsync(userName, "UserName"))
                {
                    UserEntity user = await _noteAppContext.Users.FirstAsync(o => o.UserName == userName);
                    if (user is not null)
                    {
                        if (user.Password == GetHashSHA256(password))
                        {
                            user.Password = null;
                            return new ApiResponse("登录成功", true, user);
                        }
                        else
                            throw new Exception("登录失败.");
                    }
                    else
                        throw new Exception("登录失败.");
                }
                else
                    throw new Exception("找不到该用户.");
                
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-UserService-LoginAsync-{ex.Message}.");
            }
        }

        #endregion

        #region 辅助方法区
        /// <summary>
        /// 明文加密
        /// </summary>
        /// <param name="s">密码</param>
        /// <returns>SHA256哈希码</returns>
        public string GetHashSHA256(string password) => new CryptogramService().HashSHA256(password);

        public string GetHashSHA256(UserEntity e)
        {
            if (e.Password != null)
            {
                return new CryptogramService().HashSHA256(e.Password);
            }
            else
                throw new Exception("GetHashSHA256-密码不能为空.");

        }

        public async Task<UserEntity> FindEntityByIdAsync(int id)
        {
            try
            {
                UserEntity? user = await _noteAppContext.Users.FindAsync(id);
                if (user is not null)
                    return user;
                else
                    throw new Exception($"FindEntityByIdAsync-找不到ID为{id}的用户.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns>找到不同用户返回true，否则返回false</returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> FindAnotherAsync(UserEntity e)
        {
            try
            {
                int flag = 0;
                if (await _noteAppContext.Users.Where(o => o.UserName == e.UserName && o.ID != e.ID).AnyAsync())
                {
                    flag = 1;
                }

                else if (await _noteAppContext.Users.Where(o => o.Telphone == e.Telphone && o.ID != e.ID).AnyAsync())
                {
                    flag = 2;
                }

                else if (await _noteAppContext.Users.Where(o => o.Mail == e.Mail && o.ID != e.ID).AnyAsync())
                {
                    flag = 3;
                }
                return flag;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> FindAnyAsync(object obj, string arg)
        {
            try
            {
                return arg switch
                {
                    "ID" => await _noteAppContext.Users.Where(o => o.ID == (int)obj).AnyAsync(),
                    "UserName" => await _noteAppContext.Users.Where(o => o.UserName == (string)obj).AnyAsync(),
                    "Birthday" => await _noteAppContext.Users.Where(o => o.Birthday == (DateTime)obj).AnyAsync(),
                    "Telphone" => await _noteAppContext.Users.Where(o => o.Telphone == (string)obj).AnyAsync(),
                    "Mail" => await _noteAppContext.Users.Where(o => o.Mail == (string)obj).AnyAsync(),
                    _ => false,
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool MemberIsNotNull(UserEntity e, int key)
        {
            switch (key)
            {
                case 3:
                    if (string.IsNullOrEmpty(e.Password))
                        throw new Exception("MemberIsNotNull-用户密码不能为空.");
                    return true;
                case 256:
                    if (string.IsNullOrEmpty(e.UserName))
                        throw new Exception("MemberIsNotNull-用户名不能为空.");
                    if (string.IsNullOrEmpty(e.Telphone))
                        throw new Exception("MemberIsNotNull-手机号码不能为空.");
                    if (string.IsNullOrEmpty(e.Mail))
                        throw new Exception("MemberIsNotNull-电子邮箱地址不能为空.");
                    return true;
                case 2356:
                    if (string.IsNullOrEmpty(e.UserName))
                        throw new Exception("MemberIsNotNull-用户名不能为空.");
                    if (string.IsNullOrEmpty(e.Password))
                        throw new Exception("MemberIsNotNull-用户密码不能为空.");
                    if (string.IsNullOrEmpty(e.Telphone))
                        throw new Exception("MemberIsNotNull-手机号码不能为空.");
                    if (string.IsNullOrEmpty(e.Mail))
                        throw new Exception("MemberIsNotNull-电子邮箱地址不能为空.");
                    return true;
                default:
                    return false;
            }
        }

        public async Task<bool> DataIsNotDuplicateAsync(UserEntity e, int arg)
        {
            if (e.UserName != null && e.Telphone != null && e.Mail != null)
            {
                switch (arg)
                {
                    case 2:
                        if (await FindAnyAsync(e.UserName, "UserName"))
                            throw new Exception("DataIsNotDuplicateAsync-用户名已被占用");
                        return true;
                    case 256:
                        if (await FindAnyAsync(e.UserName, "UserName"))
                            throw new Exception("DataIsNotDuplicateAsync-用户名已被占用");
                        if (await FindAnyAsync(e.Telphone, "Telphone"))
                            throw new Exception("DataIsNotDuplicateAsync-手机号码已被占用");
                        if (await FindAnyAsync(e.Mail, "Mail"))
                            throw new Exception("DataIsNotDuplicateAsync-电子邮箱地址已被占用");
                        return true;
                    default:
                        return false;
                }
            }
            else
                return false;

        }

        public async Task<ICollection<UserEntity>> SearchEntityAsync(string arg, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(arg))
                    throw new Exception($"传入{key}数据为空值.");
                switch (key)
                {
                    case "UserName":
                        ICollection<UserEntity> users_UserName = await _noteAppContext.Users.Where(o => o.UserName == arg).ToListAsync();
                        if (users_UserName.Count > 0)
                        {
                            foreach (var user in users_UserName)
                            {
                                user.Password = null;
                            }
                            return users_UserName;
                        }
                        else
                            throw new Exception($"找不到{key}为{arg}的数据.");
                    case "Birthday":
                        ICollection<UserEntity> users_Birthday = await _noteAppContext.Users.Where(o => o.Birthday.ToString() == arg).ToListAsync();
                        if (users_Birthday.Count > 0)
                        {
                            foreach (var user in users_Birthday)
                            {
                                user.Password = null;
                            }
                            return users_Birthday;
                        }
                        else
                            throw new Exception($"找不到{key}为{arg}的数据.");
                    case "Telphone":
                        ICollection<UserEntity> users_Telphone = await _noteAppContext.Users.Where(o => o.Telphone == arg).ToListAsync();
                        if (users_Telphone.Count > 0)
                        {
                            foreach (var user in users_Telphone)
                            {
                                user.Password = null;
                            }
                            return users_Telphone;
                        }
                        else
                            throw new Exception($"找不到{key}为{arg}的数据.");
                    case "Mail":
                        ICollection<UserEntity> users_Mail = await _noteAppContext.Users.Where(o => o.Mail == arg).ToListAsync();
                        if (users_Mail.Count > 0)
                        {
                            foreach (var user in users_Mail)
                            {
                                user.Password = null;
                            }
                            return users_Mail;
                        }
                        else
                            throw new Exception($"找不到{key}为{arg}的数据.");
                    default:
                        throw new Exception("获取数据失败.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"SearchEntityAsync-{ex.Message}");
            }
        }
        #endregion

    }
}
