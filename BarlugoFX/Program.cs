using System;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using BarlugoFX.Controller;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;

namespace BarlugoFX
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            AppManager manager = new AppManager(new Uri(Directory.GetCurrentDirectory() + "/JPEGS/expected.jpg"));
            manager.Brightness = 10;
            manager.ExportImage(new Uri(@"/Users/gg_mbpro/Desktop/res.png"), ImageFormat.Png );
            Console.WriteLine("Saved");
        }
    }
}
