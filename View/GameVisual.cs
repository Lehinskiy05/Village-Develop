
using Village_Develop.Model;

namespace Village_Develop
{
    public partial class GameVisual : Form
    {
        private Graphics graphics;
        private Model.Model model;

        public GameVisual()
        {
            InitializeComponent();
            graphics = CreateGraphics();
            model = new Model.Model();
        }

        // Invalidate() запускает OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawBackground(model.Map.Size);
            DrawEstate(model.Map.Estates);
            DrawPlayer(model.Player);
        }


        private void DrawBackground(Size size)
        {
            BackColor = Color.Green;
        }

        private void DrawEstate(List<Estate> estates)
        {
            foreach (Estate estate in estates)
            {
                graphics.FillRectangle(Brushes.SaddleBrown, estate.Bounds);
            }
        }

        private void DrawPlayer(Player player)
        {
            graphics.FillEllipse(Brushes.Snow, player.Bounds);
        }
    }
}
