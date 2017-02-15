using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace RBSectorUWPBusinessLogic.Service
{
    public class ImageService
    {
        #region[CTR]
        public ImageService()
        {
        }
        #endregion

        #region[Products]
        public void AssignImageToProduct(ref ProductViewModel product, BitmapImage image)
        {

        }
        #endregion

        #region[Initi]
        public async Task<ObservableCollection<ImageViewModel>> LoadLacalImages(string path = null)
        {
            ObservableCollection<ImageViewModel> images = new ObservableCollection<ImageViewModel>();

            var files = await GetFilesAsync();

            foreach (var pt in files)
            {
                byte[] bt = ConvertToByteWithPath(pt.Path);
                BitmapImage im = await GetImage(bt);
                im = await ResizedImage(im, 150, 150);

                images.Add(new ImageViewModel() { bitmapImage = im, BytesImage = bt });
            }
            return images;
        }
        public async Task<BitmapImage> GetImage(Byte[] bytes)
        {
            if (bytes == null) return new BitmapImage();
            return await ByteToImageAsync(bytes);
            // return Convert(bytes);
        }
        #endregion

        #region[Load files with folder]
        private async static Task<IReadOnlyList<StorageFile>> GetFilesAsync()
        {
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets").AsTask().ConfigureAwait(false);
            //Images
            var imagesFolder = await folder.GetFolderAsync("Images").AsTask().ConfigureAwait(false);

            return await imagesFolder.GetFilesAsync().AsTask().ConfigureAwait(false);
        }
        #endregion

        #region[File to byte]
        public byte[] ConvertToByteWithPath(string path)
        {

            byte[] image = default(byte[]);
            if (string.IsNullOrEmpty(path)) return image;
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    image = reader.ReadBytes((int)stream.Length);
                }
            }
            catch (Exception ex) { string error = ex.Message; }
            return image;
        }

        #endregion

        #region[Byte to BitmapImage]
        public BitmapImage ByteToImage(object value)
        {
            if (value == null || !(value is byte[]))
                return null;
            using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes((byte[])value);
                    writer.StoreAsync().GetResults();
                }

                var image = new BitmapImage();
                image.SetSource(ms);
                return image;
            }
        }

        private async Task<BitmapImage> ByteToImageAsync(byte[] bytes)
        {
            BitmapImage im = new BitmapImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(bytes.AsBuffer());
                stream.Seek(0);
                await im.SetSourceAsync(stream);
            }
            return im;
        }

        #endregion

        #region[Resize Image]
        public async Task<BitmapImage> ResizedImage(BitmapImage sourceImage, int maxWidth, int maxHeight)
        {
            var origHeight = sourceImage.PixelHeight;
            var origWidth = sourceImage.PixelWidth;
            var ratioX = maxWidth / (float)origWidth;
            var ratioY = maxHeight / (float)origHeight;
            var ratio = Math.Min(ratioX, ratioY);
            var newHeight = (int)(origHeight * ratio);
            var newWidth = (int)(origWidth * ratio);

            sourceImage.DecodePixelWidth = newWidth;
            sourceImage.DecodePixelHeight = newHeight;

            return sourceImage;
        }
        #endregion


        public byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string ByteToStringForDB(byte[] bytes)
        {
            string result = string.Empty;
            if (bytes.Count() < 0) return result;
            result = string.Join("#", bytes);
            return result;
        }
        public static byte[] StringToByteForDB(string bt)
        {
            string[] result = bt.Split('#');
            int count = result.Length;
            byte[] bytes = new byte[count];

            for (int i = 0; i < count; i++)
            {
                byte bts;
                if (byte.TryParse(result[i], out bts))
                    bytes[i] = bts;
            }

            return bytes;
        }
    }
    public enum ImageType { IM_Product };
}
