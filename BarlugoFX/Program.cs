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
            AppManager manager = new AppManager(new Uri(Directory.GetCurrentDirectory() + "/bin/Debug/netcoreapp2.0/JPEGS/begin.jpg"));
            manager.Contrast = 10;
            manager.ExportImage(new Uri(Directory.GetCurrentDirectory() + "/bin/Debug/netcoreapp2.0/JPEGS/outputContrast.jpeg"), ImageFormat.Jpeg );
            Console.WriteLine("Saved");
        }
    }
}
