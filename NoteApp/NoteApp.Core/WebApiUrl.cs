using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core
{
    public static class WebApiUrl
    {
        #region WebBaseLink
        public const string LocalHost = "https://localhost:7082/";
        public const string Api_Url = "https://localhost:7082/Api";
        #endregion
        #region Userlink
        public const string Api_User = "https://localhost:7082/Api/User";
        public const string User_Delete = "https://localhost:7082/api/User/DeleteUserById?id=1";
        public const string User_GetById = "https://localhost:7082/api/User/GetUserById?id=1";
        public const string User_Gets = "https://localhost:7082/api/User/GetUsers";
        public const string User_Search = "https://localhost:7082/api/User/SearchUsers?data=admin&key=UserName";
        public const string User_LoginById = "https://localhost:7082/api/User/LoginByID?id=1&password=1";
        public const string User_LoginByUserName = "https://localhost:7082/api/User/LoginByUserName?userName=admin&password=admin";
        public const string User_Register = "https://localhost:7082/api/User/RegisterUser";
        public const string User_Modify = "https://localhost:7082/api/User/ModifyUser";
        #endregion

        #region Bloglink

        #endregion

        #region BlogRelationlink

        #endregion
    }
}
