using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSampleCodeEditor
{
    [TemplatePart(Name = "PART_StackPanel", Type = typeof(StackPanel))]
    public class CodeEditor : RichTextBox
    {
        static CodeEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CodeEditor), new FrameworkPropertyMetadata(typeof(CodeEditor)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            stackPanel = GetTemplateChild("PART_StackPanel") as StackPanel;
            
        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            LineNumbers();
        }
        protected override void OnSelectionChanged(RoutedEventArgs e)
        {
            base.OnSelectionChanged(e);
            LineNumbers();
        }
        private void LineNumbers()
        {
            stackPanel.Children.Clear();
            int lineNumber = 1;
            var linePos = Document.ContentStart.GetLineStartPosition(0);
            while (true)
            {
                Label label = new Label();
                label.Padding = new Thickness(0, 0, 5, 0);
                label.Margin = new Thickness(0);
                label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                label.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                var rect = linePos.GetCharacterRect(LogicalDirection.Backward);
                label.Height = rect.Height;
                label.Content = lineNumber.ToString();
                stackPanel.Children.Add(label);
                int result;
                linePos = linePos.GetLineStartPosition(1, out result);
                if (result == 0)
                {
                    break;
                }
                ++lineNumber;
            }
        }
        public StackPanel stackPanel;

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
        }
    }
}
