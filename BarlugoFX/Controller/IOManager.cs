using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            Console.WriteLine(file.ToString());
            inputFileName = file.ToString();
            inputFileName = inputFileName.Substring(0, inputFileName.IndexOf('.'));
            return new Model.ImageTools.Image(new Bitmap(file.ToString()));
        }
        
        public void ExportImage(IImage image, Uri file, ImageFormat format) 
        {
            var bmp = new Bitmap(ImageUtils.ConvertImageToBitmap(image));
            var gBmp = Graphics.FromImage(bmp);
            gBmp.Dispose();
            bmp.Save(file.ToString(), format);
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