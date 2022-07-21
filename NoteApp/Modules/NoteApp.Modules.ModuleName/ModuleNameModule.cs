using NoteApp.Core;
using NoteApp.Modules.ModuleName.ViewModels;
using NoteApp.Modules.ModuleName.Views;
using NoteApp.Services;
using NoteApp.Services.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Windows.Controls;

namespace NoteApp.Modules.ModuleName
{
    /// <summary>
    /// 模块化类
    /// </summary>
    public class ModuleNameModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleNameModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// 区域注册
        /// </summary>
        /// <param name="containerProvider"></param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewB");
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewD");
            //_regionManager.RequestNavigate(RegionNames.ForgotRegion, "ViewC");
        }
        /// <summary>
        /// 视图控件注册
        /// </summary>
        /// <param name="containerRegistry"></param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册对话框
            //服务
            //导航
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
            containerRegistry.RegisterForNavigation<ViewB>();
            containerRegistry.RegisterForNavigation<ViewC>();
            containerRegistry.RegisterForNavigation<ViewD>();
            //弹窗
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
            containerRegistry.RegisterDialog<RegisterView, RegisterViewModel>();
            containerRegistry.RegisterDialog<ForgotView, ForgotViewModel>();

        }
    }
}