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
        public Estate Destination;
        public Point NextPoint;
        public bool IsWait;
        public double speed;
        public Dictionary<Resources, int> Inventory;
        private GameForm gameForm;
        private GameModel gameModel;
        private Map map;
        public PictureBox pictureBox;
        public int TotalDemand;
        public int LocalDemand;
        private Random random;

        public Guest(GameForm gameForm, GameModel gameModel)
        {
            this.gameForm = gameForm;
            this.gameModel = gameModel;
            map = gameModel.Map;
            TotalDemand = map.AverageDemand;
            random = new Random();
            Path = new Queue<Point>();
            Inventory = new Dictionary<Resources, int>();
            foreach (var resource in map.AvailableResources)
            {
                Inventory[resource] = 0;
            }


            Size = new Size(25, 40);
            (_x, _y) = (map.CheckPoints[1].X, map.CheckPoints[1].Y);
            speed = 0.5;

            MakeRandomPath();

            pictureBox = gameForm.CreateGuest(this);
        }

        private void MakeRandomPath()
        {

            Path.Enqueue(Position);

            Destination = map.GetEstate(map.AvailableResources[random.Next(map.AvailableResources.Count)]);
            var pointList = map.DijkstraAlgorithm(Position, Destination.CheckPoint);

            foreach (var point in pointList)
            {
                Path.Enqueue(point);
            }

            var position = Path.Dequeue();
            (_x, _y) = (position.X, position.Y);
            NextPoint = Path.Dequeue();
        }

        private void MakePathTo(Point destination)
        {
            Path.Enqueue(Position);

            var pointList = map.DijkstraAlgorithm(Position, destination);
            foreach (var point in pointList)
            {
                Path.Enqueue(point);
            }
        }


        public void Update(int delta)
        {
            if (!IsWait)
            {
                var distance = map.GetDistance(_x, _y, NextPoint.X, NextPoint.Y);
                if (distance <= delta * speed)
                {
                    (_x, _y) = (NextPoint.X, NextPoint.Y);
                    if (Path.Count == 0)
                    {
                        IsWait = true;
                    }
                    else
                    {
                        NextPoint = Path.Dequeue();
                    }
                }
                else
                {
                    _x += (NextPoint.X - _x) / distance * delta * speed;
                    _y += (NextPoint.Y - _y) / distance * delta * speed;
                }

                gameForm.Text = Position + ", " + NextPoint;

                pictureBox.Location = Position;
            }
            else
            {
                if (Position == map.CheckPoints[3]) // касса
                {
                    foreach (var item in Inventory)
                    {
                        Destination.OutputStorage += item.Key.GetPrice() * item.Value;
                    }

                    MakePathTo(map.CheckPoints[1]);
                    IsWait = false;
                }
                else if (Position == map.CheckPoints[1])
                {
                    TotalDemand = map.AverageDemand;
                    foreach (var item in Inventory.Keys)
                    {
                        Inventory[item] = 0;
                    }
                    MakeRandomPath();
                }
                else
                {
                    if (LocalDemand > 0)
                    {
                        if (Destination.OutputStorage > 0)
                        {
                            Destination.OutputStorage--;
                            Inventory[Destination.Output]++;
                            LocalDemand--;
                        }
                    }
                    else
                    {
                        if (TotalDemand > 0)
                        {
                            LocalDemand = LocalDemand = Math.Min(random.Next(1, 3), TotalDemand);
                            TotalDemand -= LocalDemand;
                            MakeRandomPath();
                        }
                        else
                        {
                            MakePathTo(map.CheckPoints[3]);
                            Destination = map.GetEstate(Resources.Coin);
                        }

                        IsWait = false;
                    }
                }
              
            }
        }
    }
}
