using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Reflection.Metadata;
using System.Windows.Documents;
using System.Windows.Markup;

namespace NoteApp.Modules.ModuleName.ControlsExtension
{
    public class RichTextBoxExtension : DependencyObject
    {


        public FlowDocument MyProperty
        {
            get { return (FlowDocument)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(FlowDocument), typeof(RichTextBoxExtension), new PropertyMetadata(new Document(),new PropertyChangedCallback(OnDocumentChanged)));

        private static void OnDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show(XamlWriter.Save(e.NewValue));
        }
    }
}
