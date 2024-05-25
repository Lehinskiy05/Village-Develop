using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Develop.Model
{
    public class Map
    {
        public readonly Size Size;
        public readonly List<Estate> Estates;
        public readonly Queue<Estate> LockedEstates;
        private GameForm gameForm;
        private GameModel gameModel;
        public List<Point> CheckPoints;
        public Dictionary<Point, List<Point>> Ways;

        public Map(GameForm gameForm, GameModel gameModel)
        {
            this.gameForm = gameForm;
            this.gameModel = gameModel;
            Size = new Size(1000, 600);
            Estates = new List<Estate>();
            LockedEstates = new Queue<Estate>();

            CreateCheckPoints();
            CreateEstates();


            // Потом убрать
            while (LockedEstates.Count > 0)
                UnlockEstate();
        }

        private void CreateCheckPoints()
        {
            CheckPoints = new List<Point>
            {
                new(265, 480),
                new(480, 610),
                new(215, 490),
                new(530, 465),
                new(340, 520),
                new(330, 300),
                new(220, 225),
                new(520, 210),
                new(780, 250),
                new(990, 480),
                new(960, 710),
                new(750, 660),
                new(650, 500),
                new(650, 330),
                new(900, 580),
                new(170, 630),
            };

            Ways = new Dictionary<Point, List<Point>>();
            CreateWays(0, 11, 14, 9, 8, 13);
            CreateWays(1, 4, 3, 12, 11, 15);
            CreateWays(2, 15, 1, 4);
            CreateWays(3, 4, 1, 12);
            CreateWays(4, 2, 15, 1, 12, 3);
            CreateWays(5, 6, 7, 13);
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

        private void CreateEstates()
        {
            Estates.Add(new Estate(gameForm, gameModel, this, "Касса", 
                new Point(310, 280), new Size(150, 100), true, 3,
                Resources.Nothing, Resources.Coin, gameForm.CassPictureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Лес", 
                new Point(39, 416), new Size(200, 160), false, 15,
                Resources.Nothing, Resources.Wood, gameForm.CassPictureBox));

            LockedEstates.Enqueue(new Estate(gameForm, gameModel, this, "Лесопилка", 
                new Point(110, 290), new Size(112, 100), true, 2,
                Resources.Wood, Resources.Board, gameForm.SawmillPictureBox));

            LockedEstates.Enqueue(new Estate(gameForm, gameModel, this, "Плотницкая",
                new Point(126, 96), new Size(110, 100), true, 6,
                Resources.Board, Resources.Chair, gameForm.CarpentryPictureBox));

            LockedEstates.Enqueue(new Estate(gameForm, gameModel, this, "Колодец",
                new Point(560, 310), new Size(80, 80), true, 0,
                Resources.Nothing, Resources.Water, gameForm.PitPictureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Пшеница",
                new Point(489, 430), new Size(200, 160), false, 11,
                Resources.Water, Resources.Wheat, gameForm.WheatPictureBox));

            LockedEstates.Enqueue(new Estate(gameForm, gameModel, this, "Мельница",
                new Point(734, 462), new Size(112, 100), true, 10,
                Resources.Wheat, Resources.Flour, gameForm.MillPitureBox));

            LockedEstates.Enqueue(new Estate(gameForm, gameModel, this, "Пекарня", 
                new Point(749, 290), new Size(112, 100), true, 9,
                Resources.Flour, Resources.Bread, gameForm.BakeryPictureBox));

            Estates.Add(new Estate(gameForm, gameModel, this, "Виноград", 
                new Point(549, 63), new Size(140, 160), false, 8,
                Resources.Water, Resources.Grape, gameForm.GrapeTreePictureBox));

            LockedEstates.Enqueue(new Estate(gameForm, gameModel, this, "Винодельня",
                new Point(350, 82), new Size(112, 100), true, 7,
                Resources.Grape, Resources.Wine, gameForm.WineryPictureBox));
        }

        public void UnlockEstate()
        {
            Estate estate = LockedEstates.Dequeue();
            Estates.Add(estate);
            estate.PictureBox.Left = estate.Position.X;
            estate.PictureBox.Top = estate.Position.Y;
        }

        public double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }

    public static class PointExtension
    {
        public static double DistanceTo(this Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }
}
