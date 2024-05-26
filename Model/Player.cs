using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Develop.Model
{
    public class Player
    {
        private GameForm gameForm;
        private GameModel gameModel;
        private Map map;
        private double _x;
        private double _y;
        public Point Position => new Point((int)_x, (int)_y);
        public Rectangle Bounds => new Rectangle(Position, Size);
        public readonly Size Size;
        private double speed;
        public Dictionary<Resources, int> Inventory;
        public Estate? InteractEstate;

        public Player(GameModel gameModel, GameForm gameForm)
        {
            this.gameForm = gameForm;
            this.gameModel = gameModel;
            map = gameModel.Map;
            (_x, _y) = (350, 430);
            Size = new Size(25, 40);
            speed = 10;
            SetEvents();

            Inventory = new();
            foreach (var resource in (Resources[]) Enum.GetValues(typeof(Resources)))
            {
                Inventory[resource] = 999999; // Вот бы в жизни так
            }
        }

        private void SetEvents()
        {
            gameForm.KeyDown += (sender, keyEventArgs) =>
            {
                var x = .0;
                var y = .0;

                switch (keyEventArgs.KeyCode)
                {
                    case Keys.W:
                        y -= speed;
                        break;
                    case Keys.S:
                        y += speed;
                        break;
                    case Keys.A:
                        x -= speed;
                        break;
                    case Keys.D:
                        x += speed;
                        break;
                    case Keys.E:
                        Take();
                        break;
                    case Keys.Q:
                        Give();
                        break;
                    case Keys.Space:
                        map.Upgrade();
                        break;
                }

                if (x != 0 && y != 0)
                {
                    x /= Math.Sqrt(2);
                    y /= Math.Sqrt(2);
                }

                Move(x, y);
            };
        }

        private void Give()
        {
            if (InteractEstate != null
                && InteractEstate.Input != Resources.Nothing
                && Inventory[InteractEstate.Input] > 0)
            {
                Inventory[InteractEstate.Input]--;
                InteractEstate.Load();
            }

            gameForm.UpdateControls();
        }

        private void Take()
        {
            if (InteractEstate != null)
            {
                if (InteractEstate.Input == Resources.Nothing && InteractEstate.Output != Resources.Coin)
                {
                    Inventory[InteractEstate.Output]++;
                }
                else if (InteractEstate.OutputStorage > 0)
                {
                    InteractEstate.OutputStorage--;
                    Inventory[InteractEstate.Output]++;
                }

                gameForm.UpdateControls();
            }
        }

        public void Move(double dX, double dY)
        {
            var newX = _x + dX;
            var newY = _y + dY;

            newX = Math.Min(Math.Max(0, newX), map.Size.Width - this.Size.Width);
            newY = Math.Min(Math.Max(0, newY), map.Size.Height - this.Size.Height);

            Point destination = new Point((int)newX, (int)newY);
            Rectangle nextBounds = new Rectangle(destination.X, destination.Y + 25, Size.Width, Size.Height - 25);

            foreach (var estate in map.UnlockedEstates.Where(estate => estate.Collidable))
            {
                if (!nextBounds.IntersectsWith(estate.Bounds))
                    continue;
                newX = _x;
                nextBounds.X = Position.X;


                if (!nextBounds.IntersectsWith(estate.Bounds))
                    continue;
                newX = _x + dX;
                nextBounds.X = destination.X;
                newY = _y;
                nextBounds.Y = Position.Y;

                if (!nextBounds.IntersectsWith(estate.Bounds))
                    continue;
                newX = _x;
                nextBounds.Location = Position;
            }

            _x = newX;
            _y = newY;

            gameForm.PlayerPictureBox.Left = Position.X;
            gameForm.PlayerPictureBox.Top = Position.Y;

            CheckInteraction();

            // gameForm.Text = "X: " + _x + ", Y: " + _y;
        }

        private void CheckInteraction()
        {
            foreach (var estate in map.Estates)
            {
                if (estate.InteractionBounds.IntersectsWith(Bounds))
                {
                    gameForm.InteractWith(estate);
                    InteractEstate = estate;
                    return;
                }
            }
            InteractEstate = null;
            gameForm.StopInteraction();
        }
    }
}
