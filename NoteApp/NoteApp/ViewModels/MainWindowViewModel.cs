using NoteApp.Core;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Net;
using System.Windows;

namespace NoteApp.ViewModels
{
    public class MainWindowViewModel : RegionViewModelBase, IConfigureService, IRegionMemberLifetime
    {
        #region 字段

        private readonly IContainerProvider _containerProvider;

        private readonly IRegionManager _regionManager;

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

        private void LoginOut()
        {
            App.LoginOut(_containerProvider);
        }

        private void Exit()
        {
            if (MessageBox.Show("确认退出应用？","请选择",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath);
        }

        //public override bool IsNavigationTarget(NavigationContext navigationContext)
        //{
        //    // 当切换到本界面时：
        //    // 当返回True的时候，返回容器里面的view
        //    // 当返回 False的时候，返回一个新的view
        //    // 和 KeepAlive时相同的意思
        //    return false;
        //}

        public void Configure()
        {
            if(!_regionManager.Regions.ContainsRegionWithName("BlogsListView"))
                _regionManager.RequestNavigate(RegionNames.ContentRegion, "BlogsListView");
            UserEntity = AppSession.user;
            
        }
    }
}
