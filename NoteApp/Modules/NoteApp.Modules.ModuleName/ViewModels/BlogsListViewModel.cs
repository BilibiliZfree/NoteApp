using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NoteApp.Core;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using Prism.Commands;
using Prism.Common;
using Prism.Regions;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class BlogsListViewModel : RegionViewModelBase, IRegionMemberLifetime
    {
        #region 字段
        public readonly IRestSharpService _restSharpService;

        public DelegateCommand<object> SelectChangedCommandA { get; private set; }
        public DelegateCommand<object> SelectChangedCommandC { get; private set; }

        #endregion

        #region 属性

        private UserEntity _userEntity;
        /// <summary>
        /// 用户信息临时储存
        /// </summary>
        public UserEntity UserEntity
        {
            get { return _userEntity; }
            set { SetProperty(ref _userEntity, value); }
        }

        private ObservableCollection<BlogEntity> _allBlogs;
        /// <summary>
        /// 所有博客信息临时储存
        /// </summary>
        public ObservableCollection<BlogEntity> AllBlogs
        {
            get { return _allBlogs; }
            set { SetProperty(ref _allBlogs, value); }
        }

        public bool KeepAlive
        {
            get
            {
                return true;
            }
        }





        #endregion

        #region 方法
        public BlogsListViewModel(IRegionManager regionManager, IRestSharpService restSharpService) : base(regionManager)
        {
            _restSharpService = restSharpService;
            UserEntity = AppSession.user;
            GetAllBlogsAsync();
            SelectChangedCommandA = new DelegateCommand<object>(GetAllOrMyBlogsAsync);
            SelectChangedCommandC = new DelegateCommand<object>(GetAllBlogsByTimeAsync);
        }

        public async void GetAllBlogsAsync()
        {
            ApiResponseB apiResponse = await _restSharpService.GetAllBlogsAsync(WebApiUrl.GetAllBlogsUrl,WebApiUrl.GetAllBlogs);
            AllBlogs = apiResponse.Object;
        }
        public async void GetMyBlogsAsync()
        {
            ApiResponseB apiResponse = await _restSharpService.GetMyBlogsAsync(WebApiUrl.GetAllBlogsUrl, UserEntity.ID);
            AllBlogs = apiResponse.Object;
        }

        private async void GetAllOrMyBlogsAsync(object arg)
        {
            switch (arg.ToString())
            {
                case "0":
                    ApiResponseB apiResponse0 = await _restSharpService.GetAllBlogsAsync(WebApiUrl.GetAllBlogsUrl, WebApiUrl.GetAllBlogs);
                    AllBlogs = apiResponse0.Object;
                    break;
                case "1":
                    ApiResponseB apiResponse1 = await _restSharpService.GetMyBlogsAsync(WebApiUrl.GetMyBlogsUrl, UserEntity.ID);
                    AllBlogs = apiResponse1.Object;
                    break;
                default:
                    break;
            }
        }

        private async void GetAllBlogsByTimeAsync(object arg)
        {
            switch (arg.ToString())
            {
                case "0":
                    ApiResponseB apiResponse0 = await _restSharpService.GetAllBlogsAsync(WebApiUrl.GetAllBlogsUrl, WebApiUrl.GetAllBlogs);
                    AllBlogs = apiResponse0.Object;
                    break;
                case "1":
                    ApiResponseB apiResponse1 = await _restSharpService.GetAllBlogsAsync(WebApiUrl.GetAllBlogsUrl, WebApiUrl.GetBlogsSortByCreateTimeAsc);
                    AllBlogs = apiResponse1.Object;
                    break;
                case "2":
                    ApiResponseB apiResponse2 = await _restSharpService.GetAllBlogsAsync(WebApiUrl.GetAllBlogsUrl, WebApiUrl.GetBlogsSortByCreateTimeDesc);
                    AllBlogs = apiResponse2.Object;
                    break;
                case "3":
                    ApiResponseB apiResponse3 = await _restSharpService.GetAllBlogsAsync(WebApiUrl.GetAllBlogsUrl, WebApiUrl.GetBlogsSortByUpdateTimeAsc);
                    AllBlogs = apiResponse3.Object;
                    break;
                case "4":
                    ApiResponseB apiResponse4 = await _restSharpService.GetAllBlogsAsync(WebApiUrl.GetAllBlogsUrl, WebApiUrl.GetBlogsSortByUpdateTimeDesc);
                    AllBlogs = apiResponse4.Object;
                    break;
                default:
                    break;

            }
        }


        #endregion





    }
}
