using Newtonsoft.Json;
using NoteApp.Core;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services;
using NoteApp.Services.Interfaces;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class WriteBlogViewModel : RegionViewModelBase, IConfigureService, IRegionMemberLifetime
    {
        #region 字段

        private readonly IRestSharpServiceBase<ApiResponse, BlogEntity> _blogService;
        private readonly IRestSharpServiceBase<ApiResponse, BlogsRelation> _blogsRelation;

        private readonly IDialogService _dialogService;

        private readonly IRegionManager _regionManager;

        private readonly IRestSharpServiceBase<ApiResponse, UserEntity> _userService;

        private BlogEntity _Blog = new BlogEntity();

        private FlowDocument _BindFlowDocument;

        private ICollection<FontFamily> _FontFamilies = Fonts.SystemFontFamilies;

        private double[] _FontSize;

        private string _Message;

        private UserEntity _User = new UserEntity();

        #endregion

        #region 属性

        public DelegateCommand<string> DelegateCommand { get; private set; }

        public BlogEntity Blog
        {
            get { return _Blog; }
            set { SetProperty(ref _Blog, value); }
        }

        public FlowDocument BindFlowDocument
        {
            get { return _BindFlowDocument; }
            set { SetProperty(ref _BindFlowDocument, value); }
        }


        /// <summary>
        /// 系统字体列表
        /// </summary>
        public ICollection<FontFamily> FontFamilies
        {
            get { return _FontFamilies; }
            set { _FontFamilies = value; }
        }

        public double[] FontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
        }


        private ImageSource _ImageSource;

        public ImageSource ImageSource
        {
            get { return _ImageSource; }
            set { SetProperty(ref _ImageSource, value); }
        }

        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }


        public UserEntity User
        {
            get { return _User; }
            set { _User = value; }
        }

        public bool KeepAlive => false;

        public string Title => "新建博客页面";

        #endregion

        #region 函数

        public WriteBlogViewModel(IRegionManager regionManager, IDialogService dialogService,
            IRestSharpServiceBase<ApiResponse, UserEntity> userService,
            IRestSharpServiceBase<ApiResponse, BlogsRelation> blogsRelationService,
            IRestSharpServiceBase<ApiResponse, BlogEntity> blogService) : base(regionManager)
        {
            _blogService = blogService;
            _blogsRelation = blogsRelationService;
            _dialogService = dialogService;
            _regionManager = regionManager;
            _userService = userService;
            DelegateCommand = new DelegateCommand<string>(DelegateMethod);
            Configure();
        }

        public bool CanCloseDialog() => true;

        public void Configure()
        {
            User = AppSession.UserSession;
            Blog.Comment_Status = true;
            double[] doubles = new double[50];
            for (int i = 2; i < 50; i++)
            {
                doubles[i] = (double)(i * 2);
            }
            FontSize = doubles;
            
        }

        private async void DelegateMethod(string arg)
        {
            switch (arg)
            {
                case "GetPictureView":
                    ShowDialog(arg);
                    break;
                case "UploadBlog":
                    await UploadBlogAsync();
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 刷新图像
        /// </summary>
        /// <param name="url"></param>
        private void RefreshImage(string url)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(url, UriKind.Absolute);
            image.EndInit();
            ImageSource = image;
        }

        public void ShowDialog(string name)
        {
            //传入参数
            var keyValues = new DialogParameters();
            _dialogService.ShowDialog(name, keyValues,
                (callback) =>
                {
                    //获取对话返回的数据
                    var result = callback.Parameters;
                    if (result.GetValue<bool>("ReturnVar"))
                    {
                        Blog.PictrueLink = result.GetValue<string>("ReturnValue");
                        RefreshImage(Blog.PictrueLink);
                    }
                });
        }

        /// <summary>
        /// 上传博客
        /// </summary>
        private async Task UploadBlogAsync()
        {
            Blog.CreateTime = DateTime.Now;
            Blog.UpdateTime = DateTime.Now;
            if (string.IsNullOrWhiteSpace(Blog.Title))
            {
                Message = "请输入标题";
            }
            ApiResponse blogResponse = await _blogService.PostApiResponseAsync(Blog);
            if (blogResponse.Status)
            {
                Message = blogResponse.Message;
                string json = JsonConvert.SerializeObject(blogResponse.Object);
                BlogEntity blog = JsonConvert.DeserializeObject<BlogEntity>(json);
                BlogsRelation blogsRelation = new BlogsRelation { BlogID = blog.ID, UserID = User.ID };
                ApiResponse relationResponse = await _blogsRelation.PostApiResponseAsync(blogsRelation);
                if (relationResponse.Status)
                {
                    Message += $"\n\r{relationResponse.Message}";
                }
            }

            //MessageBox.Show(Blog.Context);
        }

        #endregion
    }
}
