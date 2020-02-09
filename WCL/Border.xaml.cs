using System;
using System.Windows;
using System.Windows.Controls;

namespace WCL
{
    /// <summary>
    /// Interaction logic for Border.xaml
    /// </summary>
    public partial class Border : UserControl
    {
        private bool active = false;
        public enum ResizeDirection
        {
            Left = 61441,
            Right = 61442,
            Top = 61443,
            TopLeft = 61444,
            TopRight = 61445,
            Bottom = 61446,
            BottomLeft = 61447,
            BottomRight = 61448,
        }
        /// <summary>
        /// Called when a resize action has been detected.
        /// </summary>
        public event EventHandler<ResizeDirection> OnResize;
        /// <summary>
        /// If false, the border doesn't perform the resize action.
        /// </summary>
        public bool Active
        {
            get => active;
            set => active = value;
        }
        public Border()
        {
            InitializeComponent();
        }
        private void Resize(object sender, RoutedEventArgs e)
        {
            if (active)
            {
                var clickedShape = sender as System.Windows.Controls.Button;
                switch (clickedShape.Name)
                {
                    case "TopBorder":
                        OnResize?.Invoke(this, ResizeDirection.Top);
                        break;
                    case "RightBorder":
                        OnResize?.Invoke(this, ResizeDirection.Right);
                        break;
                    case "BottomBorder":
                        OnResize?.Invoke(this, ResizeDirection.Bottom);
                        break;
                    case "LeftBorder":
                        OnResize?.Invoke(this, ResizeDirection.Left);
                        break;
                    case "TopLeftCorner":
                        OnResize?.Invoke(this, ResizeDirection.TopLeft);
                        break;
                    case "TopRightCorner":
                        OnResize?.Invoke(this, ResizeDirection.TopRight);
                        break;
                    case "BottomRightCorner":
                        OnResize?.Invoke(this, ResizeDirection.BottomRight);
                        break;
                    case "BottomLeftCorner":
                        OnResize?.Invoke(this, ResizeDirection.BottomLeft);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
