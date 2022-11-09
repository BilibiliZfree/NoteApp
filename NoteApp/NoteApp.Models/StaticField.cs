using System;
using System.Collections.Generic;
using System.Text;

namespace NoteApp.Models
{
    public static class StaticField
    {
        #region WebBaseLink
        public const string LocalHost = "https://localhost:7082/";
        public const string Api_Url = "https://localhost:7082/Api/";
        #endregion
        #region Userlink
        public const string Api_User = "https://localhost:7082/Api/User/";
        public const string User_Delete = "User/DeleteUserById";
        public const string User_GetById = "User/GetUserById";
        public const string User_Gets = "User/GetUsers";
        public const string User_Search = "User/SearchUsers";
        public const string User_LoginById = "User/LoginByID";
        public const string User_LoginByUserName = "User/LoginByUserName";
        public const string User_Register = "User/RegisterUser";
        public const string User_Modify = "User/ModifyUser";
        #endregion


        #region Bloglink
        public const string Api_Blog = "https://localhost:7082/Api/Blog/";
        public const string Blog_Delete = "Blog/DeleteBlogById";
        public const string Blog_GetById = "Blog/GetBlogById";
        public const string Blog_Gets = "Blog/GetBlogs";
        public const string Blog_Search = "Blog/SearchBlogs";
        public const string Blog_Add = "Blog/AddBlog";
        public const string Blog_Modify = "Blog/ModifyBlog";
        #endregion

        #region BlogRelationlink
        public const string Api_BlogRelation = "https://localhost:7082/Api/Blog/";
        public const string BlogRelation_Delete = "BlogsRelation/DeleteBlogsRelationById";
        public const string BlogRelation_GetRelation = "BlogsRelation/GetBlogRelationResponse";
        public const string BlogRelation_GetRelations = "BlogsRelation/GetBlogsRelationResponse";
        public const string BlogRelation_Relate = "BlogsRelation/PostBlogsRelationResponse";
        #endregion

        #region 参数
        public const string Entity_Id = "id";
        public const string User_UserName = "username";
        public const string User_Password = "password";
        public const string Search_Data = "data";
        public const string Search_Key = "key";
        public const string UserId = "userid";
        public const string BlogId = "blogid";
        public const string Classification_Key = "Classification";
        #endregion
    }
}
