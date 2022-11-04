using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml;
using System.Xml;

namespace NoteApp.Modules.ModuleName.Views
{
    /// <summary>
    /// WriteBlogView.xaml 的交互逻辑
    /// </summary>
    public partial class WriteBlogView : UserControl
    {
        public WriteBlogView()
        {
            InitializeComponent();
        }

        private void GetRichText_Click(object sender, RoutedEventArgs e)
        {

            //FlowDocument document = RichText_TextBox.Document;
            //TextRange range = new TextRange(RichText_TextBox.Document.ContentStart, RichText_TextBox.Document.ContentEnd);


            string xw = System.Windows.Markup.XamlWriter.Save(RichText_TextBox.Document);
            //FileStream stream = new FileStream(@".\AboutMyDog.xaml", FileMode.Create);

            //range.Save(stream, DataFormats.Xaml);

            //if (stream != null)
            //stream.Close();

            Text_TextBox.Text = xw;
        }
    }
}
