using System;
using System.Drawing;
using System.IO;
using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools;
using NUnit.Framework;

namespace BarlugoFX
{
    /// <summary>
    /// Tests the basic operations on the Saturation tool
    /// </summary>
    public class SaturationTest
    {
        private readonly IImage _target;

        public SaturationTest()
        {
            _target = new Model.ImageTools.Image(new Bitmap(Directory.GetCurrentDirectory() + "/JPEGS/input.jpeg"));
        }
    
        /// <summary>
        /// Tests an invalid parameter
        /// </summary>
        [Test]
        public void TestInvalidParameter() {
            var saturation = Saturation.CreateSaturation();
            try 
            {
                saturation.AddParameter(Model.Tools.Common.ParameterName.Hue, new Model.Tools.Common.Parameter<Double>(150));
                Assert.Fail("The Parameter Name is not valid");
            } 
            catch(Exception) 
            {
                Assert.IsTrue(true);
            }
        }
    
        /// <summary>
        /// Tests a repeated parameter
        /// </summary>
        [Test]
        public void TestRepeatedParameter()
        {
            var saturation = Saturation.CreateSaturation();
            saturation.AddParameter(Model.Tools.Common.ParameterName.Saturation, new Model.Tools.Common.Parameter<Double>(1));
            try
            {
                saturation.AddParameter(Model.Tools.Common.ParameterName.Saturation, new Model.Tools.Common.Parameter<Double>(150));
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
            var saturation = Saturation.CreateSaturation();
            saturation.AddParameter(Model.Tools.Common.ParameterName.Saturation, new Model.Tools.Common.Parameter<double>(2));
            try
            {
                saturation.ApplyTool(_target);
                Assert.Fail("Should be between -1 and 1");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}