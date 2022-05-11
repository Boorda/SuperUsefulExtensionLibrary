namespace SUELIB.ImageExtensions
{
    using System;
    using System.Drawing;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public static class ImageExtensions
    {
        /// <summary>
        /// Converts an Icon to an ImageSource.
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static ImageSource IconToImageSource(this Icon icon)
        {
            return Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public static ImageSource BitmapToImageSource(this Bitmap bitmap)
        {
            IntPtr handle = bitmap.GetHbitmap();
            try
            {
                ImageSource newSource = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                return newSource;
            }
            catch
            {
                return null;
            }

        }

    }
}
