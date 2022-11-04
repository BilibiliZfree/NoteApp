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
    public class BlogsRelationServer : IRestSharpServiceBase<ApiResponse, BlogsRelation>
    {
        private static RestClient client = new RestClient(StaticField.Api_Url);
        public async Task<ApiResponse> DeleteApiResponseAsync(int id)
        {
            RestRequest request = new RestRequest(StaticField.BlogRelation_Delete, Method.Delete);
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
        /// <summary>
        /// 空
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ApiResponse> GetApiResponseAsync()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 空
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ApiResponse> GetApiResponseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> GetApiResponseAsync(int blogid, int userid)
        {
            RestRequest request = new RestRequest(StaticField.BlogRelation_GetRelation);
            request.AddParameter(StaticField.BlogId, blogid);
            request.AddParameter(StaticField.UserId, userid);
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

        public async Task<ApiResponse> GetApiResponseAsync(int data, string key)
        {
            RestRequest request = new RestRequest(StaticField.BlogRelation_GetRelations);
            request.AddParameter(StaticField.Search_Data, data);
            request.AddParameter(StaticField.Search_Key, key);
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
        /// <summary>
        /// 空
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ApiResponse> GetApiResponseAsync(string data, string key)
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

        public async Task<ApiResponse> PostApiResponseAsync(BlogsRelation e)
        {
            RestRequest request = new RestRequest(StaticField.BlogRelation_Relate);
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
        /// <summary>
        /// 空
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ApiResponse> PutApiResponseAsync(BlogsRelation e)
        {
            throw new NotImplementedException();
        }
    }
}
