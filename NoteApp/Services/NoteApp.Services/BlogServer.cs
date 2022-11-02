using Newtonsoft.Json;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Services
{
    public class BlogServer : IRestSharpServerBase<ApiResponse, BlogEntity>
    {
        private static RestClient client = new RestClient(StaticField.Api_Url);
        public async Task<ApiResponse> DeleteApiResponseAsync(int id)
        {
            RestRequest request = new RestRequest(StaticField.User_Delete, Method.Delete);
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
            RestRequest request = new RestRequest(StaticField.Blog_Gets);
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
            RestRequest request = new RestRequest(StaticField.Blog_GetById);
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
            RestRequest request = new RestRequest(StaticField.Blog_Search);
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

        public Task<ApiResponse> GetApiResponseAsync(int blogid, int userid)
        {
            throw new NotImplementedException();
        }

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

        public Task<ApiResponse> LoginAsync(int id, string password)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> LoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> PostApiResponseAsync(BlogEntity e)
        {
            RestRequest request = new RestRequest(StaticField.Blog_Add);
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

        public async Task<ApiResponse> PutApiResponseAsync(BlogEntity e)
        {
            RestRequest request = new RestRequest(StaticField.Blog_Modify);
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
    }
}
