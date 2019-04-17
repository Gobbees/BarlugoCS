using System;
using System.Drawing;
using System.IO;
using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools;
using NUnit.Framework;

namespace BarlugoFX
{
    public class ExposureTest
    {
        private readonly IImage _target;
        private readonly IImage _expectedOuput;

        public ExposureTest()
        {
            _target = new Model.ImageTools.Image(new Bitmap(Directory.GetCurrentDirectory() + "/JPEGS/inputExposure.jpeg"));
            _expectedOuput = new Model.ImageTools.Image(new Bitmap(Directory.GetCurrentDirectory() + "/JPEGS/outputExposure.jpeg"));
        }

        [Test]
        public void TestInvalidParameter() {
            var exposure = Exposure.CreateExposure();
            try 
            {
                exposure.AddParameter(Model.Tools.Common.ParameterName.NotValid, new Model.Tools.Common.Parameter<Double>(150));
                Assert.Fail("The Parameter Name is not valid");
            } 
            catch(Exception) 
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void TestRepeatedParameter()
        {
            var exposure = Exposure.CreateExposure();
            exposure.AddParameter(Model.Tools.Common.ParameterName.Exposure, new Model.Tools.Common.Parameter<Double>(1));
            try
            {
                exposure.AddParameter(Model.Tools.Common.ParameterName.Exposure, new Model.Tools.Common.Parameter<Double>(150));
                Assert.Fail("You need to remove the parameter first");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void TestOutOfRangeParameter()
        {
            var exposure = Exposure.CreateExposure();
            exposure.AddParameter(Model.Tools.Common.ParameterName.Exposure, new Model.Tools.Common.Parameter<double>(2));
            try
            {
                exposure.ApplyTool(_target);
                Assert.Fail("Should be between -1 and 1");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}