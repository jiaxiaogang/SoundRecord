using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.CSharp;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Media.MediaProperties;
//using Windows.Devices.Sms;
//using Windows.Devices.Sensors;
//using Windows.Media.Devices;
//using Windows.Storage;
//using Windows.Media.Capture;
//using Windows.Media;
using Windows.UI.Xaml.Navigation;
//using SDKTemplate;
using System;
using Windows.Media;
using Windows.Media.Devices;
using Windows.Devices.Enumeration;
using Windows.Foundation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace LuYinJi
{
    /// <summary>
    /// 可独立使用或用于导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        MediaCapture micphone = new MediaCapture();
        StorageFile micphoneStorageFile;
        public MainPage()
        {
            this.InitializeComponent();
        }



        //点击开始录音后调用麦克风开始录音
        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            micphoneStorageFile = await KnownFolders.VideosLibrary.CreateFileAsync(DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss")+".mp3",CreationCollisionOption.GenerateUniqueName);
            MediaEncodingProfile recordProfile = null;
            recordProfile = MediaEncodingProfile.CreateM4a(AudioEncodingQuality.Auto);
            recordProfile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.Auto);
            await micphone.InitializeAsync();
            await micphone.StartRecordToStorageFileAsync(MediaEncodingProfile.CreateMp3(AudioEncodingQuality.Auto), this.micphoneStorageFile);
        }


        //停止录音后把录音文件存储到独立文件夹
        private async void Stop_Click(object sender, RoutedEventArgs e)
        {
           await micphone.StopRecordAsync();

           var stream = await micphoneStorageFile.OpenAsync(FileAccessMode.Read);
           micPlay.SetSource(stream, this.micphoneStorageFile.FileType);
           micPlay.AutoPlay = true;
           micPlay.Play();

        }
    }
}
