using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Village_Develop.Model
{
    public class Map
    {
        public readonly Size Size;
        public readonly List<Estate> Estates;
        public readonly List<Estate> UnlockedEstates;
        public readonly Queue<Estate> LockedEstates;
        private GameForm gameForm;
        private GameModel gameModel;
        public List<Point> CheckPoints;
        public Dictionary<Point, List<Point>> Ways;
        public List<Resources> AvailableResources;
        public int AverageDemand;
        public int Stage;
        public List<int> UpgradesPrises;

        public Map(GameForm gameForm, GameModel gameModel)
        {
            this.gameForm = gameForm;
            this.gameModel = gameModel;
            Size = new Size(1000, 600);
            Estates = new List<Estate>();
            UnlockedEstates = new List<Estate>();
            LockedEstates = new Queue<Estate>();
            UnlockedEstates = new List<Estate>();
            AverageDemand = 1;
            Stage = 0;
            UpgradesPrises = new List<int>{ 5, 10, 15, 20, 30, 40, 50, 70 };

            CreateCheckPoints();
            CreateEstates();

            UnlockedEstates.Add(Estates[0]);
            UnlockedEstates.Add(Estates[1]);

            foreach (var estate in Estates.Skip(2))
            {
                LockedEstates.Enqueue(estate);
            }

            AvailableResources = new List<Resources>();
        }

        private void CreateCheckPoints()
        {
            CheckPoints = new List<Point>
            {
                new(650, 330), // 0
                new(400, 610), // 1
                new(150, 370), // 2
                new(400, 360), // 3
                new(260, 400), // 4
                new(260, 230), // 5
                new(170, 180), // 6
                new(400, 160), // 7
                new(580, 170), // 8
                new(740, 370), // 9
                new(730, 540), // 10
                new(570, 500), // 11
                new(500, 380), // 12
                new(500, 250), // 13
                new(680, 440), // 14
                new(130, 480), // 15
            };

            Ways = new Dictionary<Point, List<Point>>();
            CreateWays(0, 11, 14, 9, 8, 13);
            CreateWays(1, 4, 3, 12, 11, 15);
            CreateWays(2, 15, 1, 4);
            CreateWays(3, 4, 1, 12);
            CreateWays(4, 2, 15, 1, 12, 3, 5);
            CreateWays(5, 6, 7, 13, 4);
            CreateWays(6, 5, 7);
            CreateWays(7, 6, 5, 13, 8);
            CreateWays(8, 7, 13, 0, 9);
            CreateWays(9, 8, 0, 14, 12);
            CreateWays(10, 11, 14);
            CreateWays(11, 10, 14, 0, 12, 1);
            CreateWays(12, 1, 3, 4, 13, 9, 14, 11);
            CreateWays(13, 5, 7, 8, 0, 12);
            CreateWays(14, 10, 11, 0, 12, 9);
            CreateWays(15, 2, 4, 1);
        }

        private void CreateWays(int start, params int[] finishes)
        {
            Ways[CheckPoints[start]] = new List<Point>();
            foreach (var finish in finishes)
            {
                Ways[CheckPoints[start]].Add(CheckPoints[finish]);
            }
        }

        public Estate GetEstate(Resources resource)
        {
            Dictionary<Resources, Estate> toEstate = new()
            {
                { Resources.Coin,  Estates[0] },
                { Resources.Wood,  Estates[1] },
                { Resources.Board, Estates[2] },
                { Resources.Chair, Estates[3] },
                { Resources.Water, Estates[4] },
                { Resources.Wheat, Estates[5] },
                { Resources.Flour, Estates[6] },
                { Resources.Bread, Estates[7] },
                { Resources.Grape, Estates[8] },
                { Resources.Wine,  Estates[9] },
            };

            return toEstate[resource];
        }

        private void CreateEstates()
        {
            Estates.Add(new Estate(gameForm, gameModel, this, "Касса", 
                new Point(310, 280), new Size(150, 100), true, 3,
                Resources.Nothing, Resources.Coin, gameForm.CassPictureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Лес", 
                new Point(39, 416), new Size(200, 160), false, 15,
                Resources.Nothing, Resources.Wood, gameForm.CassPictureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Лесопилка", 
                new Point(110, 290), new Size(112, 100), true, 2,
                Resources.Wood, Resources.Board, gameForm.SawmillPictureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Плотницкая",
                new Point(126, 96), new Size(110, 100), true, 6,
                Resources.Board, Resources.Chair, gameForm.CarpentryPictureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Колодец",
                new Point(560, 310), new Size(80, 80), true, 0,
                Resources.Nothing, Resources.Water, gameForm.PitPictureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Пшеница",
                new Point(489, 430), new Size(200, 160), false, 11,
                Resources.Water, Resources.Wheat, gameForm.WheatPictureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Мельница",
                new Point(734, 462), new Size(112, 100), true, 10,
                Resources.Wheat, Resources.Flour, gameForm.MillPitureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Пекарня", 
                new Point(749, 290), new Size(112, 100), true, 9,
                Resources.Flour, Resources.Bread, gameForm.BakeryPictureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Виноград", 
                new Point(549, 63), new Size(140, 160), false, 8,
                Resources.Water, Resources.Grape, gameForm.GrapeTreePictureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Винодельня",
                new Point(350, 82), new Size(112, 100), true, 7,
                Resources.Grape, Resources.Wine, gameForm.WineryPictureBox));
        }

        public double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public List<Point> DijkstraAlgorithm(Point start, Point? end)
        {
            {
                var notVisited = new List<Point>(CheckPoints);
                var track = new Dictionary<Point, DijkstraData>();
                track[start] = new DijkstraData { Price = 0, Previous = null };

                while (true)
                {
                    Point? toOpen = null;
                    var bestPrice = double.PositiveInfinity;
                    foreach (var e in notVisited)
                    {
                        if (track.ContainsKey(e) && track[e].Price < bestPrice)
                        {
                            bestPrice = track[e].Price;
                            toOpen = e;
                        }
                    }

                    if (toOpen == null) 
                        return null;
                    if (toOpen == end) break;

                    foreach (var nextPoint in Ways[toOpen.GetValueOrDefault()])
                    {
                        var currentPrice = track[toOpen.GetValueOrDefault()].Price + toOpen.GetValueOrDefault().DistanceTo(nextPoint);
                        if (!track.ContainsKey(nextPoint) || track[nextPoint].Price > currentPrice)
                        {
                            track[nextPoint] = new DijkstraData { Previous = toOpen, Price = currentPrice };
                        }
                    }

                    notVisited.Remove(toOpen.GetValueOrDefault());
                }

                var result = new List<Point>();
                while (end != Point.Empty)
                {
                    result.Add(end.GetValueOrDefault());
                    end = track[end.GetValueOrDefault()].Previous.GetValueOrDefault();
                }
                result.Reverse();
                return result;
            }
        }

        public void Upgrade()
        {
            if (Stage < UpgradesPrises.Count && gameModel.Player.Inventory[Resources.Coin] >= UpgradesPrises[Stage])
            {
                UnlockEstate();

                gameModel.Player.Inventory[Resources.Coin] -= UpgradesPrises[Stage];
                Stage++;
                AverageDemand++;
                gameModel.Guests.Add(new Guest(gameForm, gameModel));
                if (Stage < UpgradesPrises.Count)
                    gameForm.UpgradeInfoLabel.Text = "Следующий апгрейд:\n" + UpgradesPrises[Stage] + " монет";
                else
                {
                    gameForm.UpgradeInfoLabel.Text = "Конец";
                }
            }
        }

        public void UnlockEstate()
        {
            Estate estate = LockedEstates.Dequeue();
            UnlockedEstates.Add(estate);
            estate.PictureBox.Left = estate.Position.X;
            estate.PictureBox.Top = estate.Position.Y;

            AvailableResources.Add(estate.Output);
            foreach (var guest in gameModel.Guests)
            {
                guest.Inventory[estate.Output] = 0;
            }
        }
    }

    public class DijkstraData
    {
        public Point? Previous;
        public double Price;
    }

    public static class PointExtension
    {
        public static double DistanceTo(this Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }
}
