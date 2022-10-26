using NoteApp.Core;
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
using System.Windows.Controls;

namespace NoteApp.Modules.ModuleName.ViewModels
{

    public class RegisterViewModel : RegionViewModelBase, IDialogAware
    {

        #region 字段
        public string Title => "注册新用户";

        

        private readonly IRegionManager _regionManager;
        private readonly IRestSharpService _restSharpService;


        public event Action<IDialogResult> RequestClose;

        

        #endregion

        #region 属性

        public string DialogResultStr { get; private set; }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        public DelegateCommand<string> CloseCommand { get; private set; }
        /// <summary>
        /// 发送验证码
        /// </summary>
        public DelegateCommand SendVerificationCommand { get; private set; }
        /// <summary>
        /// 登录命令
        /// </summary>
        public DelegateCommand RegisterCommand { get; private set; }

        private bool _isEnabled = true;
        /// <summary>
        /// 按钮,文本框是否可用属性
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
            }
        }

        private string _message;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _returnMessage;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ReturnMessage
        {
            get { return _returnMessage; }
            set { SetProperty(ref _returnMessage, value); }
        }


        private string userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
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

        private string rePassword;
        /// <summary>
        /// 重复密码
        /// </summary>
        public string RePassword
        {
            get { return rePassword; }
            set { rePassword = value; RaisePropertyChanged(); }
        }

        private string telphone;
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Telphone
        {
            get { return telphone; }
            set { telphone = value; RaisePropertyChanged(); }
        }

        private string verification;
        /// <summary>
        /// 验证码
        /// </summary>
        public string Verification
        {
            get { return verification; }
            set { verification = value; RaisePropertyChanged(); }
        }

        #endregion


        public RegisterViewModel(IRegionManager regionManager, IRestSharpService restSharpService) : base(regionManager)
        {
            _regionManager = regionManager;
            _restSharpService = restSharpService;
            CloseCommand = new DelegateCommand<string>(Close);
            RegisterCommand = new DelegateCommand(Register);
            SendVerificationCommand = new DelegateCommand(VerificationSending);
            ReturnMessage = "退出注册窗口.";
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

        /// <summary>
        /// 关闭逻辑
        /// </summary>
        private void Close(string para)
        {
            DialogResult dialogResult = new DialogResult();
            dialogResult.Parameters.Add("dia", para);
            RequestClose?.Invoke(dialogResult) ;
        }

        private void VerificationSending()
        {
            Message = "验证码已经发送,请注意查收短信.";
        }


        /// <summary>
        /// 登录逻辑
        /// </summary>
        private void Register()
        {
            if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) 
                || string.IsNullOrEmpty(RePassword) || string.IsNullOrEmpty(Telphone))
            {
                Message = "请完善资料.";
                return;
            }
            if (Password != Password)
            {
                Message = "请确保密码输入一致..";
                return;
            }
            if (string.IsNullOrEmpty(Verification))
            {
                Message = "请输入验证码.";
                return;
            }
            if (Verification != "fajh")
            {
                Message = "验证码错误.";
                return;
            }
            else
            {
                ApiResponseU apiResponse = _restSharpService.PostApiResponse(WebApiUrl.RegisterUserUrl, UserName,Password,Telphone);
                Message = apiResponse.Message;
                if (!apiResponse.Status)                    
                    return;
                IsEnabled = false;
                ReturnMessage = $"您已成功完成注册\n\r账号:{apiResponse.Object.ID}\n\r用户名:{apiResponse.Object.UserName}\n\r请牢记密码，防止丢失账号.";
            }
        }
    }
}
