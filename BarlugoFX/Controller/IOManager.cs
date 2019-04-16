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
                Console.WriteLine("File not valid");
            }
            inputFileName = file.ToString();
            inputFileName = inputFileName.Substring(0, inputFileName.IndexOf('.'));
            Console.WriteLine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + file.AbsolutePath);
            return new Model.ImageTools.Image(new Bitmap(file.AbsolutePath));
        }
        
        public void ExportImage(IImage image, Uri file, ImageFormat format) 
        {
            var bmp = new Bitmap(ImageUtils.ConvertImageToBitmap(image));
            var gBmp = Graphics.FromImage(bmp);
            gBmp.Dispose();
            bmp.Save(file.AbsolutePath, format);
        }
        //found on microsoft docs
        public void ExportJPEGWithQuality(IImage image, Uri file, int quality)
        {
            var bmp = new Bitmap(ImageUtils.ConvertImageToBitmap(image));
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            Encoder myEncoder =  Encoder.Quality;  
            EncoderParameters myEncoderParameters = new EncoderParameters(1);  
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);  
            myEncoderParameters.Param[0] = myEncoderParameter;  
            bmp.Save(file.ToString(), jpgEncoder, myEncoderParameters);  
        }
        //found on microsoft docs
        private ImageCodecInfo GetEncoder(ImageFormat format)  
        {  
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();  
            foreach (ImageCodecInfo codec in codecs)  
            {  
                if (codec.FormatID == format.Guid)  
                {  
                    return codec;  
                }  
            }  
            return null;  
        }
    }
}