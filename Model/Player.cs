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
        public Rectangle Bounds => new Rectangle(Position, Size);
        public readonly Image Image;

        private GameModel gameModel;
        private Map map;

        public Player(GameModel gameModel, Point position, Size size)
        {
            Position = position;
            Size = size;
            this.gameModel = gameModel;
            map = gameModel.Map;
            Image = Image.FromFile("C:\\Users\\Пользователь\\source\\repos\\Village Develop\\Assets\\Textures\\player.png");
        }

        public void Move(Size step)
        {
            Point destination = Position + step;
            destination.X = Math.Min(Math.Max(0, destination.X), map.Size.Width);
            destination.Y = Math.Min(Math.Max(0, destination.Y), map.Size.Height);
            Rectangle nextBounds = new Rectangle(destination, Size);

            foreach (var building in map.Estates.Where(estate => estate.Collidable))
            {
                if (!nextBounds.IntersectsWith(building.Bounds))
                    return;
                nextBounds.X = Position.X;

                if (!nextBounds.IntersectsWith(building.Bounds))
                    return;
                nextBounds.X = destination.X;
                nextBounds.Y = Position.Y;

                if (!nextBounds.IntersectsWith(building.Bounds))
                    return;
                nextBounds.Location = Position;
            }
        }
    }
}
