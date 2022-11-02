using NoteApp.Api.Services;
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

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class AccountCancellationViewModel : RegionViewModelBase, IDialogAware
    {
        #region 字段

        private readonly IDialogService _dialogService;

        private readonly IRegionManager _regionManager;

        private readonly IRestSharpServerBase<ApiResponse, UserEntity> _userService;

        public event Action<IDialogResult> RequestClose;

        private string _Mail;

        private string _Message;

        private string _ReturnValue;

        public string Title => "注销账号对话";

        private string _Verification;

        #endregion

        #region 属性



        public string Mail
        {
            get { return _Mail; }
            set { SetProperty(ref _Mail, value); }
        }


        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }

        /// <summary>
        /// 会话返回数据
        /// </summary>
        public string ReturnValue
        {
            get { return _ReturnValue; }
            set { SetProperty(ref _ReturnValue, value); }
        }

        private int _UserId;

        public int UserId
        {
            get { return _UserId; }
            set { SetProperty(ref _UserId, value); }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Verification
        {
            get { return _Verification; }
            set { SetProperty(ref _Verification, value); }
        }

        #endregion

        #region 命令
        public DelegateCommand<string> DelegateCommand { get; set; }
        #endregion

        #region 函数

        public AccountCancellationViewModel(IDialogService dialogService, IRestSharpServerBase<ApiResponse, UserEntity> userService, IRegionManager regionManager) : base(regionManager)
        {
            _dialogService = dialogService;
            _regionManager = regionManager;
            _userService = userService;
            DelegateCommand = new DelegateCommand<string>(DelegateMethod);
        }

        private async void Cancellate()
        {
            try
            {
                if (MessageBox.Show($"确认注销该账户？", "请选择", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (UserId == 0)
                    {
                        throw new Exception("请输入正确的用户账号.");
                    }
                    if (string.IsNullOrWhiteSpace(Mail))
                    {
                        throw new Exception("输入的邮箱不能为空.");
                    }
                    if (string.IsNullOrWhiteSpace(Verification))
                    {
                        throw new Exception("请输入验证码.");
                    }
                    if (!Verification.Equals("t1gg"))
                    {
                        throw new Exception("验证码错误.");
                    }
                    ApiResponse response = await _userService.DeleteApiResponseAsync(UserId);
                    Message = response.Status.ToString() + "\n\r" + response.Message;
                }
                    

            }
            catch (Exception ex)
            {
                Message = $"AccountCancellationViewModel-Cancellate-{ex.Message}";
            }
        }

        public bool CanCloseDialog()
        {
            return true;
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
                case "Cancellate":
                    Cancellate();
                    break;
                case "Close":
                    Close();
                    break;
                case "ForgetPasswordView":
                    ShowDialog(arg);
                    return;
                case "Verify":
                    Message = "验证码为：t1gg";
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
                    _Message = result.GetValue<string>("ReturnValue");
                });
        }

        #endregion
    }
}
