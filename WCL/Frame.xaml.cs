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
        private bool animating = false;
        private Stack<FrameworkElement> elementStack { get; } = new Stack<FrameworkElement>();
        public Frame()
        {
            InitializeComponent();
        }

        public void NavigateTo(FrameworkElement element, FrameAnimationBase animation = null)
        {
            if (element is null) throw new ArgumentNullException($"{nameof(element)} cannot be null!");

            if (animating) return;

            FrameworkElement previousChild = Container.Children.Count == 0 ? null : Container.Children[0] as FrameworkElement;
            FrameworkElement newChild = element;
            Container.Children.Add(newChild);

            if (previousChild != null) elementStack.Push(previousChild);
            if (animation != null)
            {
                animating = true;
                animation.AnimationEnded += FrameNavigation_AnimationEnded;
                (animation as IFrameAnimation).Start(previousChild, newChild, Container);
            }
            else
            {
                FrameNavigation_AnimationEnded(null, new FrameAnimationBase.AnimationEndedArgs(previousChild, newChild));
            }
        }

        public void NavigateBack(FrameAnimationBase animation = null)
        {
            if (Container.Children.Count == 0) throw new InvalidOperationException("There is no current element being displayed!");
            if (elementStack.Count == 0) throw new InvalidOperationException("Cannot navigate back as there are no more elements in the stack!");

            if (animating) return;

            FrameworkElement currentChild = Container.Children[0] as FrameworkElement;
            FrameworkElement newChild = elementStack.Pop();
            Container.Children.Add(newChild);
            if (animation != null)
            {
                animating = true;
                animation.AnimationEnded += FrameNavigation_AnimationEnded;
                (animation as IFrameAnimation).Start(currentChild, newChild, Container);
            }
            else
            {
                FrameNavigation_AnimationEnded(null, new FrameAnimationBase.AnimationEndedArgs(currentChild, newChild));
            }
        }

        public void NavigateBack<T>(FrameAnimationBase animation = null)
        {
            if (Container.Children.Count == 0) throw new InvalidOperationException("There is no current element being displayed!");
            if (elementStack.Count == 0) throw new InvalidOperationException("Cannot navigate back as there are no more elements in the stack!");

            if (animating) return;

            FrameworkElement newChild = null;
            while(elementStack.Count != 0)
            {
                var element = elementStack.Pop();
                if(element is T)
                {
                    newChild = element;
                    break;
                }
            }

            if (newChild == null) throw new InvalidOperationException($"There was no element in the stack of type {typeof(T)}");

            FrameworkElement currentChild = Container.Children[0] as FrameworkElement;
            Container.Children.Add(newChild);
            if (animation != null)
            {
                animating = true;
                animation.AnimationEnded += FrameNavigation_AnimationEnded;
                (animation as IFrameAnimation).Start(currentChild, newChild, Container);
            }
            else
            {
                FrameNavigation_AnimationEnded(null, new FrameAnimationBase.AnimationEndedArgs(currentChild, newChild));
            }
        }

        private void FrameNavigation_AnimationEnded(object sender, FrameAnimationBase.AnimationEndedArgs e)
        {
            if(e.PreviousElement != null)
            {
                Container.Children.Remove(e.PreviousElement);
            }

            Canvas.SetLeft(e.NewElement, 0);
            Canvas.SetTop(e.NewElement, 0);
            Canvas.SetBottom(e.NewElement, double.NaN);
            Canvas.SetRight(e.NewElement, double.NaN);
            e.NewElement.Width = Container.ActualWidth;
            e.NewElement.Height = Container.ActualHeight;

            Binding heightBinding = new Binding("ActualHeight");
            heightBinding.Source = Container;
            heightBinding.Mode = BindingMode.OneWay;
            e.NewElement.SetBinding(FrameworkElement.HeightProperty, heightBinding);
            Binding widthBinding = new Binding("ActualWidth");
            widthBinding.Source = Container;
            widthBinding.Mode = BindingMode.OneWay;
            e.NewElement.SetBinding(FrameworkElement.WidthProperty, widthBinding);

            animating = false;
        }
    }
}
