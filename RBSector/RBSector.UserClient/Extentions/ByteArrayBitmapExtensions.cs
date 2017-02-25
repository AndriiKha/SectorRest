using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace RBSector.UserClient.Extentions
{
    internal static class ByteArrayBitmapExtensions
    {
        public static async Task<byte[]> AsByteArray(this StorageFile file)
        {
            var fileStream = await file.OpenAsync(FileAccessMode.Read);
            var reader = new DataReader(fileStream.GetInputStreamAt(0));
            await reader.LoadAsync((uint)fileStream.Size);

            var pixels = new byte[fileStream.Size];

            reader.ReadBytes(pixels);

            return pixels;
        }

        public static byte[] AsByteArray(this WriteableBitmap bitmap)
        {
            using (var stream = bitmap.PixelBuffer.AsStream())
            {
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static BitmapImage AsBitmapImage(this byte[] byteArray)
        {
            if (byteArray == null) return null;
            using (var stream = new InMemoryRandomAccessStream())
            {
                stream.WriteAsync(byteArray.AsBuffer()).GetResults();
                var image = new BitmapImage();
                stream.Seek(0);
                image.SetSource(stream);
                return image;
            }
        }

        private static async Task<BitmapImage> AsBitmapImage(this StorageFile file)
        {
            var stream = await file.OpenAsync(FileAccessMode.Read);
            var bitmapImage = new BitmapImage();
            await bitmapImage.SetSourceAsync(stream);
            return bitmapImage;
        }
    }
}
