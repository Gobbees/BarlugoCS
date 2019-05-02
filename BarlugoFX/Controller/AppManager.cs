using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
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
        private readonly IImageTool _cropper;
        private readonly IOManager _fileManager;
        
        public IImage Image
        {
            get => _image;
        }

        public double Exposure 
        {
            set
            {
                _exposure.AddParameter(ParameterName.Exposure, new Parameter<double>(value));
                _image = _exposure.ApplyTool(_image);
                _exposure.RemoveParameter(ParameterName.Exposure);
            }
        }
        public double Contrast 
        {
            set
            {
                _contrast.AddParameter(ParameterName.Contrast, new Parameter<double>(value));
                _image = _contrast.ApplyTool(_image);
                _contrast.RemoveParameter(ParameterName.Contrast);
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
        
        public double Saturation
        {
            set
            {
                _saturation.AddParameter(ParameterName.Saturation, new Parameter<double>(value));
                _image = _saturation.ApplyTool(_image);
                _saturation.RemoveParameter(ParameterName.Saturation);
            }
        }
        
        public double Vibrance
        {
            set
            {
                _vibrance.AddParameter(ParameterName.Vibrance, new Parameter<double>(value));
                _image = _vibrance.ApplyTool(_image);
                _vibrance.RemoveParameter(ParameterName.Vibrance);
            }
        }
        public double Hue
        {
            set
            {
                _hue.AddParameter(ParameterName.Hue, new Parameter<double>(value));
                _image = _hue.ApplyTool(_image);
                _hue.RemoveParameter(ParameterName.Hue);
            }
        }
        public int[] Cropper
        {
            set
            {
                if (value.Length != 4)
                {
                    throw new ArgumentException("The arrayList must contain exactly 4 parameters");
                }
                _cropper.AddParameter(ParameterName.X1, new Parameter<double>(value[0]));
                _cropper.AddParameter(ParameterName.Y1, new Parameter<double>(value[1]));
                _cropper.AddParameter(ParameterName.X2, new Parameter<double>(value[2]));
                _cropper.AddParameter(ParameterName.Y2, new Parameter<double>(value[3]));
                _image = _cropper.ApplyTool(_image);
                _cropper.RemoveParameter(ParameterName.X1);
                _cropper.RemoveParameter(ParameterName.Y1);
                _cropper.RemoveParameter(ParameterName.X2);
                _cropper.RemoveParameter(ParameterName.Y2);
            }
        }

        /// <summary>
        /// The class constructor
        /// </summary>
        /// <param name="file">the file path</param>
        public AppManager(Uri file)
        {   
            //init. TEMP. only brightness, contrast and exposure are working.
            _exposure = Model.Tools.Exposure.CreateExposure();
            _contrast = Model.Tools.Contrast.CreateContrast();
            _brightness = new BrightNess();
            _wb = new BrightNess();
            _saturation = new Saturation();
            _hue = new Hue();
            _srgb = new BrightNess();
            _bw = new BrightNess();
            _vibrance = new Vibrance();
            _cropper = Model.Tools.Cropper.CreateCropper();
            _fileManager = new IOManager();
            if (file != null)
            {
                _image = _fileManager.LoadImageFromFile(file);
            }
        }

        public void LoadNewImage(Uri file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            _image = _fileManager.LoadImageFromFile(file);
        }

        public void ExportImage(Uri path, ImageFormat format)
        {
            _fileManager.ExportImage(_image, path, format);
        }
    }
}