using System.Windows;
using System.Windows.Controls;

namespace WCL
{
    public class ExpandingPanel : ContentControl
    {
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(ExpandingPanel), new UIPropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        static ExpandingPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpandingPanel), new FrameworkPropertyMetadata(typeof(ExpandingPanel)));
        }
    }
}
