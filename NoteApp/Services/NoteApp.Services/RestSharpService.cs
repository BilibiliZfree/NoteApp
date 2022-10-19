using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using NoteApp.Models;
using System.Threading.Tasks;
using NoteApp.Services.Interfaces;
using Newtonsoft.Json;

namespace NoteApp.Services
{
    public class RestSharpService : IRestSharpService
    {
        public RestResponse GetRestResponse(string serviceUrl) 
            => new RestClient(serviceUrl).Get(new RestRequest());

        public RestResponse GetRestResponse(string serviceUrl, Method method = Method.Get) 
            => new RestClient(serviceUrl).Get(new RestRequest() { Method = method });

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="serviceUrl">Api链接</param>
    /// <param name="id">用户编号</param>
    /// <param name="password">用户密码</param>
    /// <returns>ApiResponseR实体</returns>
        public ApiResponseR GetApiResponse(string serviceUrl, string id, string password)
        {
            try
            {
                var client = new RestClient(serviceUrl + $"?id={id}&password={password}");
                RestRequest restRequest = new RestRequest();
                RestResponse restResponse = client.Get(restRequest);
                ApiResponseR apiResponse = JsonConvert.DeserializeObject<ApiResponseR>(restResponse.Content);
                return apiResponse;
            }
            catch (Exception ex)
            {

                return new ApiResponseR() { Message = ex.Message };
            }
            
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="serviceUrl">注册链接API</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="telphone">手机号码</param>
        /// <returns></returns>
        public ApiResponseR PostApiResponse(string serviceUrl, string username, string password,string telphone)
        {
            try
            {
                UserEntity ue = new UserEntity
                {
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    UserName = username,
                    Password = password,
                    Telphone = telphone,
                    Blogs = null
                };
                var client = new RestClient(serviceUrl);
                RestRequest restRequest = new RestRequest();
                restRequest.AddJsonBody(ue);
                RestResponse restResponse = client.Post(restRequest);
                ApiResponseR apiResponse = JsonConvert.DeserializeObject<ApiResponseR>(restResponse.Content);
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponseR() { Message = ex.Message };
            }
           
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="username"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public ApiResponseR PutApiResponse(string serviceUrl, string username, string oldPassword, string newPassword)
        {
            try
            {
                var client = new RestClient(serviceUrl + $"?username={username}&oldPassword={oldPassword}&newPassword={newPassword}");
                RestRequest restRequest = new RestRequest();
                RestResponse restResponse = client.Post(restRequest);
                ApiResponseR apiResponse = JsonConvert.DeserializeObject<ApiResponseR>(restResponse.Content);
                return apiResponse;
            }
            catch (Exception ex)
            {

                return new ApiResponseR() { Message = ex.Message };
            }


            return new ApiResponseR();
        }
    }
}
