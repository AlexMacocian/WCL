using System.Windows;
using System.Windows.Controls;

namespace WCL
{
    public class ExpandingPanel : ContentControl
    {
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(ExpandingPanel), new UIPropertyMetadata(null));
        
        public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register("IsExpanded", typeof(bool), typeof(ExpandingPanel), new UIPropertyMetadata(null));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        static ExpandingPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpandingPanel), new FrameworkPropertyMetadata(typeof(ExpandingPanel)));
        }
    }
}
