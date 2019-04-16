using System;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

namespace BarlugoFX.View
{
    public class View <T> where T: IViewController
    {
        private readonly FrameDimension _dimension;
        private T _controller;

        ///<summary>
        ///Returns the view controller.
        ///</summary>
        ///<returns>the view controller</returns>
        ///<exception cref="NullReferenceException">if the controller is null</exception>
        protected T Controller {
            get
            {
                if (_controller == null) {
                    throw new NullReferenceException("The controller is null");
                }
                return (T) _controller;
            }
        }
        
        public View()
        {
        }
        
        ///<summary>
        ///Displays an error alert.
        ///</summary>
        ///<<param name="message">the alert message</param>
        public static void ShowErrorAlert(string message) {
            //Here the program should open an error alert
        }
    }
}