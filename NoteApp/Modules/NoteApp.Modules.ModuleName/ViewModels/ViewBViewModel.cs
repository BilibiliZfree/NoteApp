using NoteApp.Core;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
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
    internal class ViewBViewModel : RegionViewModelBase, IDialogAware, IRegionMemberLifetime
    {
        private readonly IRegionManager regionManager;

        public bool KeepAlive
        {
            get
            {
                return false;
            }
        }

        private string updateTime;

        public string UpdateTime
        {
            get { return updateTime; }
            set { SetProperty(ref updateTime, value); }
        }

        private UserEntity _userEntity;

        public UserEntity UserEntity
        {
            get { return _userEntity; }
            set { _userEntity = value; RaisePropertyChanged(); }
        }


        public ViewBViewModel(IRegionManager regionManager) : base(regionManager)
        {
            this.regionManager = regionManager;
            UpdateCommand = new DelegateCommand(Update);
        }

        private void Update()
        {
            UserEntity = AppSession.user;
            UpdateTime = $"{DateTime.Now}";
        }

        public string Title => "";


        public event Action<IDialogResult> RequestClose;

        public DelegateCommand UpdateCommand { get; private set; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}
