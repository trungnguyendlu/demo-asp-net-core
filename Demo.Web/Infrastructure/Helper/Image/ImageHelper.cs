using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
//using System.Linq;
//using FreeImageAPI;

namespace FlexWebClient.Infrastructure.Helper
{
    public static class ImageHelper
    {
        //public static string Resize1(string path, string filename, int size)
        //{
        //    //only resize png and jpg image
        //    if (filename.EndsWith(".png") || filename.EndsWith(".jpg") || filename.EndsWith(".jpeg"))
        //    {
        //        using (var original = FreeImageBitmap.FromFile(path))
        //        {
        //            int width, height;
        //            if (original.Width > original.Height)
        //            {
        //                width = size;
        //                height = original.Height * size / original.Width;
        //            }
        //            else
        //            {
        //                width = original.Width * size / original.Height;
        //                height = size;
        //            }
        //            var resized = new FreeImageBitmap(original, width, height);

        //            var suffix = "_thumb";
        //            path = path.Insert(path.LastIndexOf('.'), suffix);
        //            filename = filename.Insert(filename.LastIndexOf('.'), suffix);

        //            using (var stream = File.OpenWrite(path))
        //            {
        //                resized.Save(stream, GetImageFormat(filename), GetSaveFlags(filename));
        //            }
        //        }
        //    }
        //    return $"/images/upload/{filename}";
        //}

        //public static string ResizeIcon1(string path, string filename, int size)
        //{
        //    //only resize png and jpg image
        //    if (filename.EndsWith(".png") || filename.EndsWith(".jpg") || filename.EndsWith(".jpeg"))
        //    {
        //        using (var original = FreeImageBitmap.FromFile(path))
        //        {
        //            var resized = new FreeImageBitmap(original, size, size);

        //            var suffix = $"-{size}x{size}";
        //            path = path.Insert(path.LastIndexOf('.'), suffix);
        //            filename = filename.Insert(filename.LastIndexOf('.'), suffix);

        //            using (var stream = File.OpenWrite(path))
        //            {
        //                resized.Save(stream, GetImageFormat1(filename), GetSaveFlags(filename));
        //            }
        //        }
        //    }
        //    return $"/images/upload/{filename}";
        //}

        //private static FREE_IMAGE_FORMAT GetImageFormat1(string filename)
        //{
        //    if (filename.EndsWith(".png"))
        //    {
        //        return FREE_IMAGE_FORMAT.FIF_PNG;
        //    }
        //    return FREE_IMAGE_FORMAT.FIF_JPEG;
        //}

        //private static FREE_IMAGE_SAVE_FLAGS GetSaveFlags(string filename)
        //{
        //    if (filename.EndsWith(".png"))
        //    {
        //        return FREE_IMAGE_SAVE_FLAGS.PNG_Z_NO_COMPRESSION;
        //    }
        //    return FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYGOOD | FREE_IMAGE_SAVE_FLAGS.JPEG_BASELINE;
        //}

        public static string Resize(string path, string filename, int size)
        {
            //only resize png and jpg image
            if (filename.EndsWith(".png") || filename.EndsWith(".jpg") || filename.EndsWith(".jpeg"))
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var original = new Bitmap(stream))
                    {
                        int width, height;
                        if (original.Width > original.Height)
                        {
                            width = size;
                            height = original.Height * size / original.Width;
                        }
                        else
                        {
                            width = original.Width * size / original.Height;
                            height = size;
                        }
                        var resized = new Bitmap(width, height);

                        using (var graphics = Graphics.FromImage(resized))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.DrawImage(original, 0, 0, width, height);


                            var suffix = "_thumb";
                            path = path.Insert(path.LastIndexOf('.'), suffix);
                            filename = filename.Insert(filename.LastIndexOf('.'), suffix);

                            resized.Save(path, GetImageFormat(filename));
                        }
                    }
                }
            }
            return $"/images/upload/{filename}";
        }

        public static string ResizeIcon(string path, string filename, int size)
        {
            //only resize png and jpg image
            if (filename.EndsWith(".png") || filename.EndsWith(".jpg") || filename.EndsWith(".jpeg"))
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var original = new Bitmap(stream))
                    {
                        var resized = new Bitmap(size, size);

                        using (var graphics = Graphics.FromImage(resized))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.DrawImage(original, 0, 0, size, size);

                            var suffix = $"-{size}x{size}";
                            path = path.Insert(path.LastIndexOf('.'), suffix);
                            filename = filename.Insert(filename.LastIndexOf('.'), suffix);

                            resized.Save(filename, GetImageFormat(filename));
                        }
                    }
                }
            }
            return $"/images/upload/{filename}";
        }

        private static ImageFormat GetImageFormat(string filename)
        {
            if (filename.EndsWith(".png"))
            {
                return ImageFormat.Png;
            }
            return ImageFormat.Jpeg;
        }

    }
}
