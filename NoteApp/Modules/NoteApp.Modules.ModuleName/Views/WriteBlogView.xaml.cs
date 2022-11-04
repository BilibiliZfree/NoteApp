using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

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

        private void RichTextChanged(object sender, RoutedEventArgs e)
        {

            //FlowDocument document = RichText_TextBox.Document;
            //TextRange range = new TextRange(RichText_TextBox.Document.ContentStart, RichText_TextBox.Document.ContentEnd);

            
            string xw = XamlWriter.Save(RichText_TextBox.Document);


            //FlowDocument flow = (FlowDocument)XamlReader.Parse(xw);
            //TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
            
            //FileStream stream = new FileStream(@".\AboutMyDog.xaml", FileMode.Create);

            //range.Save(stream, DataFormats.Xaml);

            //if (stream != null)
            //stream.Close();

            Text_TextBox.Text = xw;
            //Text_TextBox.Text = range.Text;
        }
    }
}
