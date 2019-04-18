using System;
using System.Drawing.Imaging;
using System.IO;
using BarlugoFX.Controller;
using BarlugoFX.Model.ImageTools;
using NUnit.Framework;

namespace BarlugoFX
{
    /// <summary>
    /// This class tests the manager's I/O operations
    /// </summary>
    public class IOTest
    {
        private AppManager _manager;
        private readonly Uri _inputPath = new Uri(Directory.GetCurrentDirectory() + "/JPEGS/input.jpeg");
        private readonly Uri _outputPath = new Uri(Directory.GetCurrentDirectory() + "/JPEGS/output.png");

        public IOTest()
        {
            _manager = new AppManager(null);
        }
        
        /// <summary>
        /// Tests the loading of the new image from the method LoadNewImage
        /// </summary>
        [Test]
        public void LoadingTestFromLoadNewImage()
        {
            _manager.LoadNewImage(_inputPath);
            if (_manager.Image == null)
            {
                Assert.Fail("The image has not been loaded.");
            }
        }
        
        /// <summary>
        /// Tests the loading of the new image from the class constructor
        /// </summary>
        [Test]
        public void LoadingTestFromConstructor()
        {
            _manager = new AppManager(_inputPath);
            if (_manager.Image == null)
            {
                Assert.Fail("The image has not been loaded.");
            }
        }

        /// <summary>
        /// Tests the export operation
        /// </summary>
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