using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NoteApp.Core.Mvvm;
using NoteApp.Models;
using NoteApp.Services.Interfaces;
using Prism.Commands;
using Prism.Common;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace NoteApp.Modules.ModuleName.ViewModels
{
    public class GetPictureViewModel : RegionViewModelBase, IDialogAware, IRegionMemberLifetime,IConfigureService
    {
        #region 字段

        private readonly IRegionManager _regionManager;

        public DelegateCommand<string> DelegateCommand { get; private set; }

        private ICollection<DirectoryInfo> _Info = new Collection<DirectoryInfo>();

        public bool KeepAlive => false;

        public event Action<IDialogResult> RequestClose;

        private string _ReturnValue;

        private bool _ReturnVar = false;

        public string Title => "获取图片链接页面";

        #endregion

        #region 属性

        private ICollection<LocalFileInfo> _LocalFileInfo = new Collection<LocalFileInfo>();  

        public ICollection<LocalFileInfo> LocalFileInfo
        {
            get { return _LocalFileInfo; }
            set { SetProperty(ref _LocalFileInfo, value); }
        }



        /// <summary>
        /// 文件夹列表
        /// </summary>
        public ICollection<DirectoryInfo> Info
        {
            get { return _Info; }
            set { SetProperty(ref _Info, value); }
        }

        private string _Message;

        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }


        /// <summary>
        /// 会话返回数据
        /// </summary>
        public string ReturnValue
        {
            get { return _ReturnValue; }
            set { SetProperty(ref _ReturnValue, value); }
        }

        public bool ReturnVar
        {
            get { return _ReturnVar; }
            set { SetProperty(ref _ReturnVar, value); }
        }




        #endregion

        #region 函数

        public GetPictureViewModel(IRegionManager regionManager) : base(regionManager)
        {
            _regionManager = regionManager;
            DelegateCommand = new DelegateCommand<string>(DelegateMethod);
            Configure();
        }

        public void Close()
        {
            if (MessageBox.Show($"确认退出{Title}？", "请选择", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var result = new DialogResult();
                result.Parameters.Add("ReturnValue", ReturnValue);
                result.Parameters.Add("ReturnVar", ReturnVar);
                RequestClose?.Invoke(result);
            }
        }

        private void DelegateMethod(string arg)
        {
            switch (arg)
            {
                case "Close":
                    Close();
                    break;
                case "ReturnLink":
                    ReturnVar = true;
                    Close();
                    break;
                default:
                    FolderRead(arg);
                    break;
            }
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void Configure()
        {
            string[] logicDrives = Directory.GetLogicalDrives();
            Collection<LocalFileInfo> localFiles = new Collection<LocalFileInfo>();
            foreach (var dir in logicDrives)
            {
                DriveInfo di = new DriveInfo(driveName: dir);
                if (!di.IsReady)
                {
                    Message += $"\n\rThe drive {di.Name} could not be read";
                    continue;
                }
                DirectoryInfo directoryInfo = di.RootDirectory;
                LocalFileInfo localFileInfos = new LocalFileInfo(true,directoryInfo.FullName,directoryInfo.Name,directoryInfo.Parent, "Folder");
                if (directoryInfo == null)
                    Message += $"\n\rThe drive {di.Name} is null";

                localFiles.Add(localFileInfos);
                Message += $"\n\rThe drive {di.Name} read success";
            }
            LocalFileInfo = localFiles;
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            
            //DirectoryInfo info= new DirectoryInfo(path);

            //if (info.Exists) 
            //{
            //    ReturnValue = "Success!";
            //}
            
            

        }
        /// <summary>
        /// 判断是图片还是文件夹
        /// </summary>
        /// <param name="path"></param>
        public void FolderRead(string path)
        {
            if (!IsDirectory(path))
            {
                Message += "\n\r这不是一个文件夹.";
                if (IsPicture(path))
                {
                    ReturnValue = path;
                    Message += "\n\r这是图片.";
                }
                return;
            }
            string[] dirs= Directory.GetDirectories(path);
            string[] files= Directory.GetFiles(path);
            Collection<LocalFileInfo> localFiles = new Collection<LocalFileInfo>();
            if(new DirectoryInfo(path).Parent != null)
                localFiles.Add(new LocalFileInfo(true, new DirectoryInfo(path).Parent.FullName, "返回上级", null, "FolderArrowUp"));
            foreach (var dir in dirs)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);

                if (dirInfo.Exists)
                {
                    LocalFileInfo localFileInfos =
                        new LocalFileInfo(false, dirInfo.FullName, dirInfo.Name, dirInfo.Parent, "Folder");
                    localFiles.Add(localFileInfos);
                }
            }
            foreach (var file in files)
            {
                FileInfo fileInfo= new FileInfo(file);

                if (fileInfo.Exists)
                {
                    LocalFileInfo localFileInfos = 
                        new LocalFileInfo(false, fileInfo.FullName, fileInfo.Name, null, "File");
                    localFiles.Add(localFileInfos);
                }
            }
            LocalFileInfo = localFiles;
        }

        bool IsDirectory(string path)
        {
            return Directory.Exists(path);
        }

        bool IsPicture(string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fs);
                string fileClass;
                byte buffer;
                byte[] b = new byte[2];
                buffer = reader.ReadByte();
                b[0] = buffer;
                fileClass = buffer.ToString();
                buffer = reader.ReadByte();
                b[1] = buffer;
                fileClass += buffer.ToString();
                reader.Close();
                fs.Close();
                if (fileClass == "255216" || fileClass == "7173" || fileClass == "6677" || fileClass == "13780")//255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        #endregion
    }
}
