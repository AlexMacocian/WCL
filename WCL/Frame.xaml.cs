using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WCL.FrameAnimations;

namespace WCL
{
    /// <summary>
    /// Interaction logic for Frame.xaml
    /// </summary>
    public partial class Frame : UserControl
    {
        private Stack<FrameworkElement> elementStack { get; } = new Stack<FrameworkElement>();
        public Frame()
        {
            InitializeComponent();
        }

        public void NavigateTo(FrameworkElement element, FrameAnimationBase animation)
        {
            if (element is null) throw new ArgumentNullException($"{nameof(element)} cannot be null!");

            FrameworkElement previousChild = Container.Children.Count == 0 ? null : Container.Children[0] as FrameworkElement;
            FrameworkElement newChild = element;
            Container.Children.Add(newChild);
            (animation as IFrameAnimation).Start(previousChild, newChild, Container);
            animation.AnimationEnded += FrameNavigation_AnimationEnded; 
        }

        public void NavigateBack(FrameAnimationBase animation)
        {
            if (Container.Children.Count == 0) throw new InvalidOperationException("There is no current element being displayed!");
            if (elementStack.Count == 0) throw new InvalidOperationException("Cannot navigate back as there are no more elements in the stack!");

            FrameworkElement currentChild = Container.Children[0] as FrameworkElement;
            FrameworkElement newChild = elementStack.Pop();
            Container.Children.Add(newChild);
            (animation as IFrameAnimation).Start(currentChild, newChild, Container);
            animation.AnimationEnded += FrameNavigation_AnimationEnded;
        }

        private void FrameNavigation_AnimationEnded(object sender, FrameAnimationBase.AnimationEndedArgs e)
        {
            if(e.PreviousElement != null)
            {
                Container.Children.Remove(e.PreviousElement);
                elementStack.Push(e.PreviousElement);
            }
            Binding heightBinding = new Binding("ActualHeight");
            heightBinding.Source = Container;
            heightBinding.Mode = BindingMode.OneWay;
            e.NewElement.SetBinding(FrameworkElement.HeightProperty, heightBinding);
            Binding widthBinding = new Binding("ActualWidth");
            widthBinding.Source = Container;
            widthBinding.Mode = BindingMode.OneWay;
            e.NewElement.SetBinding(FrameworkElement.WidthProperty, widthBinding);
        }
    }
}
