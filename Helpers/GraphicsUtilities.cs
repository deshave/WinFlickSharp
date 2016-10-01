using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;

namespace Helpers
{
    public static class GraphicsUtilities
    {
        #region Public Static Methods  
        /// <summary>
        /// Calculates a thumbnail size maintaining aspect ratio.
        /// </summary>
        /// <param name="orig"><see cref="Size"/> object containing the size of the original image.</param>
        /// <param name="thumb"><see cref="Size"/> object containing the requested maximum dimensions of the thumbnail image.</param>
        /// <returns><see cref="Size"/> object containing the size of the new thumbnail image within the con
        /// straints supplied.</returns>
        public static Size GetThumbnailSize(Size orig, Size thumb)
        {
            double heightFactor = orig.Width / (double)orig.Height * thumb.Height;
            double widthFactor = orig.Height / (double)orig.Width * thumb.Width;
            if (heightFactor < thumb.Width)
            {
                thumb.Width = (int)heightFactor;
            }
            else
            {
                thumb.Height = (int)widthFactor;
            }
            return new Size(thumb.Width, thumb.Height);
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <param name="maintainaspect">Whether or not to maintain aspect.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height, Color bgcolor, bool maintainaspect = true)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.FillRectangle(new SolidBrush(bgcolor), new Rectangle(0, 0, destImage.Width, destImage.Height));

                if (maintainaspect)
                {
                    var newsize = GetThumbnailSize(image.Size, new Size(width, height));
                    destRect = new Rectangle((width - newsize.Width) / 2, (height - newsize.Height) / 2, newsize.Width, newsize.Height);
                }
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        /// <summary>
        /// Turns double buffering on or off in a control.
        /// </summary>
        /// <param name="control">The <see cref="Control"/> being sent a message about double buffering.</param>
        /// <param name="enable">A <see cref="bool"/> indicating whether to turn double buffering on or off.</param>
        public static void DoubleBuffered(this object control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, enable, null);
        }
        #endregion
    }
}