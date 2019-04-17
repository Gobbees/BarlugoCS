using System;
using System.Drawing.Imaging;
using System.IO;
using BarlugoFX.Controller;
using BarlugoFX.Model.ImageTools;
using NUnit.Framework;

namespace BarlugoFX
{
    public class IOTest
    {
        private AppManager _manager;
        private readonly Uri _inputPath = new Uri(Directory.GetCurrentDirectory() + "/JPEGS/begin.jpg");
        private readonly Uri _outputPath = new Uri(Directory.GetCurrentDirectory() + "/JPEGS/output.png");

        public IOTest()
        {
            _manager = new AppManager(null);
        }
        
        
        [Test]
        public void LoadingTestFromLoadNewImage()
        {
            _manager.LoadNewImage(_inputPath);
            if (_manager.Image == null)
            {
                Assert.Fail("The image has not been loaded.");
            }
        }
        
        [Test]
        public void LoadingTestFromConstructor()
        {
            _manager = new AppManager(_inputPath);
            if (_manager.Image == null)
            {
                Assert.Fail("The image has not been loaded.");
            }
        }

        [Test]
        public void ExportTest()
        {
            _manager = new AppManager(_inputPath);
            if (_outputPath.IsFile)
            {
                File.Delete(_outputPath.AbsolutePath);
            }
            _manager.ExportImage(_outputPath, ImageFormat.Png);
            if (!_outputPath.IsFile)
            {
                Assert.Fail("The image has not been exported");
            }
        }
    }
}