using NoteApp.Core;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class WriteBlogViewModel : RegionViewModelBase,IConfigureService,IRegionMemberLifetime
    {
        #region 字段

        private readonly IDialogService _dialogService;

        private readonly IRegionManager _regionManager;

        private BlogEntity _Blog = new BlogEntity();

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

        public WriteBlogViewModel(IRegionManager regionManager, IDialogService dialogService) : base(regionManager)
        {
            _dialogService = dialogService;
            _regionManager = regionManager;
            DelegateCommand = new DelegateCommand<string>(DelegateMethod);
            Configure();
        }

        public bool CanCloseDialog() => true;

        public void Configure()
        {
            User = AppSession.user;
            //Blog.PictrueLink = "https://i0.hdslb.com/bfs/article/6290dad3bec727f569ff7fc69d2aa15b8a2d81fc.jpg";
        }

        private void DelegateMethod(string arg)
        {
            switch (arg)
            {
                case "GetPictureView":
                    ShowDialog(arg);
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
                    if(result.GetValue<bool>("ReturnVar"))
                    {
                        Blog.PictrueLink = result.GetValue<string>("ReturnValue");
                        RefreshImage(Blog.PictrueLink);
                    }
                });
        }

        #endregion
    }
}
