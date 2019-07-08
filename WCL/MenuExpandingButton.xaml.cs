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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WCL
{
    /// <summary>
    /// Interaction logic for MenuExpandingButton.xaml
    /// </summary>
    public partial class MenuExpandingButton : UserControl
    {
        private bool expanded = false, animating = false;
        public event EventHandler Expanded, Collapsed;

        public MenuExpandingButton()
        {
            InitializeComponent();
            Holder.RenderTransform = new RotateTransform();
            Holder.RenderTransformOrigin = new Point(0.5, 0.5);
        }

        public Brush ForegroundColor { get => ForegroundRectangle.Fill; set => ForegroundRectangle.Fill = value; }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && !animating)
            {
                if (!expanded)
                {
                    DoubleAnimation doubleAnimation = new DoubleAnimation(0, 90, new Duration(TimeSpan.FromSeconds(0.5)));
                    doubleAnimation.Completed += ExpansionAnimation_Completed;
                    Holder.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation);
                    animating = true;
                }
                else
                {
                    DoubleAnimation doubleAnimation = new DoubleAnimation(90, 0, new Duration(TimeSpan.FromSeconds(0.5)));
                    doubleAnimation.Completed += ExpansionAnimation_Completed;
                    Holder.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation);
                    animating = true;
                }
            }
        }

        private void ExpansionAnimation_Completed(object sender, EventArgs e)
        {
            animating = false;
            expanded = !expanded;
            if (expanded)
            {
                Expanded?.Invoke(this, e);
            }
            else
            {
                Collapsed?.Invoke(this, e);
            }
        }
    }
}
