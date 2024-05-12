using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Develop.Model
{
    public class Player
    {
        public Point Position { get; private set; }
        public readonly Size Size;
        public Rectangle Bounds;
        private Model model;
        private Map map;

        public Player(Model model, Point position, Size size)
        {
            Position = position;
            Size = size;
            Bounds = new Rectangle(Position, Size);
            this.model = model;
            map = model.Map;
        }

        public void Move(Size step)
        {
            Point destination = Position + step;
            destination.X = Math.Min(Math.Max(0, destination.X), map.Size.Width);
            destination.Y = Math.Min(Math.Max(0, destination.Y), map.Size.Height);
            Rectangle nextBounds = new Rectangle(destination, Size);

            foreach (var building in map.Estates.Where(estate => estate.Collidable))
            {
                if (nextBounds.IntersectsWith(building.Bounds))
                    nextBounds.X = Position.X;

                if (nextBounds.IntersectsWith(building.Bounds))
                {
                    nextBounds.X = destination.X;
                    nextBounds.Y = Position.Y;
                }

                if (nextBounds.IntersectsWith(building.Bounds))
                    nextBounds.Location = Position;
            }
        }
    }
}
