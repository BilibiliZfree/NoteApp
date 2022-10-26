using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteApp.Core.Mvvm;
using Prism.Regions;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    internal class HomePageViewModel : RegionViewModelBase, IRegionMemberLifetime
    {
        #region 字段

        private readonly IRegionManager _regionManager;

        #endregion

        #region 属性

        public bool KeepAlive
        {
            get
            {
                return false;
            }
        }

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
