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

        private DispatcherTimer dispatcherTimer;

        protected FrameworkElement PreviousChild { get; private set; }
        protected FrameworkElement NewChild { get; private set; }
        protected Canvas Container { get; private set; }

        void IFrameAnimation.Start(FrameworkElement previousChild, FrameworkElement newChild, Canvas canvas)
        {
            if (dispatcherTimer is DispatcherTimer) throw new InvalidOperationException("Cannot start animation as it has already been started!");
            
            this.Container = canvas;
            this.NewChild = newChild;
            this.PreviousChild = previousChild;
            PrepareAnimation(canvas, previousChild, newChild);
            dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            dispatcherTimer.Tick += (o, e) => AnimationTick(canvas, newChild, previousChild);
            dispatcherTimer.Start();
        }

        public abstract void PrepareAnimation(Canvas containerCanvas, FrameworkElement previousChild, FrameworkElement newChild);

        public abstract void AnimationTick(Canvas containerCanvas, FrameworkElement previousChild, FrameworkElement newChild);

        protected void Stop()
        {
            AnimationEnded?.Invoke(this, new AnimationEndedArgs(PreviousChild, NewChild));
        }
    }
}
