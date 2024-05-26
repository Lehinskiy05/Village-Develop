using System.Drawing.Printing;
using System.Text;
using Village_Develop.Model;
using static System.Windows.Forms.AxHost;
using Timer = System.Windows.Forms.Timer;

namespace Village_Develop
{
    public partial class GameForm : Form
    {
        private GameModel gameModel;
        private int FPS;

        public GameForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            gameModel = new GameModel(this);
            FPS = 60;
            BackColor = Color.Green;

            UpdateInventory();

            var timer = new Timer();
            timer.Interval = 1000 / FPS;
            timer.Tick += (sender, args) =>
            {
                gameModel.Update(timer.Interval);
            };
            timer.Start();
        }

        public void UpdateControls()
        {
            UpdateInventory();
            UpdateInteraction();
        }

        private void UpdateInteraction()
        {
            var estate = gameModel.Player.InteractEstate;
            if (estate != null)
            {
                EstateNameLabel.Text = estate.Name;
                InputLabel.Text = estate.Input.ToFrendlyString();
                OutputLabel.Text = estate.Output.ToFrendlyString();
                InputStorageLabel.Text = estate.InputStorage.ToString();
                OutputStorageLabel.Text = estate.OutputStorage.ToString();
            }
        }

        public void UpdateInventory()
        {
            var newText = new StringBuilder();
            foreach (var resource in (Resources[])Enum.GetValues(typeof(Resources)))
            {
                if (gameModel.Player.Inventory[resource] > 0)
                    newText.Append(resource.ToFrendlyString() + ": " + gameModel.Player.Inventory[resource] + "  ");
            }
            InventoryLabel.Text = newText.ToString();
        }

        public void InteractWith(Estate estate)
        {
            EstateNameLabel.Text = estate.Name;
            OutputLabel.Text = estate.Output.ToFrendlyString();
            if (estate.Input != Resources.Nothing)
            {
                InputLabel.Text = estate.Input.ToFrendlyString();
                InputStorageLabel.Text = estate.InputStorage.ToString();
                OutputStorageLabel.Text = estate.OutputStorage.ToString();
            }

            if (estate.Name == "Касса")
                OutputStorageLabel.Text = estate.OutputStorage.ToString();
        }

        public void StopInteraction()
        {
            EstateNameLabel.Text = "";
            InputLabel.Text = "";
            OutputLabel.Text = "";
            InputStorageLabel.Text = "";
            OutputStorageLabel.Text = "";
        }

        public PictureBox CreateGuest(Guest guest)
        {
            var pictureBox = new PictureBox();
            GuestsPictureBoxes.Add(pictureBox);
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();

            pictureBox.Image = Properties.Resources.player;
            pictureBox.Location = guest.Position;
            pictureBox.Name = "GuestPictureBox";
            pictureBox.Size = new Size(25, 40);

            Controls.Add(pictureBox);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            pictureBox.BringToFront();
            PlayerPictureBox.BringToFront();

            return pictureBox;
        }
    }
}
