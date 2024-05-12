using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Develop.Model
{
    public class Model
    {
        public readonly Map Map;
        public readonly Player Player;
        public readonly List<Customer> Customers;

        public Model()
        {
            Map = new Map();
            Player = new Player(this, new Point(350, 430), new Size(30, 30));
            Customers = new List<Customer>();
        }
    }
}
