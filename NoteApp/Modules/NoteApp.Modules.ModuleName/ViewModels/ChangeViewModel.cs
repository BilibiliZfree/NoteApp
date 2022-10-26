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

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class ChangeViewModel : RegionViewModelBase, IDialogAware
    {
        #region 字段

        public string Title => "修改账号信息";

        private readonly IRegionManager _regionManager;
        private readonly IRestSharpService _restSharpService;

        public event Action<IDialogResult> RequestClose;

        #endregion


        #region 属性

        public DelegateCommand<string> CloseCommand { get; private set; }
        /// <summary>
        /// 发送验证码
        /// </summary>
        public DelegateCommand SendVerificationCommand { get; private set; }
        /// <summary>
        /// 修改密码
        /// </summary>
        public DelegateCommand ChangePasswordCommand { get; private set; }

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

        private bool _isSending = true;
        /// <summary>
        /// 可用属性:发送验证码专用
        /// </summary>
        public bool IsSending
        {
            get
            {
                return _isSending;
            }
            set
            {
                SetProperty(ref _isSending, value);
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

        private string oldPassword;
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword
        {
            get { return oldPassword; }
            set { oldPassword = value; RaisePropertyChanged(); }
        }

        private string newPassword;
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword
        {
            get { return newPassword; }
            set { newPassword = value; RaisePropertyChanged(); }
        }

        private string reNewPassword;
        /// <summary>
        /// 重复新密码
        /// </summary>
        public string ReNewPassword
        {
            get { return reNewPassword; }
            set { reNewPassword = value; RaisePropertyChanged(); }
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


        #region 方法

        public ChangeViewModel(IRegionManager regionManager, IRestSharpService restSharpService) : base(regionManager)
        {
            _regionManager = regionManager;
            _restSharpService = restSharpService;
            CloseCommand = new DelegateCommand<string>(Close);
            SendVerificationCommand = new DelegateCommand(VerificationSending);
            ChangePasswordCommand = new DelegateCommand(Change);
            ReturnMessage = "已退出修改密码界面.";
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

        private void Close(string para)
        {
            DialogResult dialogResult = new DialogResult();
            //对应LoginViewModel类ShowDialog方法
            dialogResult.Parameters.Add("dia", para);
            RequestClose?.Invoke(dialogResult);
        }

        private void VerificationSending()
        {
            Message = "验证码已经发送,请注意查收短信.";
        }

        /// <summary>
        /// 修改密码逻辑
        /// </summary>
        private void Change()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(OldPassword)
                || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ReNewPassword) )
            {
                Message = "请完善资料.";
                return;
            }
            if (NewPassword != ReNewPassword)
            {
                Message = "两个新密码不一致.";
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
                ApiResponseU apiResponse = _restSharpService.PutApiResponse(WebApiUrl.ChangeUserUrl,UserName,OldPassword,NewPassword);
                Message = apiResponse.Message;
                if (!apiResponse.Status)
                    return;
                Message = "密码已修改.";
                ReturnMessage = "密码已成功修改.\n\r请牢记密码，谨防账号丢失.";
                IsEnabled = false;
            }
        }

        #endregion

    }
}
