using NoteApp.Core;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class ShowBlogViewModel : RegionViewModelBase, IConfigureService, IRegionMemberLifetime
    {
        #region 字段

        private BlogEntity _Blog;

        #endregion

        #region 属性

        public BlogEntity Blog
        {
            get { return _Blog; }
            set { SetProperty(ref _Blog, value); }
        }

        private FlowDocument _flowDocument;

        public FlowDocument FlowDocument
        {
            get { return _flowDocument; }
            set { SetProperty(ref _flowDocument, value); }
        }


        public bool KeepAlive => false;


        #endregion

        #region 函数

        public ShowBlogViewModel(IRegionManager regionManager) : base(regionManager)
        {
            Configure();
        }

        public void Configure()
        {
            Blog = AppSession.BlogSeesion;
            if (Blog.Context.StartsWith("<FlowDocument") && Blog.Context.EndsWith("</FlowDocument>"))
            {
                FlowDocument = (FlowDocument)XamlReader.Parse(Blog.Context);
            }
        }

        #endregion
    }
}
