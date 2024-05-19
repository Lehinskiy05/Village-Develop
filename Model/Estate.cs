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
        public Rectangle Bounds => new Rectangle(Position, Size);
        public readonly Image Image;
        

        public Estate(string name, Point position, Size size, bool collidable, Image image)
        {
            Name = name;
            Position = position;
            Size = size;
            Collidable = collidable;
            Image = image;
        }
    }
}
