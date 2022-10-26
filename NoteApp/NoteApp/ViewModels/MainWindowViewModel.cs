using ImTools;
using NoteApp.Core;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Modules.ModuleName.Views;
using NoteApp.Services.Interfaces;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;
using System.Net;
using System.Windows;

namespace NoteApp.ViewModels
{
    public class MainWindowViewModel : RegionViewModelBase, IConfigureService, IRegionMemberLifetime
    {
        #region 字段

        private readonly IContainerProvider _containerProvider;

        private readonly IRegionManager _regionManager;
        /// <summary>
        /// 区域导航日志
        /// </summary>
        private IRegionNavigationJournal _navigationJournal;

        public DelegateCommand<string> DelegateCommands { get; private set; }

        public DelegateCommand<string> NavigateCommand { get; private set; }
        #endregion

        #region 属性

        private bool _isLeftDrawerOpen;

        public bool IsLeftDrawerOpen
        {
            get { return _isLeftDrawerOpen; }
            set
            {
                _isLeftDrawerOpen = value;
                RaisePropertyChanged();
            }
        }


        private string _title = "Prism Application";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private UserEntity _userEntity;
        /// <summary>
        /// 用户信息临时储存
        /// </summary>
        public UserEntity UserEntity
        {
            get { return _userEntity; }
            set { SetProperty(ref _userEntity, value); }
        }

        private string _blogsCountMessage;

        public string BlogsCountMessage
        {
            get { return _blogsCountMessage; }
            set { SetProperty(ref _blogsCountMessage, value); }
        }

        public bool KeepAlive => false;


        #endregion


        public MainWindowViewModel(IContainerProvider containerProvider, IRegionManager regionManager) : base(regionManager)
        {
            _containerProvider = containerProvider;
            _regionManager = regionManager;
            DelegateCommands = new DelegateCommand<string>(DelegateMethod);
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void DelegateMethod(string arg)
        {
            switch (arg)
            {
                case "HomePageView":
                    PageShow(arg);
                    break;
                case "ViewB":
                    PageShow(arg);
                    break;
                case "ViewC":
                    PageShow(arg);
                    break;
                case "BlogsListView":
                    PageShow(arg);
                    break;
                case "ClearPages":
                    ClearPages();
                    PageShow("HomePageView");
                    break;
                case "PageBack":
                    PageBack();
                    break;
                case "PageForward":
                    PageForward();
                    break;
                case "LoginOut":
                    LoginOut();
                    break;
                case "Exit":
                    Exit();
                    break;
                default:
                    break;
            }
        }




        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="args"></param>
        private void PageShow(string args)
        {
            if (args == "BlogsListView")
            {
                IsLeftDrawerOpen = !IsLeftDrawerOpen; ;
            }
            _regionManager.RequestNavigate(RegionNames.ContentRegion, args, navigationCallback =>
            {
                //判断是否有值
                if ((bool)navigationCallback.Result)
                {
                    //将记录写进导航日志
                    _navigationJournal = navigationCallback.Context.NavigationService.Journal;
                }
            });
        }

        /// <summary>
        /// 清理所有页面
        /// </summary>
        private void ClearPages()
        {
            var result = _regionManager.Regions.GetEnumerator();
            if (result.MoveNext())
            {
                result.Current.RemoveAll();
            }
        }

        private void PageBack()
        {
            if (_navigationJournal.CanGoBack)
            {
                _navigationJournal.GoBack();
            }

        }

        private void PageForward()
        {
            if (_navigationJournal.CanGoForward)
            {
                _navigationJournal.GoForward();
            }
        }

        /// <summary>
        /// 登出账号
        /// </summary>
        private void LoginOut() => App.LoginOut(_containerProvider);
        /// <summary>
        /// 退出应用
        /// </summary>
        private static void Exit()
        {
            if (MessageBox.Show("确认退出应用？", "请选择", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                App.Current.MainWindow.Close();
            }
            else
            {
                return;
            }
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath, navigationCallback =>
                {
                    if ((bool)navigationCallback.Result)
                    {
                        _navigationJournal = navigationCallback.Context.NavigationService.Journal;
                    }
                });
        }

        public void Configure()
        {
            ClearPages();
            UserEntity = AppSession.user;
            PageShow("HomePageView");
        }
    }
}
