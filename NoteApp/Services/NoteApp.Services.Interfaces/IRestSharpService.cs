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
        ApiResponseU GetApiResponse(string serviceUrl, string id, string password);
        /// <summary>
        /// 异步获取用户信息
        /// </summary>
        /// <param name="serviceUrl">Api链接</param>
        /// <param name="id">用户编号</param>
        /// <param name="password">用户密码</param>
        /// <returns>ApiResponseR实体</returns>
        Task<ApiResponseU> GetApiResponseAsync(string serviceUrl, string id, string password);

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="serviceUrl">注册链接API</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="telphone">手机号码</param>
        /// <returns></returns>
        ApiResponseU PostApiResponse(string serviceUrl, string username, string password, string telphone);

        ApiResponseU PutApiResponse(string serviceUrl, string username, string oldPassword, string newPassword);

        /// <summary>
        /// 获取数据库中博客
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        Task<ApiResponseB> GetAllBlogsAsync(string serviceUrl, string arg);
        /// <summary>
        /// 获取用户发表的博客
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApiResponseB> GetMyBlogsAsync(string serviceUrl, int userId);


    }
}
