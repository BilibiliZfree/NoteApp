using NoteApp.Core.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class HomePageViewModel : RegionViewModelBase, IRegionMemberLifetime
    {
        #region 字段

        private readonly IRegionManager _regionManager;

        #endregion

        #region 属性

        public bool KeepAlive => false;

        #endregion

        #region 方法
        public HomePageViewModel(IRegionManager regionManager) : base(regionManager)
        {
            _regionManager = regionManager;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }
        #endregion

    }
}
