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
        }
    }
}
