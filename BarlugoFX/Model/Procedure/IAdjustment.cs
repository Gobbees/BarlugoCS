using System;
using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools.Common;

namespace BarlugoFX.Model.Procedure
{
    public interface IAdjustment
    {
        bool IsEnabled { get; set; }

        IImageTool ImageTool { get; }
        
        Tool ToolType { get; }
        
        IImage StartImage { get; set; }
        
        IImage EndImage { get; set; }
    }
}