using System;
using System.Drawing.Imaging;
using BarlugoFX.Model.ImageTools;

namespace BarlugoFX.Controller
{
    public interface IIOManager
    {
        string InputFileName { get; }
        IImage LoadImageFromFile(Uri file);
        void ExportImage(IImage image, Uri file, ImageFormat format);
        void ExportJPEGWithQuality(IImage image, Uri file, int quality);
    }
}