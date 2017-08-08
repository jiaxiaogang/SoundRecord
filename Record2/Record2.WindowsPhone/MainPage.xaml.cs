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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Record2
{
    /// <summary>
    /// 可独立使用或用于导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaCapture micphone = new MediaCapture();
        StorageFile file;
        bool b = false;
        RecordLogo logo;
        ObservableCollection<string> obs = new ObservableCollection<string>();
        StorageFolder folder;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                logo = new RecordLogo();
                GridPivot1.Children.Add(logo);
                Grid.SetColumn(logo, 1);
                Grid.SetRow(logo, 1);
                logo.Tapped += logo_Tapped;
                await micphone.InitializeAsync();
                //logo.PointerEntered += logo_PointerEntered;
                //logo.PointerExited += logo_PointerExited;
                folder = await KnownFolders.MusicLibrary.CreateFolderAsync("Record", CreationCollisionOption.OpenIfExists);
                //尝试将录音放到AudioFile目录试下
                //folder = await StorageFolder.GetFolderFromPathAsync(Package.Current.InstalledLocation.Path + @"\AudioFile");
                IReadOnlyList<StorageFile> files = await folder.GetFilesAsync();
                foreach (var item in files)
                {
                    obs.Add(item.Name);
                }
                listView.ItemsSource = obs;
            }
        }

        //void logo_PointerExited(object sender, PointerRoutedEventArgs e)
        //{
        //    logo.PointerExit();
        //}

        //void logo_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{
        //    logo.PointerEnter();
        //}

        async void logo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!b)
            {
                //StartRecord();
                file = await folder.CreateFileAsync(DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".wav", CreationCollisionOption.GenerateUniqueName);
                await micphone.StartRecordToStorageFileAsync(MediaEncodingProfile.CreateWav(AudioEncodingQuality.Auto), file);
                b = true;
                logo.StartFlash();
            }
            else
            {
                //StopRecord();
                await micphone.StopRecordAsync();
                var stream = await file.OpenAsync(FileAccessMode.Read);
                mediaElement.SetSource(stream, file.FileType);
                mediaElement.AutoPlay = false;
                b = false;
                logo.StopFlash();

                obs.Add(file.Name);
            }
        }
        private async void StartRecord()
        {
            if (!b)
            {
                file = await folder.CreateFileAsync(DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".wav", CreationCollisionOption.GenerateUniqueName);
                await micphone.StartRecordToStorageFileAsync(MediaEncodingProfile.CreateWav(AudioEncodingQuality.Auto), file);
                b = true;
                logo.StartFlash();




                //file = await folder.CreateFileAsync(DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"), CreationCollisionOption.GenerateUniqueName);
                //await micphone.StartRecordToStorageFileAsync(MediaEncodingProfile.CreateWav(AudioEncodingQuality.Auto), file);
                //b = true;
                //logo.StartFlash();

            }
            else
            {
                await new MessageDialog("已经开始了好不好?").ShowAsync();
            }
        }

        private async void StopRecord()
        {

            if (b)
            {
                await micphone.StopRecordAsync();
                var stream = await file.OpenAsync(FileAccessMode.Read);
                mediaElement.SetSource(stream, file.FileType);
                mediaElement.AutoPlay = false;
                b = false;
                logo.StopFlash();

                obs.Add(file.Name);
            }
            else
            {

                await new MessageDialog("还没开始好不好？").ShowAsync();
            }
        }

        private async void listView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string filename = (string)listView.SelectedItem;
            file = await folder.GetFileAsync(filename);
            var stream = await file.OpenAsync(FileAccessMode.Read);
            mediaElement.SetSource(stream, file.FileType);
            mediaElement.Play();
        }
    }
}
