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

        ApiResponseR GetApiResponse(string serviceUrl, string id, string password);
    }
}
