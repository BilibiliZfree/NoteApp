using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using NoteApp.Services;
using Prism.Regions;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class ViewDViewModel : RegionViewModelBase
    {
        private readonly IRestSharpService _restSharpService;

        private string myVar;

        public string MyProperty
        {
            get { return myVar; }
            set { SetProperty(ref myVar, value); }
        }

        private string youVar;

        public string YouProperty
        {
            get { return youVar; }
            set { SetProperty(ref youVar,value); }
        }


        public ViewDViewModel(IRegionManager regionManager, IRestSharpService restSharpService) : 
            base(regionManager)
        {
            _restSharpService = restSharpService;
            ApiResponseR apiResponse = _restSharpService.GetApiResponse("https://localhost:7082/api/Users/LoginUserById","1","string");
            //https://localhost:7082/api/Blogs/GetBlogs
            //https://localhost:7082/api/Users/LoginUserById?id=1&password=string
            ////默认使用Get
            //RestRequest restRequest = new RestRequest() { Method = Method.Get };
            //RestResponse apiBaseRequest = client.Get(restRequest);
            MyProperty = apiResponse.Object.UserName;
            YouProperty = apiResponse.Message;
        }


        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do domething
        }
    }
}
