using NoteApp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Services.Interfaces
{
    public interface IRestSharpService
    {
        RestResponse GetRestResponse(string serviceUrl);
        
        RestResponse GetRestResponse(string serviceUrl, Method method);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="serviceUrl">Api链接</param>
        /// <param name="id">用户编号</param>
        /// <param name="password">用户密码</param>
        /// <returns>ApiResponseR实体</returns>
        ApiResponseR GetApiResponse(string serviceUrl, string id, string password);


        ApiResponseR PostApiResponse(string serviceUrl, string username, string password, string telphone);

        ApiResponseR PutApiResponse(string serviceUrl, string username, string oldPassword, string newPassword);
    }
}
