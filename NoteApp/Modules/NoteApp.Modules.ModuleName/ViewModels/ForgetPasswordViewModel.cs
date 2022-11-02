using NoteApp.Core.Mvvm;
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
    public class ForgetPasswordViewModel : RegionViewModelBase, IDialogAware
    {
        #region 字段

        private readonly IRegionManager _regionManager;

        public event Action<IDialogResult> RequestClose;

        public string Title => "找回密码对话";

        private string _ReturnValue;

        #endregion

        #region 属性

        /// <summary>
        /// 会话返回数据
        /// </summary>
        public string ReturnValue
        {
            get { return _ReturnValue; }
            set { SetProperty(ref _ReturnValue, value); }
        }

        #endregion

        #region 命令
        public DelegateCommand<string> DelegateCommand { get; set; }
        #endregion

        #region 函数

        public ForgetPasswordViewModel(IRegionManager regionManager) : base(regionManager)
        {
            _regionManager = regionManager;
            DelegateCommand = new DelegateCommand<string>(DelegateMethod);
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

        #endregion
    }
}
