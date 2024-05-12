using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Develop.Model
{
    public class Estate
    {
        public string Name;
        public readonly Point Position;
        public readonly Size Size;
        public readonly bool Collidable;
        public Rectangle Bounds;

        public Estate(string name, Point position, Size size, bool collidable)
        {
            Name = name;
            Position = position;
            Size = size;
            Collidable = collidable;
            Bounds = new Rectangle(Position, Size);
        }
    }
}
