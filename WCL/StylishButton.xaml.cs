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
    /// Interaction logic for StylishButton.xaml
    /// </summary>
    public partial class StylishButton : UserControl
    {
        private Color borderColor = Colors.Transparent;
        public event EventHandler<RoutedEventArgs> Click;
        public StylishButton()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Content property of the textbox
        /// </summary>
        public string Text { get => (string)ButtonHolder.Content; set => ButtonHolder.Content = value; }
        /// <summary>
        /// Color of the opaque portion of the border.
        /// </summary>
        public Color BorderColor { get => borderColor; set => borderColor = value; }
        /// <summary>
        /// Opacity of the background.
        /// </summary>
        public double BackgroundOpacity { get => GradientBrush.Opacity; set => GradientBrush.Opacity = value; }
        public HorizontalAlignment HorizontalTextAlignment { get => ButtonHolder.HorizontalContentAlignment; set => ButtonHolder.HorizontalContentAlignment = value; }
        public VerticalAlignment VerticalTextAlignment { get => ButtonHolder.VerticalContentAlignment; set => ButtonHolder.VerticalContentAlignment = value; }

        private void Grid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(this);
            ColorGradientStop.Offset = mousePos.X / this.ActualWidth;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            ColorGradientStop.Color = borderColor;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            ColorGradientStop.Color = Colors.Transparent;
        }

        private void ButtonHolder_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                BackgroundRectangle.Opacity = 0.7;
            }
        }

        private void Grid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                BackgroundRectangle.Opacity = 0.5;
            }
        }
    }
}
