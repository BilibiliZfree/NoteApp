using NoteApp.Core.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class LoginRegisterViewModel : RegionViewModelBase
    {
        public LoginRegisterViewModel(IRegionManager regionManager) : base(regionManager)
        {
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something
        }
    }
}
