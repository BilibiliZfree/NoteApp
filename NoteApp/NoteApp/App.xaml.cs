using NoteApp.Core;
using NoteApp.Core.RegionAdapter;
using NoteApp.Models;
using NoteApp.Modules.ModuleName;
using NoteApp.Services;
using NoteApp.Services.Interfaces;
using NoteApp.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NoteApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        /// <summary>
        /// 程序启动页
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// 注销当前用户
        /// </summary>
        /// <param name="containerProvider"></param>
        public static void LoginOut(IContainerProvider containerProvider)
        {
            Current.MainWindow.Hide();

            var dialog = containerProvider.Resolve<IDialogService>();

            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
                AppSession.session( callback.Parameters.GetValue<UserEntity>("user") );

            });
            var server = Current.MainWindow.DataContext as IConfigureService;
            if (server != null)
                server.Configure();
            Current.MainWindow.Show();
        }

        protected override void OnInitialized()
        {
            var dialog = Container.Resolve<IDialogService>();

            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
                AppSession.session(callback.Parameters.GetValue<UserEntity>("user"));

            });
            var server = Current.MainWindow.DataContext as IConfigureService;
            if (server != null)
                server.Configure();
            base.OnInitialized();
        }



        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="containerRegistry">注册容器</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            containerRegistry.RegisterSingleton<IRestSharpServerBase<ApiResponse, UserEntity>, UserServer>();
            //containerRegistry.RegisterDialogWindow<ShowDialogWindow>("ShowDialogWindow");

        }
        /// <summary>
        /// 模块化配置列表
        /// </summary>
        /// <param name="moduleCatalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
        }
    }
}
