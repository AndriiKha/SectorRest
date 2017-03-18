using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace RBSectorUWPBusinessLogic.ViewModel
{
    public class ImageViewModel
    {
        public BitmapImage bitmapImage { get; set; }
        public byte[] BytesImage { get; set; }
        public string IM_Name { get; set; }
        public string IM_Type { get; set; }
        public string ByteString { get; set; }
    }
}
