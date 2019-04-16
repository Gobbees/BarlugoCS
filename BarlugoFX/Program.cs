using System;
using System.Drawing.Imaging;
using BarlugoFX.Controller;

namespace BarlugoFX
{
    class Program
    {
        static void Main(string[] args)
       { 
            AppManager manager = new AppManager(new Uri(@"/Users/gg_mbpro/Desktop/ViewDiagram.jpg"));
            manager.Brightness = 10;
//            manager.ExportImage(new Uri(@"../../JPEGS/res.png"), ImageFormat.Png );
//            Console.WriteLine("Saved");
        }
    }
}
