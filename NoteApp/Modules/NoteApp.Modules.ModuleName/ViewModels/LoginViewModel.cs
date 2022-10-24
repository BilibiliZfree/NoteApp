using NoteApp.Core;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services;
using NoteApp.Services.Interfaces;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Net;
using System.Threading;
using System.Windows;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class LoginViewModel : RegionViewModelBase, IDialogAware
    {
        #region 字段

        private readonly IRegionManager regionManager;

        private readonly IRestSharpService _restSharpService;

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

        private string _visibility;
        /// <summary>
        /// 进度条显示属性
        /// </summary>
        public string Visibility
        {
            get { return _visibility; }
            set { SetProperty(ref _visibility, value); }
        }


        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string userId;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId
        {
            get { return userId; }
            set { userId = value; RaisePropertyChanged(); }
        }

        private string password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }


        #endregion


        #region 方法

        public LoginViewModel(IRegionManager regionManager, IDialogService dialogService, IRestSharpService restSharpService) : base(regionManager)
        {
            this.regionManager = regionManager;

            _restSharpService = restSharpService;

            FlippingCommand = new DelegateCommand(Flipping);

            ShowDialogCommand = new DelegateCommand<string>(ShowDialog);

            LoginCommand = new DelegateCommand(Login);

            DbDelegateCommand = new DelegateCommand(Execute, CanExecute);

            CloseCommand = new DelegateCommand(Close);

            this.dialogService = dialogService;
            //this.loginService = loginService;
            ChangeVisibility();
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
                    var diaResult = result.Parameters;
                    MessageBox.Show(diaResult.GetValue<string>("dia"));
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
        private async void Login()
        {
            if(string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "用户名和密码不能为空";
                return;
            }
            ChangeVisibility();
            ApiResponseR apiResponse =await _restSharpService.GetApiResponseAsync(WebApiUrl.LoginUserByIdUrl, userId, Password);

            if(!apiResponse.Status)
            {
                Message = apiResponse.Message;
                ChangeVisibility();
                return;
            }
            DialogParameters keyValues = new DialogParameters();
            keyValues.Add("loginResult",apiResponse.Object);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK,keyValues));
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

        public void ChangeVisibility()
        {
            if(Visibility == null)
            {
                Visibility = "Hidden";
                return;
            }
            if (Visibility.Equals("Hidden"))
            {
                Visibility = "Visible";
                return;
            }
            Visibility = "Hidden";
        }

        #endregion
    }
}
