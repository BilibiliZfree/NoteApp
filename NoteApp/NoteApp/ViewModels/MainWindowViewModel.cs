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

namespace NoteApp.ViewModels
{
    public class MainWindowViewModel : RegionViewModelBase, IConfigureService
    {
        #region 字段

        private readonly IContainerProvider _containerProvider;

        private readonly IRegionManager _regionManager;

        public DelegateCommand LoginOutCommand { get; private set; }

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


        #endregion


        public MainWindowViewModel(IContainerProvider containerProvider, IRegionManager regionManager) : base(regionManager)
        {
            LoginOutCommand = new DelegateCommand(() =>
            {
                //注销当前用户
                App.LoginOut(containerProvider);
            });
            _containerProvider = containerProvider;
            _regionManager = regionManager;
        }
        //public MainWindowViewModel(IRegionManager regionManager)
        //{
        //    this.regionManager = regionManager;
        //    //NavigateCommand = new RegisterCommand<string>(Navigate);
        //}

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath);
        }


        public void Configure()
        {
            UserEntity = AppSession.user;
            
        }
    }
}
