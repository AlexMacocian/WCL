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
    /// Interaction logic for TransparentTextBox.xaml
    /// </summary>
    public partial class StylishTextBlock : UserControl
    {
        private Color borderColor = Colors.Transparent;
        public StylishTextBlock()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Text property of the textbox
        /// </summary>
        public string Text { get => Textblock.Text; set => Textblock.Text = value; }
        /// <summary>
        /// Color of the opaque portion of the border.
        /// </summary>
        public Color BorderColor { get => borderColor; set => borderColor = value; }
        /// <summary>
        /// Opacity of the background.
        /// </summary>
        public double BackgroundOpacity { get => GradientBrush.Opacity; set => GradientBrush.Opacity = value; }
        /// <summary>
        /// Color of the text.
        /// </summary>
        public SolidColorBrush ForegroundColor { get => Textblock.Foreground as SolidColorBrush; set => Textblock.Foreground = value; }
        public TextAlignment HorizontalTextAlignment { get => Textblock.TextAlignment; set => Textblock.TextAlignment = value; }
        public VerticalAlignment VerticalTextAlignment { get => Textblock.VerticalAlignment; set => Textblock.VerticalAlignment = value; }
        public double TextFontSize { get => Textblock.FontSize; set => Textblock.FontSize = value; }

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
    }
}
