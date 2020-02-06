using System.Windows;
using System.Windows.Controls;

namespace WCL.FrameAnimations
{
    interface IFrameAnimation
    {
        void Start(FrameworkElement previousChild, FrameworkElement newChild, Canvas canvas);
    }
}
