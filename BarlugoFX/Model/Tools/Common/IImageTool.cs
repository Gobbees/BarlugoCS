using System;
using BarlugoFX.Model.ImageTools;

namespace BarlugoFX.Model.Tools.Common
{
    public interface IImageTool
    {
        IImage ApplyTool(IImage target);

        void AddParameter(ParameterName name, IParameter<Double> value);

        IParameter<Double> GetParameter(ParameterName name);

        void RemoveParameter(ParameterName name);

        Tool ToolType { get; }
    }
}
