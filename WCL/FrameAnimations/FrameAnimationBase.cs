using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WCL.FrameAnimations
{
    public abstract class FrameAnimationBase : IFrameAnimation
    {
        public class AnimationEndedArgs
        {
            public AnimationEndedArgs(FrameworkElement previousElement, FrameworkElement newElement)
            {
                this.PreviousElement = previousElement;
                this.NewElement = newElement;
            }
            public FrameworkElement PreviousElement { get; }
            public FrameworkElement NewElement { get; }
        }

        public event EventHandler<AnimationEndedArgs> AnimationEnded;

        protected FrameworkElement PreviousChild { get; private set; }
        protected FrameworkElement NewChild { get; private set; }
        protected Canvas Container { get; private set; }

        void IFrameAnimation.Start(FrameworkElement previousChild, FrameworkElement newChild, Canvas canvas)
        {            
            this.Container = canvas;
            this.NewChild = newChild;
            this.PreviousChild = previousChild;
            StartAnimation();
        }

        public abstract void StartAnimation();

        protected void Stop()
        {
            AnimationEnded?.Invoke(this, new AnimationEndedArgs(PreviousChild, NewChild));
        }
    }
}
