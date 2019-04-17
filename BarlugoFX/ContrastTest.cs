using System;
using System.Drawing;
using System.IO;
using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools;
using BarlugoFX.Model.Tools.Common;
using NUnit.Framework;

namespace BarlugoFX
{
    /// <summary>
    /// Tests the basic operations on the Contrast tool
    /// </summary>
    public class ContrastTest
    {
        private readonly IImage _input;
        private readonly IImage _output;

        public ContrastTest()
        {
            _input = new Model.ImageTools.Image(new Bitmap(Directory.GetCurrentDirectory() + "/JPEGS/begin.JPG"));
            _output = new Model.ImageTools.Image(new Bitmap(Directory.GetCurrentDirectory() + "/JPEGS/outputContrast.jpeg"));
        }

        [Test]
        public void TestInvalidParameter()
        {
            var contrast = Contrast.CreateContrast();
            try
            {
                contrast.AddParameter(Model.Tools.Common.ParameterName.Brightness, new Parameter<Double>(150));
                Assert.Fail("The Parameter Name is not valid");
            }
            catch (Exception e)
            {
                Assert.True(true);
            }
        }

        /// <summary>
        /// Tests an invalid parameter
        /// </summary>
        [Test]
        public void TestRepeatedParameter()
        {
            var contrast = Contrast.CreateContrast();
            contrast.AddParameter(Model.Tools.Common.ParameterName.Contrast, new Parameter<Double>(30));
            try
            {
                contrast.AddParameter(Model.Tools.Common.ParameterName.Contrast, new Parameter<Double>(100));
                Assert.Fail("You need to remove the parameter first");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Tests a repeated parameter
        /// </summary>
        [Test]
        public void TestDoubleParameter()
        {
            var contrast = Contrast.CreateContrast();
            contrast.AddParameter(Model.Tools.Common.ParameterName.Contrast, new Parameter<Double>(150.5));
            try
            {
                contrast.ApplyTool(_input);
                Assert.Fail("Should be an int");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Test an out of range parameter
        /// </summary>
        [Test]
        public void TestOutOfRangeParameter()
        {
            var contrast = Contrast.CreateContrast();
            contrast.AddParameter(Model.Tools.Common.ParameterName.Contrast, new Parameter<Double>(256));
            try
            {
                contrast.ApplyTool(_input);
                Assert.Fail("Should be between -255 and 255");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}