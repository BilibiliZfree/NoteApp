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
    }
}
