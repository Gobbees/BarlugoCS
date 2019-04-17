using System;
using System.Drawing.Imaging;
using BarlugoFX.Model.ImageTools;

namespace BarlugoFX.Controller
{
    /// <summary>
    /// A component that allows I/O operations
    /// </summary>
    public interface IIOManager
    {
        /// <summary>
        /// Returns the input file name
        /// </summary>
        string InputFileName { get; }
        /// <summary>
        /// Loads an image from the input path
        /// </summary>
        /// <param name="file">the file path</param>
        /// <returns>the image</returns>
        IImage LoadImageFromFile(Uri file);
        /// <summary>
        /// Exports an image in the requested format to the specified uri
        /// </summary>
        /// <param name="image">the image</param>
        /// <param name="file">the output file path</param>
        /// <param name="format">the export format</param>
        void ExportImage(IImage image, Uri file, ImageFormat format);
    }
}