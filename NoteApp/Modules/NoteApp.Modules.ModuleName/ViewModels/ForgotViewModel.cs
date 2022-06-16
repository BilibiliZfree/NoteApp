using NoteApp.Core;
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
    public class ForgotViewModel : RegionViewModelBase, IDialogAware
    {
        private readonly IRegionManager _regionManager;

        public ForgotViewModel(IRegionManager regionManager) : base(regionManager)
        {
            DelegateCommand = new DelegateCommand<string>(Forget);
            this._regionManager = regionManager;
        }

        public DelegateCommand<string> DelegateCommand { get; private set; }

        public string Title => "修改账号信息";

        public event Action<IDialogResult> RequestClose;

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

        private void Forget(string s)
        {
            DialogResult result = new DialogResult();
            result.Parameters.Add("test", s);
            RequestClose?.Invoke(result);
        }
    }
}
