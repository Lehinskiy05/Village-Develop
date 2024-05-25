using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Develop.Model
{
    public class Guest
    {
        private double _x;
        private double _y;
        public Point Position => new((int)_x, (int)_y);
        public Size Size;
        public Queue<Point> Path;
        public Point NextPoint;
        public double speed;
        public Dictionary<Resources, int> Inventory;
        private GameForm gameForm;
        private GameModel gameModel;
        private Map map;
        public PictureBox pictureBox;
        public int Demand;

        public Guest(GameForm gameForm, GameModel gameModel, int demand)
        {
            this.gameForm = gameForm;
            this.gameModel = gameModel;
            map = gameModel.Map;
            Demand = demand;
            Size = new Size(25, 40);

            MakePath();

            var position = Path.Dequeue();
            (_x, _y) = (position.X, position.Y);
            NextPoint = Path.Dequeue();
            speed = 5;

            pictureBox = gameForm.CreateGuest(this);
        }

        private void MakePath()
        {
            Path = new Queue<Point>();
            Path.Enqueue(map.CheckPoints[2]);
            Path.Enqueue(map.CheckPoints[4]);
            Path.Enqueue(map.CheckPoints[3]);
            Path.Enqueue(map.CheckPoints[1]);
        }


        public void Update(int delta)
        {
            var distance = map.GetDistance(_x, _y, NextPoint.X, NextPoint.Y);
            if (distance <= delta * speed)
            {
                if (Path.Count == 0)
                {
                    MakePath();
                    var position = Path.Dequeue();
                    (_x, _y) = (position.X, position.Y);
                    NextPoint = Path.Dequeue();
                    return;
                }
                (_x, _y) = (NextPoint.X, NextPoint.Y);
                NextPoint = Path.Dequeue();
            }
            else
            {
                _x += (NextPoint.X - _x) / distance * delta;
                _x += (NextPoint.Y - _y) / distance * delta;
            }
        }
    }
}
