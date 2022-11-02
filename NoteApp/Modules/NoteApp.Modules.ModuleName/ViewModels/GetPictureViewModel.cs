using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NoteApp.Core.Mvvm;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class GetPictureViewModel : RegionViewModelBase, IDialogAware, IRegionMemberLifetime
    {
        #region 字段

        private readonly IRegionManager _regionManager;

        public DelegateCommand<string> DelegateCommand { get; private set; }

        public event Action<IDialogResult> RequestClose;

        public bool KeepAlive => false;

        public string Title => "获取图片链接页面";

        private string _ReturnValue;

        private bool _ReturnVar = false;

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

        public bool ReturnVar
        {
            get { return _ReturnVar; }
            set { SetProperty(ref _ReturnVar, value); }
        }

        #endregion

        #region 函数

        public GetPictureViewModel(IRegionManager regionManager) : base(regionManager)
        {
            _regionManager = regionManager;
            DelegateCommand = new DelegateCommand<string>(DelegateMethod);
        }

        public void Close()
        {
            if (MessageBox.Show($"确认退出{Title}？", "请选择", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var result = new DialogResult();
                result.Parameters.Add("ReturnValue", ReturnValue);
                result.Parameters.Add("ReturnVar", ReturnVar);
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
                case "ReturnLink":
                    ReturnVar = true;
                    Close();
                    break;
                default:
                    break;
            }
        }

        #endregion










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
    }
}
