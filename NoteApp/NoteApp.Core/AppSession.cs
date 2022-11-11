using NoteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core
{
    public static class AppSession
    {
        public static UserEntity UserSession { get; private set; }

        public static BlogEntity BlogSeesion { get; private set; }

        public static UserEntity UserSessionMethod(UserEntity updateUser) => UserSession = updateUser;

        public static BlogEntity BlogSessionMethod(BlogEntity updateBlog) => BlogSeesion = updateBlog;
    }
}
