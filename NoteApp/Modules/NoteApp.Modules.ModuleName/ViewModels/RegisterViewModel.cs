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

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class RegisterViewModel : RegionViewModelBase, IDialogAware
    {

        #region 字段

        private readonly IRegionManager _regionManager;

        private readonly IRestSharpServerBase<ApiResponse, UserEntity> _userService;

        public event Action<IDialogResult> RequestClose;

        private DateTime _displayDateEnd;

        private DateTime _displayDateStart = new DateTime(1990/1/1);

        private bool _isEnable = true;
        public string Title => "注册对话";

        private string _message;

        private string _password;

        private string _ReturnValue;

        private UserEntity _user = new UserEntity();

        private string _verification;

        #endregion

        #region 属性

        public DateTime DisplayDateEnd
        {
            get { return _displayDateEnd; }
            set { SetProperty(ref _displayDateEnd, value); }
        }

        public DateTime DisplayDateStart
        {
            get { return _displayDateStart; }
            set { SetProperty(ref _displayDateStart, value); }
        }

        /// <summary>
        /// 按钮,文本框是否可用属性
        /// </summary>
        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }


        /// <summary>
        /// 会话返回数据
        /// </summary>
        public string ReturnValue
        {
            get { return _ReturnValue; }
            set { SetProperty(ref _ReturnValue, value); }
        }

        /// <summary>
        /// 承载用户数据
        /// </summary>
        public UserEntity User 
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Verification
        {
            get { return _verification; }
            set { SetProperty(ref _verification, value); }
        }


        #endregion

        #region 命令
        public DelegateCommand<string> DelegateCommand { get; set; }
        #endregion

        #region 函数
        public RegisterViewModel(IRegionManager regionManager, IRestSharpServerBase<ApiResponse, UserEntity> userService) : base(regionManager)
        {
            _regionManager = regionManager;
            _userService = userService;
            DelegateCommand = new DelegateCommand<string>(DelegateMethod);
            User.Birthday = DisplayDateEnd = DateTime.Now;
            
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
                case "Close":
                    Close();
                    break;
                case "Register":
                    Register();
                    break;
                case "Verify":
                    Message = "验证码为：adjf";
                    break;
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

        private async void Register()
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(User.Avatar))
                {
                    User.Avatar = "C:\\Users\\紫枫伊\\Documents\\source\\repos\\NoteApp\\NoteApp\\NoteApp\\Resources\\Images\\note.png";
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    throw new Exception("再次输入的密码不能为空.");
                }
                if (!Password.Equals(User.Password))
                {
                    throw new Exception("两次密码输入不一致.");
                }
                if (User.Birthday.Equals(new DateTime()))
                {
                    throw new Exception("请选择你的出生日期.");
                }
                if (string.IsNullOrWhiteSpace(Verification))
                {
                    throw new Exception("请输入验证码.");
                }
                if (!Verification.Equals("adjf"))
                {
                    throw new Exception("验证码错误.");
                }
                User.CreateTime = DateTime.Now;
                User.UpdateTime = DateTime.Now;
                ApiResponse response = await _userService.PostApiResponseAsync(User);
                if (response.Status)
                {
                    IsEnable = !IsEnable;
                }
                MessageBox.Show($"{response.Status.ToString()}\n\r{response.Message}\n\r{response.Object}");
                Message = response.Status.ToString() + "-" + response.Message+"\n\r"+response.Object;
            }
            catch (Exception ex)
            {
                Message = $"RegisterViewModel-Register-{ex.Message}";
            }
        }

        #endregion

    }
}
