using NoteApp.Core;
using NoteApp.Modules.ModuleName.ViewModels;
using NoteApp.Modules.ModuleName.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

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
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewA");
            _regionManager.RequestNavigate(RegionNames.LoginRegion, "LoginView");
            //_regionManager.RequestNavigate(RegionNames.BackRegion, "LoginSettingView");
            //_regionManager.RequestNavigate(RegionNames.BackRegion, "LoginRegisterView");
            //_regionManager.RegisterViewWithRegion(RegionNames.BackRegion, typeof(LoginRegisterView));
            //_regionManager.RegisterViewWithRegion(RegionNames.BackRegion, typeof(LoginSettingView));
            //_regionManager.RegisterViewWithRegion(RegionNames.BackRegion, typeof(LoginRegisterView));
            //_regionManager.RegisterViewWithRegion(RegionNames.BackRegion, typeof(LoginSettingView));
        }

        /// <summary>
        /// 视图控件注册
        /// </summary>
        /// <param name="containerRegistry"></param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<LoginRegisterView, LoginRegisterViewModel>();
            containerRegistry.RegisterForNavigation<LoginSettingView, LoginSettingViewModel>();
        }
    }
}