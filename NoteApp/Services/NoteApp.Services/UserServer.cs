using Newtonsoft.Json;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Services
{
    public class UserServer : IRestSharpServerBase<ApiResponse, UserEntity>
    {
        private static RestClient client = new RestClient(StaticField.Api_Url);

        public async Task<ApiResponse> DeleteApiResponseAsync(int id)
        {
            RestRequest request = new RestRequest(StaticField.User_Delete,Method.Delete);
            request.AddParameter(StaticField.Entity_Id, id);
            RestResponse response = await client.DeleteAsync(request);
            if (response.IsSuccessful)
            {
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                return apiResponse;
            }
            else
            {
                return new ApiResponse(response.ErrorMessage);
            }
        }

        public async Task<ApiResponse> GetApiResponseAsync()
        {
            RestRequest request = new RestRequest(StaticField.User_Gets);
            RestResponse response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                return apiResponse;
            }
            else
            {
                return new ApiResponse(response.ErrorMessage);
            }

        }

        public async Task<ApiResponse> GetApiResponseAsync(int id)
        {
            RestRequest request = new RestRequest(StaticField.User_GetById);
            request.AddParameter(StaticField.Entity_Id, id);
            RestResponse response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                return apiResponse;
            }
            else
            {
                return new ApiResponse(response.ErrorMessage);
            }
        }

        public async Task<ApiResponse> GetApiResponseAsync(string data, string key)
        {
            RestRequest request = new RestRequest(StaticField.User_Search);
            request.AddParameter(StaticField.Search_Data, data);
            request.AddParameter(StaticField.Search_Key, key);
            RestResponse response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                return apiResponse;
            }
            else
            {
                return new ApiResponse(response.ErrorMessage);
            }
        }
        /// <summary>
        /// 空
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ApiResponse> GetApiResponseAsync(int blogid, int userid)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 空
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ApiResponse> GetApiResponseAsync(int data, string key)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 空
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ApiResponse> GetRestResponseAsync()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 空
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ApiResponse> GetRestResponseAsync(Method method)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApiResponse> LoginAsync(int id, string password)
        {
            RestRequest request = new RestRequest(StaticField.User_LoginById);
            request.AddParameter(StaticField.Entity_Id, id);
            request.AddParameter(StaticField.User_Password, password);
            RestResponse response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                return apiResponse;
            }
            else
            {
                return new ApiResponse(response.ErrorMessage);
            }
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApiResponse> LoginAsync(string username, string password)
        {
            RestRequest request = new RestRequest(StaticField.User_LoginByUserName);
            request.AddParameter(StaticField.User_UserName, username);
            request.AddParameter(StaticField.User_Password, password);
            RestResponse response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                return apiResponse;
            }
            else
            {
                return new ApiResponse(response.ErrorMessage);
            }
        }

        public async Task<ApiResponse> PostApiResponseAsync(UserEntity e)
        {
            RestRequest request = new RestRequest(StaticField.User_Register);
            request.AddJsonBody(JsonConvert.SerializeObject(e));
            RestResponse response = await client.PostAsync(request);
            if (response.IsSuccessful)
            {
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                return apiResponse;
            }
            else
            {
                return new ApiResponse(response.ErrorMessage);
            }
        }

        public async Task<ApiResponse> PutApiResponseAsync(UserEntity e)
        {
            RestRequest request = new RestRequest(StaticField.User_Modify);
            request.AddJsonBody(JsonConvert.SerializeObject(e));
            RestResponse response = await client.PutAsync(request);
            if (response.IsSuccessful)
            {
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                return apiResponse;
            }
            else
            {
                return new ApiResponse(response.ErrorMessage);
            }
        }
    }
}
