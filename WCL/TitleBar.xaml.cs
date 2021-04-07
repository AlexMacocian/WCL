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
        public static DependencyProperty CloseButtonVisibleProperty = DependencyProperty.Register(nameof(CloseButtonVisible), typeof(bool), typeof(TitleBar), new PropertyMetadata(true));
        public static DependencyProperty ResizeButtonVisibleProperty = DependencyProperty.Register(nameof(ResizeButtonVisible), typeof(bool), typeof(TitleBar), new PropertyMetadata(true));
        public static DependencyProperty MinimizeButtonVisibleProperty = DependencyProperty.Register(nameof(MinimizeButtonVisible), typeof(bool), typeof(TitleBar), new PropertyMetadata(true));
        public static DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(TitleBar));

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
        public string Title
        {
            get => (string)this.GetValue(TitleProperty);
            set => this.SetValue(TitleProperty, value);
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
        public bool CloseButtonVisible
        {
            get => (bool)this.GetValue(CloseButtonVisibleProperty);
            set => this.SetValue(CloseButtonVisibleProperty, value);
        }
        public bool ResizeButtonVisible
        {
            get => (bool)this.GetValue(ResizeButtonVisibleProperty);
            set => this.SetValue(ResizeButtonVisibleProperty, value);
        }
        public bool MinimizeButtonVisible
        {
            get => (bool)this.GetValue(MinimizeButtonVisibleProperty);
            set => this.SetValue(MinimizeButtonVisibleProperty, value);
        }

        public TitleBar()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == ResizeButtonVisibleProperty)
            {
                this.ManageResizeButtonsVisibility();
            }
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

        private void ManageResizeButtonsVisibility()
        {
            if (this.ResizeButtonVisible is false)
            {
                this.RestoreButton.Visibility = Visibility.Collapsed;
                this.MaximizeButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (this.WindowState == WindowState.Normal)
                {
                    this.RestoreButton.Visibility = Visibility.Collapsed;
                    this.MaximizeButton.Visibility = Visibility.Visible;
                }
                else
                {
                    this.RestoreButton.Visibility = Visibility.Visible;
                    this.MaximizeButton.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
