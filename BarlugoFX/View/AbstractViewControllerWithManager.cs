using System;
using System.IO;
using BarlugoFX.Controller;

namespace BarlugoFX.View
{
    public class AbstractViewControllerWithManager : AbstractViewController
    {
        private IAppManager _manager;

        protected IAppManager Manager
        {
            get => _manager;
            set => _manager = value ?? throw new ArgumentNullException($"The manager must not be null");
        }
        
        protected void checkManager() {
            if (_manager == null) {
                throw new InvalidDataException("The manager is null");
            }
        }
    }
}