using NoteApp.Core;
using NoteApp.Core.Mvvm;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    /// <summary>
    /// LoginView的视图模型
    /// </summary>
    public class LoginViewModel : RegionViewModelBase
    {
        private readonly IRegionManager _regionManager;
        /// <summary>
        /// 卡片翻转命令
        /// </summary>
        public DelegateCommand<string> NavigateCommand { get; private set; }

        public LoginViewModel(IRegionManager regionManager) : base(regionManager)
        {
            this._regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(ViewChange);
        }

        private bool flippedStatus;
        /// <summary>
        /// 卡片翻面判定
        /// </summary>
        public bool FlippedStatus
        {
            get { return flippedStatus; }
            set { SetProperty(ref flippedStatus, value); RaisePropertyChanged(); }
        }

        private void ViewChange(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RegisterViewWithRegion(RegionNames.BackRegion, navigatePath);
            FlippedStatus = !FlippedStatus;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something
        }
    }
}
