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
    /// Tests the basic operations on the Cropper tool
    /// </summary>
    public class CropperTest
    {
        private readonly IImage _input;
       
        public CropperTest()
        {
            _input = new Model.ImageTools.Image(new Bitmap(Directory.GetCurrentDirectory() + "/JPEGS/input.jpeg"));
        }

        /// <summary>
        /// Tests an invalid parameter
        /// </summary>
        [Test]
        public void TestInvalidParameter()
        {
            var cropper = Cropper.CreateCropper();
            try
            {
                cropper.AddParameter(Model.Tools.Common.ParameterName.Brightness, new Parameter<Double>(150));
                Assert.Fail("The Parameter Name is not valid");
            }
            catch (Exception e)
            {
                Assert.True(true);
            }
        }

        /// <summary>
        /// Tests a repeated parameter
        /// </summary>
        [Test]
        public void TestRepeatedParameter()
        {
            var cropper = Cropper.CreateCropper();
            cropper.AddParameter(Model.Tools.Common.ParameterName.X1, new Parameter<Double>(30));
            try
            {
                cropper.AddParameter(Model.Tools.Common.ParameterName.X1, new Parameter<Double>(100));
                Assert.Fail("You need to remove the parameter first");
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
            var cropper = Cropper.CreateCropper();
            cropper.AddParameter(Model.Tools.Common.ParameterName.X1, new Parameter<Double>(1500000));
            try
            {
                cropper.ApplyTool(_input);
                Assert.Fail("Should be between -255 and 255");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}