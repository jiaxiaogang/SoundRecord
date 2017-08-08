using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace StorageFile2
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

            //using{Stream stream=new StorageFile().open}

            //using{}
            //Stream stream = Application.Start;
            //StreamReader stremRead = new StreamReader(stream);
            ////StorageFile storageFile=S
            string str=DateTime.Now.ToString("yyyy-MM-dd");
            StorageFolder folder = await KnownFolders.MusicLibrary.CreateFolderAsync(DateTime.Now.ToString("yyyy-MM-dd"));
                
                
              storageFile  = await folder.CreateFileAsync(DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".mp3", CreationCollisionOption.GenerateUniqueName);
            StorageFile micphoneStorageFile;



        }
    }
}
