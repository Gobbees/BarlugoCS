namespace BarlugoFX.Utils
{
    public class Scene
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Scene(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}