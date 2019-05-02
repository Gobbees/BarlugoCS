using System;
using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools.Common;

namespace BarlugoFX.Model.Tools
{
    public class BrightNess : AbstractImageTool
    {
        int value;
        const int MAX_VALUE = 255;
        const int DEFAULT_VALUE = 0;
        public BrightNess()
        {
        }

        public override Tool ToolType => Tool.Brightness;

        public override IImage ApplyTool(IImage target)
        {
            var savedValue = GetValueFromParameter(ParameterName.Brightness, -MAX_VALUE, MAX_VALUE, DEFAULT_VALUE);
            if(savedValue - Math.Floor(savedValue)>0){
                throw new Exception("The number must not have any decimal value");
            }
            value = (int)savedValue;

            int[,] pixels = target.ImageRGB;
            int[,] newPixels = new int[target.Height, target.Width];

            for (int i = 0; i < target.Height; i++)
            {
                for (int j = 0; j < target.Width; j++)
                {
                    newPixels[i,j] = pixels[i,j];
                    newPixels[i,j] = ColorUtils.UpdateBlue(newPixels[i,j], value);
                    newPixels[i,j] = ColorUtils.UpdateGreen(newPixels[i,j], value);
                    newPixels[i,j] = ColorUtils.UpdateRed(newPixels[i,j], value);
                }
            }
            return new Image(newPixels);
        }

        protected override bool IsAccepted(ParameterName name)
        {
            return name == ParameterName.Brightness;
        }
    }
}
