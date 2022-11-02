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
            //_regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewB");
            //_regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewD");
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
            //containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
            //containerRegistry.RegisterForNavigation<ViewB>();
            //containerRegistry.RegisterForNavigation<ViewC>();
            //containerRegistry.RegisterForNavigation<ViewD>();
            containerRegistry.RegisterForNavigation<BlogsListView, BlogsListViewModel>();
            containerRegistry.RegisterForNavigation<HomePageView, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<WriteBlogView, WriteBlogViewModel>();
            //----------对话----------
            //注销账户
            containerRegistry.RegisterDialog<AccountCancellationView, AccountCancellationViewModel>();
            //修改密码
            containerRegistry.RegisterDialog<ChangePasswordView, ChangePasswordViewModel>();
            //忘记密码
            containerRegistry.RegisterDialog<ForgetPasswordView, ForgetPasswordViewModel>();
            //选择图片
            containerRegistry.RegisterDialog<GetPictureView, GetPictureViewModel>();
            //用户登录
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
            //用户注册
            containerRegistry.RegisterDialog<RegisterView, RegisterViewModel>();

        }
    }
}