using NoteApp.Modules.ModuleName;
using NoteApp.Modules.ModuleName.Views;
using NoteApp.Services;
using NoteApp.Services.Interfaces;
using NoteApp.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

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
            return Container.Resolve<LoginWindow>();
        }

        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="containerRegistry">注册容器</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }
        /// <summary>
        /// 模块化配置列表
        /// </summary>
        /// <param name="moduleCatalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}
