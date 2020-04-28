using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WCL
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WCL"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WCL;assembly=WCL"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ExtendedScrollViewer/>
    ///
    /// </summary>
    public class ExtendedScrollViewer : ScrollViewer
    {
        public static readonly DependencyProperty ScrollbarForegroundProperty =
            DependencyProperty.Register("ScrollbarForeground", typeof(Brush), typeof(ExtendedScrollViewer), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ScrollbarBackgroundProperty =
            DependencyProperty.Register("ScrollbarBackground", typeof(Brush), typeof(ExtendedScrollViewer), new UIPropertyMetadata(null));

        public Brush ScrollbarForeground
        {
            get => (SolidColorBrush)GetValue(ScrollbarForegroundProperty);
            set => SetValue(ScrollbarForegroundProperty, value);
        }

        public Brush ScrollbarBackground
        {
            get => (SolidColorBrush)GetValue(ScrollbarBackgroundProperty);
            set => SetValue(ScrollbarBackgroundProperty, value);
        }

        static ExtendedScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedScrollViewer), new FrameworkPropertyMetadata(typeof(ExtendedScrollViewer)));
        }
    }
}
