using NoteApp.Core.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class BlogsListViewModel : RegionViewModelBase,IRegionMemberLifetime
    {
        #region 字段

        private readonly IRegionManager _regionManager;

        #endregion

        #region 属性

        public bool KeepAlive => false;

        #endregion

        #region 函数

        public BlogsListViewModel(IRegionManager regionManager) : base(regionManager)
        {
            this._regionManager = regionManager;
        }

        #endregion

    }
}
