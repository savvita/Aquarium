using Aquarium.Controller;
using Aquarium.Model;
using Aquarium.View;
using System;
using System.Windows.Forms;

namespace Aquarium
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FishController fishController = new FishController
                ("something.png", 4,
                new ImageBounds(0, 90, 90, 60),
                new ImageBounds(0, 164, 90, 60),
                new ImageBounds(0, 230, 90, 90),
                new ImageBounds(0, 0, 90, 80)
                );

            FoodController foodController = new FoodController ("apple.png", new ImageBounds(370, 200, 200, 200));

            AquariumController controller = new AquariumController(fishController, foodController);

            Application.Run(new AquariumForm(controller));
        }
    }
}
