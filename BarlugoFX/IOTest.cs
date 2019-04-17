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
        private readonly IImage _image;
        private AppManager _manager;
        private readonly Uri inputPath = new Uri(Directory.GetCurrentDirectory() + "/JPEGS/image.jpg");
        private readonly Uri outputPath = new Uri(Directory.GetCurrentDirectory() + "/JPEGS/output.png");

        public IOTest()
        {
            _manager = new AppManager(null);
        }
        
        
        [Test]
        public void LoadingTestFromLoadNewImage()
        {
            _manager.LoadNewImage(inputPath);
            if (_manager.Image == null)
            {
                Assert.Fail("The image has not been loaded.");
            }
        }
        
        [Test]
        public void LoadingTestFromConstructor()
        {
            _manager = new AppManager(inputPath);
            if (_manager.Image == null)
            {
                Assert.Fail("The image has not been loaded.");
            }
        }

        [Test]
        public void ExportTest()
        {
            _manager = new AppManager(inputPath);
            if (outputPath.IsFile)
            {
                File.Delete(outputPath.AbsolutePath);
            }
            _manager.ExportImage(outputPath, ImageFormat.Png);
            if (!outputPath.IsFile)
            {
                Assert.Fail("The image has not been exported");
            }
        }
    }
}