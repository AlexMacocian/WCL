using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for WindowTitleBar.xaml
    /// </summary>
    public partial class TitleBar : UserControl
    {
        private WindowState windowState = WindowState.Normal;

        public event EventHandler Clicked, CloseButtonClicked, RestoreButtonClicked, MaximizeButtonClicked, MinimizeButtonClicked;
        /// <summary>
        /// State of the window. Affects the maximize and restore down buttons.
        /// </summary>
        public WindowState WindowState
        {
            get => windowState;
            set
            {
                windowState = value;
                if(windowState == WindowState.Maximized)
                {
                    RestoreButton.Visibility = Visibility.Visible;
                    MaximizeButton.Visibility = Visibility.Hidden;
                }
                else if(windowState == WindowState.Normal)
                {
                    RestoreButton.Visibility = Visibility.Hidden;
                    MaximizeButton.Visibility = Visibility.Visible;
                }
            }
        }
        /// <summary>
        /// String that is displayed on the left side of the titlebar.
        /// </summary>
        public string Text
        {
            get => TitleBlock.Text;
            set => TitleBlock.Text = value;
        }
        public UIElement Icon
        {
            get => IconContainer.Children.Count > 0 ? IconContainer.Children[0] : null;
            set
            {
                IconContainer.Children.Clear();
                IconContainer.Children.Add(value);
            }
        }

        public TitleBar()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseButtonClicked?.Invoke(this, e);
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            RestoreButtonClicked?.Invoke(this, e);
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            MaximizeButtonClicked?.Invoke(this, e);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            MinimizeButtonClicked?.Invoke(this, e);
        }

        private void Container_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                Clicked?.Invoke(this, e);
            }
        }
    }
}
