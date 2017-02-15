using RBSectorUWPBusinessLogic.Service;
using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RBSector.ImagePages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImageViewPage : ContentDialog
    {
        private ObservableCollection<ImageViewModel> ImagesByte;
        private ImageService srv_image;
        private ImageViewModel image;
        public ImageViewPage()
        {
            this.InitializeComponent();
            ImagesByte = new ObservableCollection<ImageViewModel>();
            BitmapImage s;
             srv_image = new ImageService();
           
            Loading += ImageViewPage_Loading;
            Loading(null, null);
        }

        private async void ImageViewPage_Loading(object sender, EventArgs e)
        {
            foreach (var a in await srv_image.LoadLacalImages())
            {
                ImagesByte.Add(a);
            }
            
        }

        public event EventHandler Loading;

        private void btn_Choose_Click(object sender, RoutedEventArgs e)
        {
            ProductService.SelectedImageForProduct = image;
            Hide();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
        private void GridView_ItemClickOnImage(object sender, ItemClickEventArgs e)
        {
            image = (ImageViewModel)e.ClickedItem;
        }
    }
}
