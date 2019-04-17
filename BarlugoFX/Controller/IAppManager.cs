using System;
using System.Collections;
using System.Drawing.Imaging;
using BarlugoFX.Model.ImageTools;

namespace BarlugoFX.Controller
{
    public interface IAppManager
    {
        /// <summary>
        /// The manager's image.
        /// </summary>
        IImage Image { get; }
        /// <summary>
        /// Sets the exposure
        /// </summary>
        double Exposure { set; }
        /// <summary>
        /// Sets the contrast
        /// </summary>
        double Contrast { set; }
        /// <summary>
        /// Sets the brightness
        /// </summary>
        double Brightness { set; }
        /// <summary>
        /// Sets the cropper. The array must be like [x1,y1,x2,y2].
        /// </summary>
        int[] Cropper { set; }
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
    }
}