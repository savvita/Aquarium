using Aquarium.Controller;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Aquarium.View
{
    public partial class AquariumForm : Form
    {
        private Bitmap bufferedImage;
        private Bitmap fishImage;
        private Bitmap foodImage;
        private Graphics graphics;

        private AquariumController controller;

        public AquariumForm()
        {
            InitializeComponent();
        }

        public AquariumForm(AquariumController controller) : this()
        {
            this.DoubleBuffered = true;

            this.BackgroundImageLayout = ImageLayout.Center;

            this.controller = controller;
            this.controller.Fish.Location = new Point(0, this.Height / 2);


            this.bufferedImage = new Bitmap(this.Width, this.Height);
            this.graphics = Graphics.FromImage(bufferedImage);

            this.fishImage = new Bitmap(this.controller.FishImage);
            this.fishImage.MakeTransparent();

            this.foodImage = new Bitmap(this.controller.FoodImage);
            this.foodImage.MakeTransparent();
            this.foodImage.MakeTransparent(Color.White);

            this.timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.graphics.DrawImage(Properties.Resources.Water, this.ClientRectangle);

            this.controller.MoveAll(0, this.Width, 0, this.Height, 10);

            this.graphics.DrawImage(fishImage, this.controller.FishBounds, this.controller.FishImageBounds, GraphicsUnit.Pixel);

            this.graphics.DrawString($"Count = {this.controller.Count}", new Font("Times New Roman", 14, FontStyle.Bold), Brushes.AliceBlue, new Point(10, 10));

            for (int i = 0; i < this.controller.Food.Count; i++)
            {
                this.graphics.DrawImage(foodImage, this.controller.Food[i].Bounds, this.controller.FoodImageBounds, GraphicsUnit.Pixel);
            }


            this.BackgroundImage = bufferedImage;
            this.Invalidate();
        }


        private void AquariumForm_SizeChanged(object sender, EventArgs e)
        {
            this.bufferedImage = new Bitmap(this.Width, this.Height);
            this.graphics = Graphics.FromImage(bufferedImage);

            this.controller.Fish.Location = new Point(this.controller.Fish.Location.X, this.Height / 2);
        }

        private void AquariumForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.controller.CreateFood(new Point(e.X, e.Y));
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            this.startButton.Visible = false;
            this.timer.Start();
        }
    }
}
