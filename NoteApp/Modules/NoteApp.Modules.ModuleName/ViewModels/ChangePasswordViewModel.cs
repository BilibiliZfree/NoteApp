using Newtonsoft.Json;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Modules.ModuleName.Views;
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

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class ChangePasswordViewModel : RegionViewModelBase, IDialogAware
    {
        #region 字段

        private readonly IDialogService _dialogService;

        private readonly IRegionManager _regionManager;

        private readonly IRestSharpServiceBase<ApiResponse, UserEntity> _userService;

        public event Action<IDialogResult> RequestClose;

        public string Title => "修改密码对话";

        private string _Message;

        private string _OldPassword;

        private string _Password;

        private string _RePassword;

        private string _ReturnValue;

        private int _UserId;

        #endregion

        #region 属性

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }

        public string OldPassword
        {
            get { return _OldPassword; }
            set { SetProperty(ref _OldPassword, value); }
        }

        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }

        public string RePassword
        {
            get { return _RePassword; }
            set { SetProperty(ref _RePassword, value); }
        }

        /// <summary>
        /// 会话返回数据
        /// </summary>
        public string ReturnValue
        {
            get { return _ReturnValue; }
            set { SetProperty(ref _ReturnValue, value); }
        }

        private UserEntity _User = new UserEntity();

        public UserEntity User
        {
            get { return _User; }
            set { SetProperty(ref _User, value); }
        }


        public int UserId
        {
            get { return _UserId; }
            set { SetProperty(ref _UserId, value); }
        }

        #endregion

        #region 命令
        public DelegateCommand<string> DelegateCommand { get; set; }
        #endregion

        #region 函数

        public ChangePasswordViewModel(IDialogService dialogService, IRestSharpServiceBase<ApiResponse, UserEntity> userService,IRegionManager regionManager) : base(regionManager)
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

        private async void ChangePassword()
        {
            try
            {
                if (UserId == 0)
                {
                    throw new Exception("请输入正确的用户账号.");
                }
                if (string.IsNullOrWhiteSpace(OldPassword))
                {
                    throw new Exception("密码不能为空.");
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    throw new Exception("新密码不能为空.");
                }
                if (!Password.Equals(RePassword))
                {
                    throw new Exception("两次新密码输入不一致.");
                }
                ApiResponse response = await _userService.LoginAsync(UserId,OldPassword);
                if (response.Status)
                {
                    var js = JsonConvert.SerializeObject(response.Object);
                    User = JsonConvert.DeserializeObject<UserEntity>(js);
                    User.Password = Password;
                    User.UpdateTime = DateTime.Now;
                    response = await _userService.PutApiResponseAsync(User);
                }
                Message = response.Status.ToString() + "-" + response.Message;
            }
            catch (Exception ex)
            {
                Message = $"ChangePasswordViewModel-ChangePassword-{ex.Message}";
            }
        }

        public void Close()
        {
            if (MessageBox.Show($"确认退出{Title}？", "请选择", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var result = new DialogResult();
                result.Parameters.Add("ReturnValue", ReturnValue);
                RequestClose?.Invoke(result);
            }
        }

        private void DelegateMethod(string arg)
        {
            switch (arg)
            {
                case "ChangePassword":
                    ChangePassword();
                    break;
                case "Close":
                    Close();
                    break;
                case "ForgetPasswordView":
                    ShowDialog(arg);
                    return;
                case "AccountCancellationView":
                    ShowDialog(arg);
                    return;
                default:
                    break;
            }
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        public void ShowDialog(string navigatePath)
        {
            //传入参数
            var keyValues = new DialogParameters();
            _dialogService.ShowDialog(navigatePath, keyValues,
                (callback) =>
                {
                    //获取对话返回的数据
                    var result = callback.Parameters;
                    _Message = result.GetValue<string>("Tip");
                });
        }

        #endregion

    }
}
