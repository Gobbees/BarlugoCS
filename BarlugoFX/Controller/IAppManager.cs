using System;
using System.Drawing.Imaging;
using BarlugoFX.Model.ImageTools;

namespace BarlugoFX.Controller
{
    public interface IAppManager
    {
        /// <summary>
        /// The manager's image.
        /// </summary>
        IImage Image { get; set; }
        double Exposure { set; }
        double Contrast { set; }
        double Brightness { set; }
        double WhiteBalance { set; }
        double Saturation { set; }
        double Hue { set; }
        double BlackAndWhite { set; }
        double Vibrance { set; }
        /// <summary>
        /// Loads a new file from path.
        /// </summary>
        /// <param name="file">the file path</param>
        /// <exception cref="ArgumentNullException">if the param is null</exception>
        void LoadNewImage(Uri file);
        /// <summary>
        /// Exports an image in the requested format.
        /// </summary>
        /// <param name="path">the output file path</param>
        /// <param name="format">the output format</param>
        void ExportImage(Uri path, ImageFormat format);
        /// <summary>
        /// Exports an image in JPEG format with the requested quality.
        /// </summary>
        /// <param name="path">the output file path</param>
        /// <param name="quality">the quality</param>
        void ExportImage(Uri path, int quality);
    }
}