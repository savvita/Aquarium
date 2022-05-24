using Aquarium.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Aquarium.Controller
{
    public class AquariumController
    {
        private FishController fishController;
        private FoodController foodController;

        public Rectangle FishBounds
        {
            get => fishController.FishRectangle;
        }

        public GraphicsPath FoodBounds
        {
            get => foodController.FoodBounds;
        }

        public Rectangle FishImageBounds { get; private set; }

        public Rectangle FoodImageBounds
        {
            get => new Rectangle(foodController.ImageBounds.StartX, foodController.ImageBounds.StartY, 
                foodController.ImageBounds.Width, foodController.ImageBounds.Height);
        }

        public FishModel Fish
        {
            get => fishController.Fish;
        }

        public List<FoodModel> Food
        {
            get => foodController.Food;
        }

        public string FishImage
        {
            get => fishController.ImageFile;
        }

        public string FoodImage
        {
            get => foodController.ImageFile;
        }

        public int Count { get; private set; }

        public AquariumController(FishController fishController, FoodController foodController)
        {
            this.fishController = fishController;
            this.foodController = foodController;

            Count = 0;
        }

        public void MoveAll(int minWidth, int maxWidth, int minHeight, int maxHeight, int speed)
        {
            if (foodController.Food.Count > 0)
            {
                FishImageBounds = fishController.MoveTo(speed * 3, foodController.Food[0]);
            }
            else
            {
                FishImageBounds = fishController.Walk(minWidth, maxWidth, speed);
            }

            foodController.Fall(minHeight, maxHeight, speed);

            for (int i = 0; i < foodController.Food.Count; i++)
            {
                if (fishController.IsEat(this.foodController.Food[i]))
                {
                    foodController.Food.Remove(this.foodController.Food[i]);
                    Count++;
                }
            }
        }

        public FoodModel CreateFood(Point location)
        {
            return foodController.CreateFood(location);
        }
    }
}
