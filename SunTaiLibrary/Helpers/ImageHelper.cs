using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SunTaiLibrary
{
    public static class ImageHelper
    {
        /// <summary>
        /// 根据文件路径加载图片资源，这样可以解决文件一直占用的问题。
        /// </summary>
        /// <param name="imageFilePath">图片文件</param>
        /// <param name="imageType">加载方式，它会影响图片资源加载的大小</param>
        /// <exception cref="FileNotFoundException">图片文件不存在</exception>
        /// <returns>图片资源</returns>
        public static ImageSource FromPath(string imageFilePath, ImageType imageType)
        {
            if (!File.Exists(imageFilePath)) throw new FileNotFoundException("Image file not found.", imageFilePath);

            using Stream fstream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Delete);
            return FromStream(fstream, imageType);
        }

        public static ImageSource FromStream(Stream imageStream, ImageType imageType)
        {
            var size = GetImageSize(imageStream);
            imageStream.Position = 0;

            var bitmap = new BitmapImage();
            bitmap.BeginInit();

            bitmap.DecodePixelWidth = imageType switch
            {
                // 最大不能超过屏幕。
                ImageType.Background => (int)Math.Min(SystemParameters.PrimaryScreenWidth, size.Width),
                // 素材大小。
                ImageType.Display => (int)Math.Min(1280, size.Width),
                // 小型预览图
                ImageType.Thumbnail => 240,
                _ => 1024
            };

            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.StreamSource = imageStream;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;

            static Size GetImageSize(Stream fs)
            {
                using var image = System.Drawing.Image.FromStream(fs);
                return new Size(image.Width, image.Height);
            }
        }

        public static BitmapImage ToBitmapImage(this System.Drawing.Bitmap bmp)
        {
            var ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public static BitmapImage ToBitmapImage(this byte[] bytes)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(bytes);
            image.EndInit();
            return image;
        }

        public static BitmapImage ToBitmapImage(string filePath, int? decodePixelWidth = null)
        {
            using var fs = File.OpenRead(filePath);
            var ms = new MemoryStream();
            fs.CopyTo(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            if (decodePixelWidth.HasValue)
            {
                image.DecodePixelWidth = decodePixelWidth.Value;
            }
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public static BitmapImage ToBitmapImage(FileInfo file, int? decodePixelWidth = null)
        {
            using var fs = file.OpenRead();
            var ms = new MemoryStream();
            fs.CopyTo(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            if (decodePixelWidth.HasValue)
            {
                image.DecodePixelWidth = decodePixelWidth.Value;
            }
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public static BitmapImage ToBitmapImage(Uri uri, int? decodePixelWidth = null)
        {
            var image = new BitmapImage();
            if (decodePixelWidth.HasValue)
            {
                image.DecodePixelWidth = decodePixelWidth.Value;
            }
            image.BeginInit();
            image.UriSource = uri;
            image.EndInit();
            return image;
        }
    }

    public enum ImageType
    {
        Background,
        Thumbnail,
        Display
    }
}