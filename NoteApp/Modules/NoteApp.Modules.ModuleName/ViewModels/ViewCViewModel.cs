using NoteApp.Core.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteApp.Services.Interfaces;
using NoteApp.Models;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class ViewCViewModel : RegionViewModelBase, IDialogAware
    {
        //public ViewCViewModel(IRegionManager regionManager) :
        //    base(regionManager)
        //{

        //}



        public ViewCViewModel(IRegionManager regionManager) :
            base(regionManager)
        {

        }

        public string Title => "ViewC";


        public event Action<IDialogResult> RequestClose;

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
