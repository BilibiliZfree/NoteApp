using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core
{
    public static class WebApiUrl
    {
        /// <summary>
        /// 使用用户ID登录链接
        /// </summary>
        public const string LoginUserByIdUrl = "https://localhost:7082/api/Users/LoginUserById";
        /// <summary>
        /// 注册用户链接
        /// </summary>
        public const string RegisterUserUrl = "https://localhost:7082/api/Users/PostUser";
        /// <summary>
        /// 修改用户链接
        /// </summary>
        public const string ChangeUserUrl = "https://localhost:7082/api/Users/ChangePasswordByUserName";
        /// <summary>
        /// 获取所有博客链接
        /// </summary>
        public const string GetAllBlogsUrl = "https://localhost:7082/api/Blogs/GetAllBlogs";
        public const string GetAllBlogs = "GetAllBlogs";
        public const string GetBlogsSortByCreateTimeAsc = "GetBlogsSortByCreateTimeAsc";
        public const string GetBlogsSortByCreateTimeDesc = "GetBlogsSortByCreateTimeDesc";
        public const string GetBlogsSortByUpdateTimeAsc = "GetBlogsSortByUpdateTimeAsc";
        public const string GetBlogsSortByUpdateTimeDesc = "GetBlogsSortByUpdateTimeDesc";
        /// <summary>
        /// 获取个人博客链接
        /// </summary>
        public const string GetMyBlogsUrl = "https://localhost:7082/api/Blogs/GetMyBlogs";
    }
}
