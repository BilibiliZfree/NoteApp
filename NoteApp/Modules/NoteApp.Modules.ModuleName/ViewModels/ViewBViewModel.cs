using NoteApp.Core.Mvvm;
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
    internal class ViewBViewModel : RegionViewModelBase, IDialogAware
    {
        private readonly IRegionManager regionManager;

        private string updateTime;

        public string UpdateTime
        {
            get { return updateTime; }
            set { SetProperty(ref updateTime, value); }
        }


        public ViewBViewModel(IRegionManager regionManager) : base(regionManager)
        {
            this.regionManager = regionManager;
            UpdateCommand = new DelegateCommand(Update);
        }

        private void Update()
        {
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
