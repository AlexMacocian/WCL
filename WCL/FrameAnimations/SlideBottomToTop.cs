using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WCL.FrameAnimations
{
    public class SlideBottomToTop : FrameAnimationBase
    {
        Duration animationDuration = TimeSpan.FromMilliseconds(200);
        public SlideBottomToTop(TimeSpan? duration = null)
        {
            if (duration != null)
            {
                animationDuration = (TimeSpan)duration;
            }
        }

        public override void StartAnimation()
        {
            Storyboard sb = new Storyboard();

            if (PreviousChild != null)
            {
                PreviousChild.Width = Container.ActualWidth;
                PreviousChild.Height = Container.ActualHeight;
                Canvas.SetLeft(PreviousChild, 0);
                Canvas.SetTop(PreviousChild, 0);
                Canvas.SetRight(PreviousChild, double.NaN);
                Canvas.SetBottom(PreviousChild, double.NaN);
                DoubleAnimation prevAnim = new DoubleAnimation()
                {
                    From = 0,
                    To = -Container.ActualHeight,
                    Duration = animationDuration,
                    AccelerationRatio = 0.5,
                    DecelerationRatio = 0.5,
                };
                Storyboard.SetTarget(prevAnim, PreviousChild);
                Storyboard.SetTargetProperty(prevAnim, new PropertyPath("(Canvas.Top)"));
                sb.Children.Add(prevAnim);
            }

            NewChild.Width = Container.ActualWidth;
            NewChild.Height = Container.ActualHeight;
            Canvas.SetLeft(NewChild, 0);
            Canvas.SetTop(NewChild, Container.ActualHeight);
            Canvas.SetRight(NewChild, double.NaN);
            Canvas.SetBottom(NewChild, double.NaN);
            DoubleAnimation newAnim = new DoubleAnimation()
            {
                From = Container.ActualHeight,
                To = 0,
                Duration = animationDuration,
                AccelerationRatio = 0.5,
                DecelerationRatio = 0.5,
            };
            Storyboard.SetTarget(newAnim, NewChild);
            Storyboard.SetTargetProperty(newAnim, new PropertyPath("(Canvas.Top)"));
            sb.Children.Add(newAnim);
            sb.Completed += Sb_Completed;
            sb.Begin();
        }

        private void Sb_Completed(object sender, EventArgs e)
        {
            Stop();
        }
    }
}
