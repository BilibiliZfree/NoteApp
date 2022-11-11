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
  

        public static FlowDocument GetFlowDocument(DependencyObject obj)
        {
            return (FlowDocument)obj.GetValue(FlowDocumentProperty);
        }

        public static void SetFlowDocument(DependencyObject obj, FlowDocument value)
        {
            obj.SetValue(FlowDocumentProperty, value);
        }

        // Using a DependencyProperty as the backing store for FlowDocument.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlowDocumentProperty =
            DependencyProperty.RegisterAttached("FlowDocument", typeof(FlowDocument), typeof(RichTextBoxExtension), new PropertyMetadata(FlowDocumentPropertyChanged));

        private static void FlowDocumentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RichTextBox box = d as RichTextBox;
            if (box != null)
            {
                box.Document = (FlowDocument)e.NewValue;
            }
        }
    }
}
