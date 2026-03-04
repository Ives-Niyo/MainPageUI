using Microsoft.Maui.Controls;

namespace MainPageUITraining
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GameBackground.IsAnimationPlaying = false;
            AirCraft.IsAnimationPlaying = false;
            
        }

        private void OnPlayClicked(object sender, EventArgs e)
        {

            GameBackground.IsAnimationPlaying = true;
            AirCraft.IsAnimationPlaying = true;
            // 🔧 TEST multiplier
            double testMultiplier = 6.0;

            // Normalize multiplier
            var progress = Math.Min(testMultiplier / 10.0, 1.0);

            // 🔑 USE FRAME SIZE (not Page size)
            double frameWidth = GameFrame.Width;
            double frameHeight = GameFrame.Height;

            // 🔑 Plane size
            double CraftWidth = AirCraft.Width;
            double CraftHeight = AirCraft.Height;

            // 🔑 Max safe positions (keep plane INSIDE frame)
            double maxX = frameWidth - CraftWidth - 25;
            double maxY = frameHeight - CraftHeight - 25;

            // 🔑 Target diagonal position
            double targetX = maxX * progress;
            double targetY = -maxY * progress;

            // Stop previous animation
            AirCraft.AbortAnimation("CraftDiagonalAnimation");

            // Animate diagonally
            var animation = new Animation();

            animation.Add(0, 1, new Animation(
                d => AirCraft.TranslationX = d,
                0,
                targetX,
                Easing.Linear));

            animation.Add(0, 1, new Animation(
                d => AirCraft.TranslationY = d,
                0,
                targetY,
                Easing.Linear));

            animation.Commit(
                owner: AviatorPlane,
                name: "CraftDiagonalAnimation",
                length: 900,
                easing: Easing.Linear,
                finished: (v, c) =>
                {
                    // 🔑 RESET after reaching edge
                    AirCraft.TranslationX = 0;
                    AirCraft.TranslationY = 0;
                    AirCraft.Rotation = 0;
                    GameBackground.IsAnimationPlaying = false;
                    AirCraft.IsAnimationPlaying = false;

                });
        

            

        }
        private void UpdateCraftAnimation(double currentMultiplier)
        {
            
        }

    }
}
