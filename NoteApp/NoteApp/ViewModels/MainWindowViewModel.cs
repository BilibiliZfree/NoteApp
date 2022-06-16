using NoteApp.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace NoteApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private readonly IRegionManager regionManager;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            //NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath);
        }
    }
}
