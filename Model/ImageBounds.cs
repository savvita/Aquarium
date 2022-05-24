using System.Drawing;

namespace Aquarium.Model
{
    public class ImageBounds
    {
        public int StartX { get; }
        public int StartY { get; }

        public int Width { get; }
        public int Height { get; }

        public ImageBounds(int startX, int startY, int width, int height)
        {
            StartX = startX;
            StartY = startY;
            Width = width;
            Height = height;
        }

        public Rectangle Rectangle
        {
            get => new Rectangle(StartY, StartY, Width, Height);
        }
    }
}
