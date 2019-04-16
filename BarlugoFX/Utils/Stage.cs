namespace BarlugoFX.Utils
{
    public class Stage
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Scene Scene { get; }

        public Stage(Scene scene)
        {
            Scene = scene;
            Width = scene.Width;
            Height = scene.Height;
        }
    }
}