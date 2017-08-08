using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace Record2
{
    public sealed partial class RecordLogo : UserControl
    {
        //double CenterXY;
        public RecordLogo()
        {
            this.InitializeComponent();
        }


        //public void PointerEnter()
        //{
        //    LogoBack.Visibility = Visibility.Visible;
        //}
        //public void PointerExit()
        //{
        //    LogoBack.Visibility = Visibility.Collapsed;
        //}
        public void StartFlash()
        {
            double dou = (int)Logo.ActualWidth / 2;

            scaleLogoBackOpacityBig.CenterX = dou;
            scaleLogoBackOpacityBig.CenterY = dou;
            scaleLogoBackOpacitySmall.CenterX = dou;
            scaleLogoBackOpacitySmall.CenterY = dou;

            sb1.Begin();
            LogoBackOpacityBig.Visibility = Visibility.Visible;
            LogoBackOpacitySmall.Visibility = Visibility.Visible;
        }
        public void StopFlash()
        {
            sb1.Stop();
            LogoBackOpacityBig.Visibility = Visibility.Collapsed;
            LogoBackOpacitySmall.Visibility = Visibility.Collapsed;
        }
    }
}




