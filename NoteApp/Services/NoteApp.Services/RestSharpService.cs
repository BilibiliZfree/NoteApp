using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using NoteApp.Models;
using System.Threading.Tasks;
using NoteApp.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace NoteApp.Services
{
    public class RestSharpService : IRestSharpService
    {
        public RestResponse GetRestResponse(string serviceUrl) 
            => new RestClient(serviceUrl).Get(new RestRequest());

        public RestResponse GetRestResponse(string serviceUrl, Method method = Method.Get) 
            => new RestClient(serviceUrl).Get(new RestRequest() { Method = method });


        public ApiResponseU GetApiResponse(string serviceUrl, string id, string password)
        {
            try
            {
                var client = new RestClient(serviceUrl + $"?id={id}&password={password}");
                RestRequest restRequest = new RestRequest();
                RestResponse restResponse = client.Get(restRequest);
                ApiResponseU apiResponse = JsonConvert.DeserializeObject<ApiResponseU>(restResponse.Content);
                return apiResponse;
            }
            catch (Exception ex)
            {

                return new ApiResponseU() { Message = ex.Message };
            }
            
        }

        public async Task<ApiResponseU> GetApiResponseAsync(string serviceUrl, string id, string password)
        {
            try
            {
                var client = new RestClient(serviceUrl + $"?id={id}&password={password}");
                RestRequest restRequest = new RestRequest();
                RestResponse restResponse = await client.GetAsync(restRequest);
                ApiResponseU apiResponse = JsonConvert.DeserializeObject<ApiResponseU>(restResponse.Content);
                return apiResponse;
            }
            catch (Exception ex)
            {

                return new ApiResponseU() { Message = ex.Message };
            }
        }



        public ApiResponseU PostApiResponse(string serviceUrl, string username, string password,string telphone)
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
                ApiResponseU apiResponse = JsonConvert.DeserializeObject<ApiResponseU>(restResponse.Content);
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponseU() { Message = ex.Message };
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
        public ApiResponseU PutApiResponse(string serviceUrl, string username, string oldPassword, string newPassword)
        {
            try
            {
                var client = new RestClient(serviceUrl + $"?username={username}&oldPassword={oldPassword}&newPassword={newPassword}");
                RestRequest restRequest = new RestRequest();
                RestResponse restResponse = client.Post(restRequest);
                ApiResponseU apiResponse = JsonConvert.DeserializeObject<ApiResponseU>(restResponse.Content);
                return apiResponse;
            }
            catch (Exception ex)
            {

                return new ApiResponseU() { Message = ex.Message };
            }
        }

        public async Task<ApiResponseB> GetAllBlogsAsync(string serviceUrl,string arg)
        {
            try
            {
                var client = new RestClient(serviceUrl + $"?arg={arg}");
                RestRequest restRequest = new RestRequest();
                RestResponse restResponse = await client.GetAsync(restRequest);
                ApiResponseB apiResponse = JsonConvert.DeserializeObject<ApiResponseB>(restResponse.Content);
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponseB() { Message = ex.Message };
            }
        }

        public async Task<ApiResponseB> GetMyBlogsAsync(string serviceUrl, int userId)
        {
            try
            {
                var client = new RestClient(serviceUrl + $"?id={userId}");
                RestRequest restRequest = new RestRequest();
                RestResponse restResponse = await client.GetAsync(restRequest);
                ApiResponseB apiResponse = JsonConvert.DeserializeObject<ApiResponseB>(restResponse.Content);
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponseB() { Message = ex.Message };
            }
        }
    }
}
