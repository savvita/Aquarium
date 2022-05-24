using Aquarium.Model;
using System;
using System.Drawing;

namespace Aquarium.Controller
{
    public class FishController
    {
        private int imageNumber = 0;
        private Func<int, Rectangle> currentMove;

        public FishModel Fish { get; }

        /// <summary>
        /// Path to the file with images sprite
        /// </summary>
        public string ImageFile { get; set; }

        /// <summary>
        /// Number of images in the row for the one direction of moving
        /// </summary>
        public int NumberOfImages { get; }

        /// <summary>
        /// Bounds for the image in the sprite for moving left
        /// </summary>
        public ImageBounds MovingLeftImageBounds { get; set; }

        /// <summary>
        /// Bounds for the image in the sprite for moving right
        /// </summary>
        public ImageBounds MovingRightImageBounds { get; set; }

        /// <summary>
        /// Bounds for the image in the sprite for moving up
        /// </summary>
        public ImageBounds MovingUpImageBounds { get; set; }

        /// <summary>
        /// Bounds for the image in the sprite for moving down
        /// </summary>
        public ImageBounds MovingDownImageBounds { get; set; }

        /// <summary>
        /// Bounds of the fish
        /// </summary>
        public Rectangle FishRectangle
        {
            get => new Rectangle(Fish.Location, Fish.Size);
        }

        public FishController(string imageFile, int numberOfImages, ImageBounds movingLeftImageBounds, ImageBounds movingRightImageBounds,
            ImageBounds movingUpImageBounds, ImageBounds movingDownImageBounds)
        {
            ImageFile = imageFile;
            NumberOfImages = numberOfImages;
            MovingLeftImageBounds = movingLeftImageBounds;
            MovingRightImageBounds = movingRightImageBounds;
            MovingUpImageBounds = movingUpImageBounds;
            MovingDownImageBounds = movingDownImageBounds;

            Fish = new FishModel();
            Fish.Size = new Size(MovingRightImageBounds.Width, MovingRightImageBounds.Height);

            currentMove = MoveRight;
        }

        /// <summary>
        /// Move the fish from left to right and from right to left
        /// </summary>
        /// <param name="min">Minimum value of loaction X</param>
        /// <param name="max">Maximum value of loaction X</param>
        /// <param name="speed">Shift on which the fish is moving</param>
        /// <returns>Bounds of the image in the sprite for current moving</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Rectangle Walk(int min, int max, int speed)
        {
            if (Fish == null)
                throw new ArgumentNullException(nameof(Fish));

            if (min >= max)
                throw new ArgumentException("Max cannot be less or equal min");

            if(currentMove == MoveRight && (Fish.Location.X + speed >= max - Fish.Size.Width))
            {
                currentMove = MoveLeft;
            }

            else if (currentMove == MoveLeft && (Fish.Location.X + speed <= min))
            {
                currentMove = MoveRight;
            }

            return currentMove(speed);
        }

        /// <summary>
        /// Move towards the food
        /// </summary>
        /// <param name="speed">Shift on which the fish is moving</param>
        /// <param name="food">Food to eat</param>
        /// <returns>Bounds of the image in the sprite for moving</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Rectangle MoveTo(int speed, FoodModel food)
        {
            if (Fish == null)
                throw new ArgumentNullException(nameof(Fish));

            if (Fish.Location.X + Fish.Size.Width / 2 < food.Location.X)
                return MoveRight(speed);

            if (Fish.Location.X > food.Location.X)
                return MoveLeft(speed);

            if (Fish.Location.Y + Fish.Size.Height / 2 < food.Location.Y)
                return MoveDown(speed);

            if (Fish.Location.Y > food.Location.Y + food.Size.Height / 2)
                return MoveUp(speed);

            return new Rectangle(0, 0, 0, 0);
        }


        #region Moves

        /// <summary>
        /// Move location of the fish to the right
        /// </summary>
        /// <param name="speed">Shift on which the fish is moving</param>
        /// <returns>Bounds for the image in the sprite for current moving</returns>
        private Rectangle MoveRight(int speed)
        {
            Fish.Location = new Point(Fish.Location.X + speed, Fish.Location.Y);
            Fish.Size = new Size(MovingRightImageBounds.Width, MovingRightImageBounds.Height);

            Rectangle rect = new Rectangle(MovingRightImageBounds.StartX + imageNumber * MovingRightImageBounds.Width,
                MovingRightImageBounds.StartY, MovingRightImageBounds.Width, MovingRightImageBounds.Height);

            imageNumber++;
            imageNumber %= NumberOfImages;

            return rect;
        }

        /// <summary>
        /// Move location of the fish to the left
        /// </summary>
        /// <param name="speed">Shift on which the fish is moving</param>
        /// <returns>Bounds for the image in the sprite for current moving</returns>
        private Rectangle MoveLeft(int speed)
        {
            Fish.Location = new Point(Fish.Location.X - speed, Fish.Location.Y);
            Fish.Size = new Size(MovingLeftImageBounds.Width, MovingLeftImageBounds.Height);

            Rectangle rect = new Rectangle(MovingLeftImageBounds.StartX + imageNumber * MovingLeftImageBounds.Width,
                MovingLeftImageBounds.StartY, MovingLeftImageBounds.Width, MovingLeftImageBounds.Height);

            imageNumber++;
            imageNumber %= NumberOfImages;

            return rect;
        }

        /// <summary>
        /// Move location of the fish up
        /// </summary>
        /// <param name="speed">Shift on which the fish is moving</param>
        /// <returns>Bounds for the image in the sprite for current moving</returns>
        private Rectangle MoveUp(int speed)
        {
            Fish.Location = new Point(Fish.Location.X, Fish.Location.Y - speed);
            Fish.Size = new Size(MovingUpImageBounds.Width, MovingUpImageBounds.Height);

            Rectangle rect = new Rectangle(MovingUpImageBounds.StartX + imageNumber * MovingUpImageBounds.Width,
                MovingUpImageBounds.StartY, MovingUpImageBounds.Width, MovingUpImageBounds.Height);

            imageNumber++;
            imageNumber %= NumberOfImages;

            return rect;
        }

        /// <summary>
        /// Move location of the fish down
        /// </summary>
        /// <param name="speed">Shift on which the fish is moving</param>
        /// <returns>Bounds for the image in the sprite for current moving</returns>
        private Rectangle MoveDown(int speed)
        {
            Fish.Location = new Point(Fish.Location.X, Fish.Location.Y + speed);
            Fish.Size = new Size(MovingUpImageBounds.Width, MovingUpImageBounds.Height);

            Rectangle rect = new Rectangle(MovingDownImageBounds.StartX + imageNumber * MovingDownImageBounds.Width,
                MovingDownImageBounds.StartY, MovingDownImageBounds.Width, MovingDownImageBounds.Height);

            imageNumber++;
            imageNumber %= NumberOfImages;

            return rect;
        } 
        #endregion

        /// <summary>
        /// Eat the food
        /// </summary>
        /// <param name="food">Food to eat</param>
        /// <returns>True if food is eaten false otherwise</returns>
        public bool IsEat(FoodModel food)
        {
            if(this.FishRectangle.IntersectsWith(food.Bounds))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
