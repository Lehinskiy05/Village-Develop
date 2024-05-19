using System.Drawing.Printing;
using Village_Develop.Model;
using Timer = System.Windows.Forms.Timer;

namespace Village_Develop
{
    public partial class GameForm : Form
    {
        private Graphics graphics;
        private GameModel gameModel;
        private int fps;
        private Timer fpsTimer;
        private float cameraZoom;

        public GameForm()
        {
            InitializeComponent();
            gameModel = new GameModel();
            graphics = CreateGraphics();
            cameraZoom = 1.0f;
        }

        // Invalidate() запускает OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            fps++;

            var graphicsState = graphics.Save();

            //var widthRatio = ClientSize.Width / (float)gameModel.Map.Size.Width * cameraZoom;
            //var heightRatio = ClientSize.Height / (float)gameModel.Map.Size.Height * cameraZoom;

            //graphics.ScaleTransform(widthRatio, heightRatio);
            //graphics.Transform(...);

            Draw();
            
            graphics.Restore(graphicsState);
        }

        private void Draw()
        {
            DrawBackground(gameModel.Map.Size);
            DrawEstates(gameModel.Map.Estates);
            DrawPlayer(gameModel.Player);
        }


        private void DrawBackground(Size size)
        {
            BackColor = Color.Green;
        }

        private void DrawEstates(List<Estate> estates)
        {
            foreach (Estate estate in estates)
            {
                graphics.DrawImage(estate.Image, estate.Bounds);
            }
        }

        private void DrawPlayer(Player player)
        {
            graphics.DrawImage(player.Image, player.Bounds);
        }

        private void StartFpsCounterTimer()
        {
            fpsTimer = new Timer();
            fpsTimer.Interval = 1000;
            fpsTimer.Tick += (sender, args) =>
            {
                Parent.Text = "Village Develop" + "   [FPS: " + fps + "]";
                fps = 0;
            };
            fpsTimer.Start();
        }
    }
}
