using Aquarium.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Aquarium.Controller
{
    public class FoodController
    {
        public List<FoodModel> Food { get; }

        /// <summary>
        /// Path to the file with images sprite
        /// </summary>
        public string ImageFile { get; set; }

        /// <summary>
        /// Bounds for the image in the sprite
        /// </summary>
        public ImageBounds ImageBounds { get; set; }

        /// <summary>
        /// Bounds of the food
        /// </summary>
        public GraphicsPath FoodBounds
        {
            get
            {
                GraphicsPath path = new GraphicsPath();

                foreach(FoodModel food in Food)
                {
                    path.AddRectangle(food.Bounds);
                }

                return path;
            }
        }

        public FoodController(string imageFile, ImageBounds imageBounds)
        {
            ImageFile = imageFile;
            ImageBounds = imageBounds;

            Food = new List<FoodModel>();
        }

        public FoodModel CreateFood(Point location)
        {
            FoodModel food = new FoodModel { Location = location, Size = new Size(20, 20) };

            this.Food.Add(food);

            return food;
        }

        /// <summary>
        /// Move the food down
        /// </summary>
        /// <param name="min">Minimum value of loaction Y</param>
        /// <param name="max">Maximum value of loaction Y</param>
        /// <param name="speed">Shift on which the food is moving</param>
        /// <returns>Bounds of the image in the sprite for current food</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Rectangle Fall(int min, int max, int speed)
        {
            if (Food == null)
                throw new ArgumentNullException(nameof(Food));

            if (min >= max)
                throw new ArgumentException("Max cannot be less or equal min");

            for (int i = 0; i < Food.Count; i++)
            {
                Food[i].Location = new Point(Food[i].Location.X, Food[i].Location.Y + speed);

                if (Food[i].Location.Y >= max)
                {
                    Food.Remove(Food[i]);
                }
            }

            Rectangle rect = new Rectangle(ImageBounds.StartX, ImageBounds.StartY, ImageBounds.Width, ImageBounds.Height);

            return rect;
        }

    }
}
