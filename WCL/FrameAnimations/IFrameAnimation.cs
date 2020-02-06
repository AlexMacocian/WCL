using System.Windows;
using System.Windows.Controls;

namespace WCL.FrameAnimations
{
    interface IFrameAnimation
    {
        void Start(FrameworkElement previousElement, FrameworkElement newElement, Canvas canvas);
    }
}
