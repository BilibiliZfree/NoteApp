using Newtonsoft.Json;
using NoteApp.Core;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class BlogsListViewModel : RegionViewModelBase,IRegionMemberLifetime,IConfigureService
    {
        #region 字段

        private readonly IRestSharpServiceBase<ApiResponse, BlogEntity> _blogService;

        private readonly IRestSharpServiceBase<ApiResponse, BlogsRelation> _blogsRelationService;

        private readonly IRegionManager _regionManager;

        private ICollection<BlogEntity> _BlogsList;

        private ICollection<BlogEntity> _Temp_BlogsList;

        #endregion

        #region 属性

        public DelegateCommand<string> DelegateCommand { get; private set; }

        public bool KeepAlive => false;

        public ICollection<BlogEntity> BlogsList
        {
            get { return _BlogsList; }
            set { SetProperty(ref _BlogsList, value); }
        }

        public ICollection<BlogEntity> Temp_BlogsList
        {
            get { return _Temp_BlogsList; }
            set { SetProperty(ref _Temp_BlogsList, value); }
        }

        private UserEntity _User;

        public UserEntity User
        {
            get { return _User; }
            set { _User = value; }
        }


        #endregion

        #region 函数

        public BlogsListViewModel(IRegionManager regionManager,IRestSharpServiceBase<ApiResponse,BlogEntity> blogService,
            IRestSharpServiceBase<ApiResponse, BlogsRelation> blogRelationService) : base(regionManager)
        {
            _regionManager = regionManager;
            _blogService = blogService;
            _blogsRelationService = blogRelationService;
            DelegateCommand = new DelegateCommand<string>(DelegateMethod);
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
                if (item.Title != null)
                {
                    if (item.Title.Length > 25)
                    {
                        item.Title = item.Title.Substring(0, 25);
                    }
                }
                //if (item.Context != null && item.Context.Length > 100)
                if (item.Context != null)
                {
                    if (item.Context.StartsWith("<FlowDocument") && item.Context.EndsWith("</FlowDocument>"))
                    {
                        FlowDocument flow = (FlowDocument)XamlReader.Parse(item.Context);
                        TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
                        item.Context = range.Text;
                    }
                    if (item.Context.Length > 200)
                    {
                        item.Context = item.Context.Substring(0,200);
                    }
                }
            }
            return e;
        }

        private void DelegateMethod(string obj)
        {
            switch (obj)
            {
                case "全部":
                    GetAllBlogsAsync();
                    break;
                case "个人":
                    GetMyBlogsAsync();
                    break;
                case "CSharp": SortByData(obj); break;
                case "WPF": SortByData(obj); break;
                case "Web": SortByData(obj); break;
                case "Unity": SortByData(obj); break;
                case "数据库": SortByData(obj); break;
                case "按发布时间升序": SortByData(obj); break;
                case "按发布时间降序": SortByData(obj); break;
                case "按修改时间升序": SortByData(obj); break;
                case "按修改时间降序": SortByData(obj); break;
                default:
                    break;
            }
        }

        private async void GetAllBlogsAsync()
        {
            ApiResponse response = await _blogService.GetApiResponseAsync();
            if (response.Status)
            {
                string json = JsonConvert.SerializeObject(response.Object);
                BlogsList = CorrectBlogs(JsonConvert.DeserializeObject<ICollection<BlogEntity>>(json));
                Temp_BlogsList = BlogsList;
            }
        }

        private async void GetMyBlogsAsync()
        {
            ApiResponse response = await _blogsRelationService.GetApiResponseAsync(AppSession.user.ID, StaticField.UserId);
            string relationList = JsonConvert.SerializeObject(response.Object);
            ICollection<BlogsRelation> relations = JsonConvert.DeserializeObject<ICollection<BlogsRelation>>(relationList);
            ICollection<BlogEntity> blogs = new List<BlogEntity> (){ };
            foreach (var item in relations)
            {
                ApiResponse response1 = await _blogService.GetApiResponseAsync(item.BlogID);
                string json = JsonConvert.SerializeObject(response1.Object);
                BlogEntity blog = JsonConvert.DeserializeObject<BlogEntity>(json);
                blogs.Add(blog);
            }
            //ApiResponse response = await _blogService.GetApiResponseAsync(User.ID);
            BlogsList = CorrectBlogs(blogs);
            Temp_BlogsList = blogs;
        }

        private async void GetBlogByClassification(string data)
        {
            ApiResponse response = await _blogService.GetApiResponseAsync(data,StaticField.Classification_Key);
            if (response.Status)
            {
                string json = JsonConvert.SerializeObject(response.Object);
                BlogsList = CorrectBlogs(JsonConvert.DeserializeObject<ICollection<BlogEntity>>(json));
            }
            
        }

        private void SortByData(string data)
        {
            
            switch (data)
            {
                case "全部": BlogsList = Temp_BlogsList; break;
                case "CSharp": ICollection<BlogEntity> entities0 = Temp_BlogsList; BlogsList = entities0.Where(e => e.Classification == data).ToList(); break;
                case "WPF": ICollection<BlogEntity> entities1 = Temp_BlogsList; BlogsList = entities1.Where(e => e.Classification == data).ToList(); break;
                case "Web": ICollection<BlogEntity> entities2 = Temp_BlogsList; BlogsList = entities2.Where(e => e.Classification == data).ToList(); break;
                case "Unity": ICollection<BlogEntity> entities3 = Temp_BlogsList; BlogsList = entities3.Where(e => e.Classification == data).ToList(); break;
                case "数据库": ICollection<BlogEntity> entities4 = Temp_BlogsList; BlogsList = entities4.Where(e => e.Classification == data).ToList(); break;
                
                case "按发布时间升序": ICollection<BlogEntity> entities5 = BlogsList; BlogsList = entities5.OrderBy(e => e.CreateTime).ToList(); break;
                case "按发布时间降序": ICollection<BlogEntity> entities6 = BlogsList; BlogsList = entities6.OrderByDescending(e => e.CreateTime).ToList(); break;
                case "按修改时间升序": ICollection<BlogEntity> entities7 = BlogsList; BlogsList = entities7.OrderBy(e => e.UpdateTime).ToList(); break;
                case "按修改时间降序": ICollection<BlogEntity> entities8 = BlogsList; BlogsList = entities8.OrderByDescending(e => e.UpdateTime).ToList(); break;
                
                default:
                    break;
            }
        }

        #endregion

    }
}
