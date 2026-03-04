using Microsoft.Maui.Controls;

namespace MainPageUITraining
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GameBackground.IsAnimationPlaying = false;
            AviatorPlane.IsAnimationPlaying = false;
            
        }

        private void OnPlayClicked(object sender, EventArgs e)
        {

            GameBackground.IsAnimationPlaying = true;
            AviatorPlane.IsAnimationPlaying = true;
            // 🔧 TEST multiplier
            double testMultiplier = 6.0;

            // Normalize multiplier
            var progress = Math.Min(testMultiplier / 10.0, 1.0);

            // 🔑 USE FRAME SIZE (not Page size)
            double frameWidth = GameFrame.Width;
            double frameHeight = GameFrame.Height;

            // 🔑 Plane size
            double planeWidth = AviatorPlane.Width;
            double planeHeight = AviatorPlane.Height;

            // 🔑 Max safe positions (keep plane INSIDE frame)
            double maxX = frameWidth - planeWidth - 25;
            double maxY = frameHeight - planeHeight - 25;

            // 🔑 Target diagonal position
            double targetX = maxX * progress;
            double targetY = -maxY * progress;

            // Stop previous animation
            AviatorPlane.AbortAnimation("PlaneDiagonalAnimation");

            // Animate diagonally
            var animation = new Animation();

            animation.Add(0, 1, new Animation(
                d => AviatorPlane.TranslationX = d,
                0,
                targetX,
                Easing.Linear));

            animation.Add(0, 1, new Animation(
                d => AviatorPlane.TranslationY = d,
                0,
                targetY,
                Easing.Linear));

            animation.Commit(
                owner: AviatorPlane,
                name: "PlaneDiagonalAnimation",
                length: 900,
                easing: Easing.Linear,
                finished: (v, c) =>
                {
                    // 🔑 RESET after reaching edge
                    AviatorPlane.TranslationX = 0;
                    AviatorPlane.TranslationY = 0;
                    AviatorPlane.Rotation = 0;
                    GameBackground.IsAnimationPlaying = false;
                    AviatorPlane.IsAnimationPlaying = false;

                });
        

            

        }
        private void UpdatePlaneAnimation(double currentMultiplier)
        {
            
        }

    }
}