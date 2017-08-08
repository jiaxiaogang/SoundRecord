using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Record
{
    /// <summary>
    /// 可独立使用或用于导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        /// 

        private ObservableCollection<string> obs = new ObservableCollection<string>();
        bool BtnColVis = true;
        MediaCapture micphone = new MediaCapture();
        StorageFile micphoneStorageFile;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            if (e.NavigationMode == NavigationMode.New)
            {
                //麦克风必须初始化并且只能初始化一次;
                await micphone.InitializeAsync();
            }
        }


        private void Image_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            LogoBack.Visibility = Visibility.Visible;
        }
        private void Image_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (BtnColVis)
            {
                LogoBack.Visibility = Visibility.Collapsed;
            }
        }
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StartRecord();
        }
        private void LogoBack_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StartRecord();
        }


        private async void StartRecord()
        {
            if (BtnColVis)
            {
                LogoBack.Visibility = Visibility.Visible;
                LogoBack.Opacity = 1;
                LogoBackOpacitySmall.Visibility = Visibility.Visible;
                LogoBackOpacityBig.Visibility = Visibility.Visible;
                Logo.Visibility = Visibility.Collapsed;
                StopLogo.Visibility = Visibility.Visible;
                BtnColVis = false;
                sb1.Begin();


                
                //开始录音
                //micphoneStorageFile = await KnownFolders.MusicLibrary.CreateFileAsync(DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".mp3", CreationCollisionOption.GenerateUniqueName);
                //micphoneStorageFile = await KnownFolders.RemovableDevices.CreateFileAsync(DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".mp3", CreationCollisionOption.GenerateUniqueName);
                StorageFolder folder2 = await KnownFolders.MusicLibrary.CreateFolderAsync(DateTime.Now.ToString("yyyy-MM-dd"), CreationCollisionOption.OpenIfExists);
                micphoneStorageFile = await folder2.CreateFileAsync(DateTime.Now.ToString("yyyy=MM=dd hh=mm=ss") + ".mp3", CreationCollisionOption.GenerateUniqueName);

                await micphone.StartRecordToStorageFileAsync(MediaEncodingProfile.CreateMp3(AudioEncodingQuality.Auto), this.micphoneStorageFile);
            }
        }
        private async void StopRecord()
        {
            LogoBack.Visibility = Visibility.Collapsed;
            LogoBack.Opacity = 0.4;
            LogoBackOpacitySmall.Visibility = Visibility.Collapsed;
            LogoBackOpacityBig.Visibility = Visibility.Collapsed;
            Logo.Visibility = Visibility.Visible;
            StopLogo.Visibility = Visibility.Collapsed;
            BtnColVis = true;
            sb1.Stop();



            //停止录音
            await micphone.StopRecordAsync();
            var stream = await micphoneStorageFile.OpenAsync(FileAccessMode.Read);
            micPlay.SetSource(stream, this.micphoneStorageFile.FileType);



            obs.Add(micphoneStorageFile.Name);
            listView.ItemsSource = obs;
        }

               
           

                //    lists.Add(micphoneStorageFile.Name );
                //    allAudio.List = new List<Audio>();
                //    allAudio.List.Add(new Audio() { FileName = micphoneStorageFile.Name });
            
        //private async void StopRecord()
        //{
        //    if (!BtnColVis)
        //    {
        //        LogoBack.Visibility = Visibility.Collapsed;
        //        LogoBack.Opacity = 0.4;
        //        LogoBackOpacity.Visibility = Visibility.Collapsed;
        //        Logo.Visibility = Visibility.Visible;
        //        StopLogo.Visibility = Visibility.Collapsed;
        //        BtnColVis = true;
        //        sb1.Stop();



        //        //停止录音
        //        await micphone.StopRecordAsync();
        //        var stream = await micphoneStorageFile.OpenAsync(FileAccessMode.Read);
        //        micPlay.SetSource(stream, this.micphoneStorageFile.FileType);
        //        micPlay.AutoPlay = false;
        //       }
        //    else
        //    {
        //        BtnColVis = false;
        //    }
            
        //    //lists.Add(micphoneStorageFile.Name );
        //    //allAudio.List = new List<Audio>();
        //    allAudio.List.Add(new Audio() { FileName = micphoneStorageFile.Name });
        //    listView.ItemsSource = allAudio.List;
        //}

        private void StopLogo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StartRecord();
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            StartRecord();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            StopRecord();
        }
    }
}