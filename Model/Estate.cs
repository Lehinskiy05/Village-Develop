using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace Village_Develop.Model
{
    public class Estate
    {
        private GameForm gameForm;
        private GameModel gameModel;
        private Map map;
        public string Name;
        public int InputStorage;
        public int OutputStorage;
        public readonly Point Position;
        public readonly Size Size;
        public readonly bool Collidable;
        public PictureBox PictureBox;
        public Resources Input;
        public Resources Output;
        private int interactArea;
        public Timer OutputTimer;
        public Point CheckPoint;

        public Rectangle Bounds => new Rectangle(Position, Size);
        public Rectangle InteractionBounds;


        public Estate(GameForm gameForm,
            GameModel gameModel,
            Map map,
            string name, 
            Point position, 
            Size size, 
            bool collidable,
            int CheckPointIndex,
            Resources input, 
            Resources output, 
            PictureBox pictureBox)
        {
            this.gameForm = gameForm;
            this.gameModel = gameModel;
            this.map = map;
            Name = name;
            Position = position;
            Size = size;
            Collidable = collidable;
            CheckPoint = map.CheckPoints[CheckPointIndex];
            Input = input;
            Output = output;
            PictureBox = pictureBox;
            interactArea = 15;
            OutputTimer = new Timer { Interval = 200 };
            OutputTimer.Tick += OnUnitDone;
            if (input == Resources.Nothing && output != Resources.Coin)
                OutputStorage = int.MaxValue;

            if (Collidable)
                InteractionBounds = new Rectangle(Position.X - interactArea, Position.Y - interactArea,
                    Size.Width + interactArea * 2, Size.Height + interactArea * 2);
            else
                InteractionBounds = Bounds;
        }

        public void Load()
        {
            InputStorage++;
            if (!OutputTimer.Enabled)
                OutputTimer.Start();
            gameForm.UpdateControls();
        }

        public void OnUnitDone(object sender, EventArgs args)
        {
            InputStorage--;
            OutputStorage++;
            if (InputStorage == 0)
                OutputTimer.Stop();
            gameForm.UpdateControls();
        }
    }
}
