using System.Windows;

namespace NoteApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //左键窗体移动
            MainBoeder.MouseLeftButtonDown += (o, e) =>
            {
                DragMove();
            };
            //退出按钮
            CloseButton.Click += (o, e) =>
            {
                if (MessageBox.Show("是否退出程序？", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    Close();
            };
        }
    }
}
