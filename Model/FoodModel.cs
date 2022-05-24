using System.Drawing;

namespace Aquarium.Model
{
    public class FoodModel
    {
        public Point Location { get; set; }

        public Size Size { get; set; }

        public Rectangle Bounds
        {
            get => new Rectangle(Location, Size);
        }
    }
}
