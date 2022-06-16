
using NoteApp.Core.Mvvm;
using Prism.Commands;
using Prism.Navigation;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class LoginViewModel : RegionViewModelBase, IDialogAware
    {
        #region 字段

        private readonly IRegionManager regionManager;

        public string Title => "登录页面";

        public event Action<IDialogResult> RequestClose;

        private bool _IsFlipped;

        private bool _isEnabled = false;

        private string _message;
        private IDialogService dialogService;

        /// <summary>
        /// 翻转界面
        /// </summary>
        public DelegateCommand FlippingCommand { get; set; }

        public DelegateCommand<string> ShowDialogCommand { get; private set; }
        /// <summary>
        /// 登录命令
        /// </summary>
        public DelegateCommand LoginCommand { get; set; }

        /// <summary>
        /// 选择数据库文件路径
        /// </summary>
        public DelegateCommand DbDelegateCommand { get; private set; }

        public DelegateCommand CloseCommand { get; private set; }

        #endregion

        #region 属性


        /// <summary>
        /// materialDesign:Flipper的翻转属性
        /// </summary>
        public bool IsFlipped
        {
            get { return _IsFlipped; }
            set { SetProperty(ref _IsFlipped, value); }
        }


        /// <summary>
        /// 按钮可用属性
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                SetProperty(ref _isEnabled, value);
                DbDelegateCommand.RaiseCanExecuteChanged();
            }
        }


        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        #endregion


        #region 方法

        public LoginViewModel(IRegionManager regionManager, IDialogService dialogService) : base(regionManager)
        {
            this.regionManager = regionManager;

            FlippingCommand = new DelegateCommand(Flipping);

            ShowDialogCommand = new DelegateCommand<string>(ShowDialog);

            LoginCommand = new DelegateCommand(Login);

            DbDelegateCommand = new DelegateCommand(Execute, CanExecute);

            CloseCommand = new DelegateCommand(Close);

            this.dialogService = dialogService;
        }

        private void Close()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        /// <summary>
        /// 界面翻转逻辑
        /// </summary>
        private void Flipping() => IsFlipped = !IsFlipped;

        /// <summary>
        /// 弹窗逻辑
        /// </summary>
        /// <param name="viewName">区域名</param>
        private void ShowDialog(string navigatePath)
        {
            DialogParameters param = new DialogParameters();
            // View的注册名称 - 参数键值对 - 弹窗回调 - 指定弹出窗口的注册名称
            dialogService.ShowDialog(navigatePath, param,
                (result) =>
                {
                    var r = result.Parameters;
                    MessageBox.Show(r.GetValue<string>("test"));
                });
            /**
            if (navigatePath != null)
            {
                switch (navigatePath)
                {
                    case "ViewA":
                        new DialogWindow() { Content = new ViewA() }.ShowDialog();
                        break;
                    case "ViewB":
                        new DialogWindow() { Content = new ViewB() }.ShowDialog();
                        break;
                    case "ViewC":
                        new DialogWindow() { Content = new ViewC() }.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
            */
        }

        /// <summary>
        /// 登录逻辑
        /// </summary>
        private void Login()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        private void Execute()
        {
            Message = $"Updated: {DateTime.Now}";
        }

        private bool CanExecute()
        {
            return IsEnabled;
        }


        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        #endregion
    }
}
