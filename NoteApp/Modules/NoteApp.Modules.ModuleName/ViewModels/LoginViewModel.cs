using Newtonsoft.Json;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class LoginViewModel : RegionViewModelBase, IDialogAware
    {
        #region 字段

        private readonly IDialogService _dialogService;

        private readonly IRegionManager _regionManager;

        private readonly IRestSharpServiceBase<ApiResponse, UserEntity> _userService;

        public event Action<IDialogResult> RequestClose;

        private string _Account;

        public string Title => "登录界面";

        private bool _IsFlipped = false;

        private string _Message;

        private string _Password;

        private string _Visibility = "Hidden";

        #endregion

        #region 属性

        public string Account
        {
            get { return _Account; }
            set { SetProperty(ref _Account, value); }
        }

        /// <summary>
        /// materialDesign:Flipper的翻转标志
        /// </summary>
        public bool IsFlipped
        {
            get { return _IsFlipped; }
            set { SetProperty(ref _IsFlipped, value); }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }

        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }

        /// <summary>
        /// 进度条显示属性
        /// </summary>
        public string Visibility
        {
            get { return _Visibility; }
            set { SetProperty(ref _Visibility, value); }
        }

        #endregion

        #region 命令

        public DelegateCommand<string> DelegateCommand { get; set; }

        #endregion

        #region 函数

        public LoginViewModel(IDialogService dialogService, IRegionManager regionManager, IRestSharpServiceBase<ApiResponse, UserEntity> userService) : base(regionManager)
        {
            _dialogService = dialogService;
            _regionManager = regionManager;
            _userService = userService;
            DelegateCommand = new DelegateCommand<string>(DelegateMethod);
        }



        public bool CanCloseDialog()
        {
            return true;
        }

        private void Close()
        {
            if (MessageBox.Show($"确认退出{Title}？", "请选择", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        private void DelegateMethod(string arg)
        {

            switch(arg)
            {
                case "Close":
                    Close();
                    return;
                case "Flip":
                    Flipping();
                    return;
                case "Login":
                    Login();
                    break;
                case "RegisterView":
                    ShowDialog(arg);
                    return;
                case "ChangePasswordView":
                    ShowDialog(arg);
                    return;
                case "Visible":
                    IsVisible();
                    return;
                default:
                    break;
            }
        }

        public void Flipping() => IsFlipped = !IsFlipped;

        public void IsVisible() => Visibility = Visibility.Equals("Hidden") ? "Visible" : "Hidden";

        private async void Login()
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(Account))
                {
                    throw new Exception("账号不能为空.");
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    throw new Exception("密码不能为空.");
                }
                //id = int.Parse(Account);
                IsVisible();
                ApiResponse response1 = await _userService.LoginAsync(Account, Password);
                if (response1.Status)
                {
                    
                    var js = JsonConvert.SerializeObject(response1.Object);
                    UserEntity user = JsonConvert.DeserializeObject<UserEntity>(js);
                    DialogParameters keyValues = new DialogParameters();
                    keyValues.Add("UserSession", user);
                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keyValues));
                }
                ApiResponse response2 = await _userService.LoginAsync(int.Parse(Account), Password);
                if (response2.Status)
                {
                    var js = JsonConvert.SerializeObject(response2.Object);
                    UserEntity user = JsonConvert.DeserializeObject<UserEntity>(js);
                    DialogParameters keyValues = new DialogParameters();
                    keyValues.Add("UserSession", user);
                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keyValues));
                }
                IsVisible();
                Message = response1.Message + "\n\r" + response2.Message;


            }
            catch (Exception ex)
            {

                Message = $"LoginViewModel-Login-{ex.Message}";
            }
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public void ShowDialog(string name)
        {
            //传入参数
            var keyValues = new DialogParameters();
            _dialogService.ShowDialog(name, keyValues,
                (callback) => 
                {
                    //获取对话返回的数据
                    var result = callback.Parameters;
                    Message = result.GetValue<string>("ReturnValue");
                });
        }

        #endregion

    }
}
