using System.Windows;
using System.Windows.Controls;

namespace WCL.FrameAnimations
{
    public class SlideRightToLeftAnimation : FrameAnimationBase
    {
        private double widthOffset = 0;
        private double widthOffsetStep;
        public override void AnimationTick(Canvas containerCanvas, FrameworkElement previousChild, FrameworkElement newChild)
        {
            widthOffset += widthOffsetStep;
            if (previousChild != null)
            {
                previousChild.Width = previousChild.Width - widthOffset;
            }
            newChild.Width = newChild.Width + widthOffset;
            if(newChild.Width >= containerCanvas.ActualWidth)
            {
                Stop();
            }
        }

        public override void PrepareAnimation(Canvas containerCanvas, FrameworkElement previousChild, FrameworkElement newChild)
        {
            if (previousChild != null)
            {
                Canvas.SetLeft(previousChild, 0);
                Canvas.SetTop(previousChild, 0);
                Canvas.SetBottom(previousChild, double.NaN);
                Canvas.SetRight(previousChild, double.NaN);
                previousChild.Width = containerCanvas.ActualWidth;
                previousChild.Height = containerCanvas.ActualHeight;
            }

            if (newChild != null)
            {
                Canvas.SetLeft(newChild, double.NaN);
                Canvas.SetTop(newChild, 0);
                Canvas.SetBottom(newChild, double.NaN);
                Canvas.SetRight(newChild, 0);
                newChild.Width = 0;
                newChild.Height = containerCanvas.ActualHeight;
            }
            widthOffsetStep = containerCanvas.ActualWidth / 60;
        }
    }
}
