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
    public class RegisterViewModel : RegionViewModelBase, IDialogAware
    {
        public RegisterViewModel(IRegionManager regionManager) : base(regionManager)
        {
            DelegateCommand = new DelegateCommand<string>(Register);
        }

        public DelegateCommand<string> DelegateCommand { get; private set; }

        public string Title => "注册新用户";

        public string dialogResultStr { get; private set; }

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

        private void Register(string s)
        {
            DialogResult result = new DialogResult();
            result.Parameters.Add("test", s);
            RequestClose?.Invoke(result);
        }
    }
}
