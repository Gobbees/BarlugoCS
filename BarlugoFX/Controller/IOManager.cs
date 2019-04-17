using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection;
using BarlugoFX.Model.ImageTools;

namespace BarlugoFX.Controller
{
    public class IOManager : IIOManager
    {
        private string inputFileName;
        public string InputFileName 
        {
            get { return inputFileName; }
        }

        public IOManager()
        {
            inputFileName = null;
        }
        
        public IImage LoadImageFromFile(Uri file) 
        {
            if (!file.IsFile)
            {
                throw new Exception("File not valid");
            }
            inputFileName = file.ToString();
            inputFileName = inputFileName.Substring(0, inputFileName.IndexOf('.'));
            return new Model.ImageTools.Image(new Bitmap(file.AbsolutePath));
        }
        
        public void ExportImage(IImage image, Uri file, ImageFormat format) 
        {
            var bmp = new Bitmap(ImageUtils.ConvertImageToBitmap(image));
            var gBmp = Graphics.FromImage(bmp);
            gBmp.Dispose();
            bmp.Save(file.AbsolutePath, format);
        }
    }
}