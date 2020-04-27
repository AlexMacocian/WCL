using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

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
    ///     <MyNamespace:ExtendedViewer/>
    ///
    /// </summary>
    public class ExtendedViewer : ContentControl
    {
        static ExtendedViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedViewer), new FrameworkPropertyMetadata(typeof(ExtendedViewer)));
        }

        public ExtendedViewer()
        {
            this.Loaded += ExtendedViewer_Loaded;
            this.PreviewMouseDown += ScrollViewer_PreviewMouseDown;
            this.PreviewMouseWheel += ScrollViewer_PreviewMouseWheel;
            this.PreviewMouseMove += ScrollViewer_PreviewMouseMove;
        }

        private void ExtendedViewer_Loaded(object sender, RoutedEventArgs e)
        {
            (this.Content as FrameworkElement).RenderTransform = new TransformGroup()
            {
                Children = new TransformCollection
                {
                    new TranslateTransform(),
                    new ScaleTransform()
                }
            };
        }

        public event EventHandler<Point> OffsetChanged;
        public event EventHandler<double> ZoomChanged;

        private bool panning = false, dragging = false;
        private Point draggingMousePosition;
        private Point panningMousePosition, newPanningPosition;
        private DispatcherTimer panningTimer;
        private double zoom = 100;

        private TranslateTransform TranslateTransform { get => ((this.Content as FrameworkElement).RenderTransform as TransformGroup).Children[0] as TranslateTransform; }
        private ScaleTransform ScaleTransform { get => ((this.Content as FrameworkElement).RenderTransform as TransformGroup).Children[1] as ScaleTransform; }

        public double Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                UpdateZoom();
            }
        }

        public Point Offset
        {
            get => new Point(TranslateTransform.X, TranslateTransform.Y);
            set
            {
                TranslateTransform.X = value.X;
                TranslateTransform.Y = value.Y;
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            if (e.Delta > 0 && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                zoom += zoom / 10;
            }
            else if (e.Delta < 0 && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                zoom -= zoom / 10;
            }
            UpdateZoom();
        }

        private void ScrollViewer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                panning = true;
                panningMousePosition = e.GetPosition(this);
                newPanningPosition = panningMousePosition;
                panningTimer = new DispatcherTimer(DispatcherPriority.Render)
                {
                    Interval = TimeSpan.FromMilliseconds(11)
                };
                panningTimer.Tick += ScrollViewer_Tick;
                panningTimer.IsEnabled = true;
            }
            else if (e.ChangedButton == MouseButton.Left)
            {
                dragging = true;
                draggingMousePosition = e.GetPosition(this);
            }
        }

        private void ScrollViewer_Tick(object sender, EventArgs e)
        {
            var offsetX = newPanningPosition.X - panningMousePosition.X;
            var offsetY = newPanningPosition.Y - panningMousePosition.Y;
            TranslateTransform.X -= offsetX / 10 / (zoom / 100);
            TranslateTransform.Y -= offsetY / 10 / (zoom / 100);
            OffsetChanged?.Invoke(this, new Point(TranslateTransform.X, TranslateTransform.Y));
        }

        private void ScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!panning && !dragging)
            {
                return;
            }

            if (panning)
            {
                if (e.MiddleButton == MouseButtonState.Pressed)
                {
                    newPanningPosition = e.GetPosition(this);
                    return;
                }
                else
                {
                    panning = false;
                    panningTimer.IsEnabled = false;
                }
            }
            else if (dragging)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var newPosition = e.GetPosition(this);
                    var offsetX = newPosition.X - draggingMousePosition.X;
                    var offsetY = newPosition.Y - draggingMousePosition.Y;
                    TranslateTransform.X += offsetX / (zoom / 100);
                    TranslateTransform.Y += offsetY / (zoom / 100);
                    OffsetChanged?.Invoke(this, new Point(TranslateTransform.X, TranslateTransform.Y));
                    draggingMousePosition = newPosition;
                }
                else
                {
                    dragging = false;
                }
            }
        }

        private void UpdateZoom()
        {
            ScaleTransform.ScaleX = zoom / 100;
            ScaleTransform.ScaleY = zoom / 100;
            ZoomChanged?.Invoke(this, zoom);
        }
    }
}
