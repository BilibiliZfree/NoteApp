using NoteApp.Core;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private bool _IsLeftDrawerOpen;

        private UserEntity _UserEntity;

        #endregion

        #region 属性

        public DelegateCommand<string> DelegateCommand { set; private get; }

        public bool IsLeftDrawerOpen
        {
            get { return _IsLeftDrawerOpen; }
            set { SetProperty(ref _IsLeftDrawerOpen, value); }
        }

        public bool KeepAlive => false;

        /// <summary>
        /// 用户信息临时储存
        /// </summary>
        public UserEntity UserEntity
        {
            get { return _UserEntity; }
            set { SetProperty(ref _UserEntity, value); }
        }

        #endregion

        #region 函数

        public MainWindowViewModel(IContainerProvider containerProvider,IRegionManager regionManager) : base(regionManager)
        {
            _containerProvider = containerProvider;
            _regionManager = regionManager;
            DelegateCommand = new DelegateCommand<string>(DelegateMethod);
            Configure();
            
        }

        /// <summary>
        /// 清理导航数据
        /// </summary>
        private void ClearNavigations()
        {
            var result = _regionManager.Regions.GetEnumerator();
            if (result.MoveNext())
            {
                result.Current.RemoveAll();
            }
            if (_navigationJournal != null)
                _navigationJournal.Clear();

        }

        /// <summary>
        /// 数据配置
        /// </summary>
        public void Configure()
        {
            ClearNavigations();
            UserEntity = AppSession.user;
            RegionShow("HomePageView");
        }

        private void DelegateMethod(string arg)
        {
            switch (arg)
            {
                case "ClearNavigations":
                    ClearNavigations();
                    RegionShow("HomePageView");
                    break;
                case "Exit":
                    Exit();
                    break;
                case "LoginOut":
                    LoginOut();
                    break;
                case "NavigateBack":
                    NavigateBack();
                    break;
                case "NavigateForward":
                    NavigateForward();
                    break;
                default:
                    RegionShow(arg);
                    break;
            }
        }

        

        /// <summary>
        /// 退出应用
        /// </summary>
        private static void Exit()
        {
            if (MessageBox.Show("确认退出应用？", "请选择", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.MainWindow.Close();
            }
            else
            {
                return;
            }
        }

        private void LoginOut() => App.LoginOut(_containerProvider);

        /// <summary>
        /// 导航回退
        /// </summary>
        private void NavigateBack()
        {
            if (_navigationJournal.CanGoBack)
            {
                _navigationJournal.GoBack();
            }

        }

        /// <summary>
        /// 导航前进
        /// </summary>
        private void NavigateForward()
        {
            if (_navigationJournal.CanGoForward)
            {
                _navigationJournal.GoForward();
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="args"></param>
        private void RegionShow(string args)
        {
            if (IsLeftDrawerOpen)
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
        #endregion


    }
}
