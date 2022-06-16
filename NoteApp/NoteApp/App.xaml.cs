using NoteApp.Core.RegionAdapter;
using NoteApp.Modules.ModuleName;
using NoteApp.Modules.ModuleName.Views;
using NoteApp.Services;
using NoteApp.Services.Interfaces;
using NoteApp.ViewModels;
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
            //return Container.Resolve<LoginWindow>();
            return Container.Resolve<MainWindow>();
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
            });
            base.OnInitialized();
        }

        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="containerRegistry">注册容器</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
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
