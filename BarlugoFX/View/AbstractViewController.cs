using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using BarlugoFX.Utils;

namespace BarlugoFX.View
{
    public abstract class AbstractViewController : IViewController
    {
        public Stage Stage
        {
            get => Stage;
            set
            {
                Stage = value ?? throw new ArgumentNullException("The stage must not be null");
                Scene = Stage.Scene;
            }
        }
        public Scene Scene { get; private set; }
        
        protected void CheckStage() {
            if (Stage == null) {
                throw new InvalidDataException("The stage is null");
            }
        }
    }
}