using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteApp.Core.Mvvm;
using Prism.Regions;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class BlogsListViewModel : RegionViewModelBase
    {
        public BlogsListViewModel(IRegionManager regionManager) : base(regionManager)
        {
        }
    }
}
