using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using BarlugoFX.Model.ImageTools;

namespace BarlugoFX
{   
    public class BrightNessTest
    {
        private readonly IImage target;
        private readonly IImage expectedOuput;

        public BrightNessTest()
        {
            target = new Model.ImageTools.Image(new Bitmap(Directory.GetCurrentDirectory() + "/JPEGS/begin.JPG"));
            expectedOuput = new Model.ImageTools.Image(new Bitmap(Directory.GetCurrentDirectory() + "/JPEGS/expected.JPG"));
        }

        [Test]
        public void TestInvalidParameter(){
            Model.Tools.Common.IImageTool bright = new Model.Tools.BrightNess();
            try{
                bright.AddParameter(Model.Tools.Common.ParameterName.NotValid, new Model.Tools.Common.Parameter<Double>(150));
                Assert.Fail("The Parameter Name is not valide");
            }catch(Exception){
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void TestRepeatedParameter()
        {
            Model.Tools.Common.IImageTool bright = new Model.Tools.BrightNess();
            bright.AddParameter(Model.Tools.Common.ParameterName.Brightness, new Model.Tools.Common.Parameter<Double>(150));
            try
            {
                bright.AddParameter(Model.Tools.Common.ParameterName.Brightness, new Model.Tools.Common.Parameter<Double>(150));
                Assert.Fail("You need to remove the parameter first");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void TestDoubleParameter()
        {
            Model.Tools.Common.IImageTool bright = new Model.Tools.BrightNess();
            bright.AddParameter(Model.Tools.Common.ParameterName.Brightness, new Model.Tools.Common.Parameter<Double>(150.5));
            try
            {
                bright.ApplyTool(target);
                Assert.Fail("Should be an int");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void TestOutOfRangeParameter()
        {
            Model.Tools.Common.IImageTool bright = new Model.Tools.BrightNess();
            bright.AddParameter(Model.Tools.Common.ParameterName.Brightness, new Model.Tools.Common.Parameter<Double>(256));
            try
            {
                bright.ApplyTool(target);
                Assert.Fail("Should be between -255 and 255");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void ResultAsExpected()
        {
            Model.Tools.Common.IImageTool bright = new Model.Tools.BrightNess();
            bright.AddParameter(Model.Tools.Common.ParameterName.Brightness, new Model.Tools.Common.Parameter<Double>(150));

            IImage result = bright.ApplyTool(target);
            for (int i = 0; i < target.Height; i++)
            {
                for (int j = 0; j < target.Width; j++)
                {
                    if (result.ImageRGB[i, j] != expectedOuput.ImageRGB[i, j])
                    {
                        Assert.Fail("Result shall be the same as expectedOutput");    
                    }
                }    
            }
        }
    }
}
