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
            AppManager manager = new AppManager(new Uri(Directory.GetCurrentDirectory() + "/Output/input.jpeg"));
            Console.WriteLine("Applying exposure....");
            manager.Exposure = 0.2;
            manager.ExportImage(new Uri(Directory.GetCurrentDirectory() + "/Output/exposure.jpeg"), ImageFormat.Jpeg );
            Console.WriteLine("Saved");
            
            manager = new AppManager(new Uri(Directory.GetCurrentDirectory() + "/Output/input.jpeg"));
            Console.WriteLine("Applying contrast....");
            manager.Contrast = 100;
            manager.ExportImage(new Uri(Directory.GetCurrentDirectory() + "/Output/contrast.jpeg"), ImageFormat.Jpeg );
            Console.WriteLine("Saved");
            
            manager = new AppManager(new Uri(Directory.GetCurrentDirectory() + "/Output/input.jpeg"));
            Console.WriteLine("Applying brightness....");
            manager.Brightness = 50;
            manager.ExportImage(new Uri(Directory.GetCurrentDirectory() + "/Output/brightness.jpeg"), ImageFormat.Jpeg );
            Console.WriteLine("Saved");
            
            manager = new AppManager(new Uri(Directory.GetCurrentDirectory() + "/Output/input.jpeg"));
            Console.WriteLine("Applying crop....");
            manager.Cropper = new int[] {200,200,1200,1200};
            manager.ExportImage(new Uri(Directory.GetCurrentDirectory() + "/Output/crop.jpeg"), ImageFormat.Jpeg );
            Console.WriteLine("Saved");
            
            manager = new AppManager(new Uri(Directory.GetCurrentDirectory() + "/Output/input.jpeg"));
            Console.WriteLine("Applying Saturation....");
            manager.Saturation = 0.25;
            manager.ExportImage(new Uri(Directory.GetCurrentDirectory() + "/Output/Saturation.jpeg"), ImageFormat.Jpeg );
            Console.WriteLine("Saved");
        }
    }
}
