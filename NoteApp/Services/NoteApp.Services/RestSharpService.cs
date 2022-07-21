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

        public ApiResponseR GetApiResponse(string serviceUrl, string id, string password)
        {
            var client = new RestClient(serviceUrl+ $"?id={id}&password={password}");
            RestRequest restRequest = new RestRequest();
            RestResponse restResponse = client.Get(restRequest);
            ApiResponseR apiResponse = JsonConvert.DeserializeObject<ApiResponseR>(restResponse.Content);

            return apiResponse;
        }
    }
}
