using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using BarlugoFX.Utils;

namespace BarlugoFX.View
{
    public interface IViewController
    {
        Stage Stage { get; }
    }
}