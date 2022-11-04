using Newtonsoft.Json;
using NoteApp.Core;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class BlogsListViewModel : RegionViewModelBase,IRegionMemberLifetime,IConfigureService
    {
        #region 字段

        private readonly IRestSharpServiceBase<ApiResponse, BlogEntity> _blogService;

        private readonly IRegionManager _regionManager;

        private ICollection<BlogEntity> _BlogsList;

        #endregion

        #region 属性

        public DelegateCommand<object> DelegateCommand { get; private set; }

        public bool KeepAlive => false;

        public ICollection<BlogEntity> BlogsList
        {
            get { return _BlogsList; }
            set { SetProperty(ref _BlogsList, value); }
        }

        private UserEntity _User;

        public UserEntity User
        {
            get { return _User; }
            set { _User = value; }
        }


        #endregion

        #region 函数

        public BlogsListViewModel(IRegionManager regionManager,IRestSharpServiceBase<ApiResponse,BlogEntity> blogService) : base(regionManager)
        {
            _regionManager = regionManager;
            _blogService = blogService;
            DelegateCommand = new DelegateCommand<object>(DelegateMethod);
            Configure();
        }

        public void Configure()
        {
            User = AppSession.user;
             GetAllBlogsAsync();
        }

        public ICollection<BlogEntity> CorrectBlogs(ICollection<BlogEntity> e)
        {
            foreach (var item in e)
            {
                if (item.Title != null && item.Title.Length > 25)
                {
                    FlowDocument flow = (FlowDocument)XamlReader.Parse(item.Title);
                    TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
                    item.Title = range.Text;
                }
                if (item.Context != null && item.Context.Length > 100)
                {
                    FlowDocument flow = (FlowDocument)XamlReader.Parse(item.Context);
                    TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
                    item.Context = range.Text;
                }
            }
            return e;
        }

        private void DelegateMethod(object obj)
        {
            var item = obj as ListBoxItem;
            switch (item.Content.ToString())
            {
                case "全部":
                    GetAllBlogsAsync();
                    break;
                case "个人":
                    break;
                default:
                    break;
            }
        }

        private async void GetAllBlogsAsync()
        {
            ApiResponse response = await _blogService.GetApiResponseAsync();
            string json = JsonConvert.SerializeObject(response.Object);
            BlogsList = CorrectBlogs(JsonConvert.DeserializeObject<ICollection<BlogEntity>>(json));
            //BlogsList = JsonConvert.DeserializeObject<ICollection<BlogEntity>>(json);
        }

        #endregion

    }
}
