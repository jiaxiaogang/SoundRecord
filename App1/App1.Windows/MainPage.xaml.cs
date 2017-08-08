using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace App1
{
    /// <summary>
    /// 可独立使用或用于导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public StorageFile storageFile = null;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = DateTime.Now.ToString("yyyy-MM-dd");
            StorageFolder folder = await KnownFolders.MusicLibrary.CreateFolderAsync(DateTime.Now.ToString("yyyy-MM-dd"),CreationCollisionOption.GenerateUniqueName);
            StorageFolder folder3 = await Package.Current.InstalledLocation.GetFolderAsync("Assets");
            //folder3 = await folder3.CreateFolderAsync("321");
            //StorageFolder folder2=await 


            var yourFolder = await StorageFolder.GetFolderFromPathAsync(Package.Current.InstalledLocation.Path + @"\Assets\123");
            //StorageFolder folder4 = await Package.Current.InstalledLocation.GetFolderAsync("ms-appx:///Assets/123");
            storageFile = await folder.CreateFileAsync(DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".mp3", CreationCollisionOption.GenerateUniqueName);
            StorageFile micphoneStorageFile;
        }









    }
}
