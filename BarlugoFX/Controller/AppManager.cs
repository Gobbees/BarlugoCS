using System;
using System.Drawing.Imaging;
using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools;
using BarlugoFX.Model.Tools.Common;

namespace BarlugoFX.Controller
{
    ///<summary>
    ///The main controller class.
    ///Now the only usable tools are Exposure, Contrast, Brightness. All the other ones are not working.
    ///</summary>
    public class AppManager : IAppManager
    {
        //multipliers used to adapt the input from the view to the model
        private const float HsbMultiplier = 0.01f;
        private const float WbMultiplier = 0.015f;
        private const float VibranceMultiplier = 0.01f;
        private const float BwMultiplier = 0.004f;
        private const float BwShifter = 0.8f;
        private IImage _image;
        private readonly IImageTool _exposure;
        private readonly IImageTool _contrast;
        private readonly IImageTool _brightness;
        private readonly IImageTool _wb;
        private readonly IImageTool _saturation;
        private readonly IImageTool _hue;
        private readonly IImageTool _srgb;
        private readonly IImageTool _bw;
        private readonly IImageTool _vibrance;
        private readonly IOManager _fileManager;
        
        public IImage Image { get; set; }

        public double Exposure 
        {
            set
            {
                _exposure.AddParameter(ParameterName.Exposure, new Parameter<double>(value));
                _image = _exposure.ApplyTool(_image);
                _exposure.RemoveParameter(ParameterName.Brightness);
            }
        }
        public double Contrast 
        {
            set
            {
                _contrast.AddParameter(ParameterName.Contrast, new Parameter<double>(value));
                _image = _contrast.ApplyTool(_image);
                _contrast.RemoveParameter(ParameterName.Brightness);
            }
        }
        public double Brightness 
        {
            set
            {
                _brightness.AddParameter(ParameterName.Brightness, new Parameter<double>(value));
                _image = _brightness.ApplyTool(_image);
                _brightness.RemoveParameter(ParameterName.Brightness);
            }
        }
        public double WhiteBalance 
        {
            set
            {
                _wb.AddParameter(/*ParametersName.WHITEBALANCE*/ParameterName.Brightness, new Parameter<double>(value));
                _image = _wb.ApplyTool(_image);
                _wb.RemoveParameter(ParameterName.Brightness);
            }
        }
        public double Saturation
        {
            set
            {
                _saturation.AddParameter( /*ParametersName.SATURATION*/ParameterName.Brightness, new Parameter<double>(value));
                _image = _saturation.ApplyTool(_image);
                _saturation.RemoveParameter(ParameterName.Brightness);
            }
        }
        public double Hue 
        {
            set
            {
                _hue.AddParameter(/*ParametersName.HUE*/ParameterName.Brightness, new Parameter<double>(value));
                _image = _hue.ApplyTool(_image);
                _hue.RemoveParameter(ParameterName.Brightness);
            }
        }
        public double SelectiveColor  //three parameters
        {
            set
            {
                _srgb.AddParameter(/*ParametersName.BLACKANDWHITE*/ParameterName.Brightness, new Parameter<double>(value));
                _image = _srgb.ApplyTool(_image);
                _srgb.RemoveParameter(ParameterName.Brightness);
            }
        }
        public double BlackAndWhite  //three parameters
        {
            set
            {
                _bw.AddParameter(/*ParametersName.BLACKANDWHITE*/ParameterName.Brightness, new Parameter<double>(value));
                _image = _bw.ApplyTool(_image);
                _bw.RemoveParameter(ParameterName.Brightness);
            }
        }
        public double Vibrance 
        {
            set
            {
                _vibrance.AddParameter(/*ParametersName.VIBRANCE*/ParameterName.Brightness, new Parameter<double>(value));
                _image = _vibrance.ApplyTool(_image);
                _vibrance.RemoveParameter(ParameterName.Brightness);
            }
        }
        /// <summary>
        /// The class constructor
        /// </summary>
        /// <param name="file">the file path</param>
        /// <exception cref="ArgumentNullException">if the param is null</exception>
        public AppManager(Uri file)
        {   
            //init. TEMP. only brightness, contrast and exposure are working.
            _exposure = Model.Tools.Exposure.CreateExposure();
            _contrast = Model.Tools.Contrast.CreateContrast();
            _brightness = new BrightNess();
            _wb = new BrightNess();
            _saturation = new BrightNess();
            _hue = new BrightNess();
            _srgb = new BrightNess();
            _bw = new BrightNess();
            _vibrance = new BrightNess();
            _fileManager = new IOManager();
            if (file == null) throw new ArgumentNullException(nameof(file));
            _fileManager.LoadImageFromFile(file);
        }

        public void LoadNewImage(Uri file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            _fileManager.LoadImageFromFile(file);
        }

        public void ExportImage(Uri path, ImageFormat format)
        {
            _fileManager.ExportImage(_image, path, format);
        }

        public void ExportImage(Uri path, int quality)
        {
            _fileManager.ExportJPEGWithQuality(_image, path, quality);
        }
    }
}