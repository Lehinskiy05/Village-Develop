﻿using System;
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

        public Map()
        {
            Size = new Size(1000, 600);
            Estates = new List<Estate>();
            CreateEstates();
        }

        private void CreateEstates()
        {
            Estates.Add(new Estate("Касса", new Point(310, 280), new Size(150, 100), true,
                Image.FromFile("C:\\Users\\Пользователь\\source\\repos\\Village Develop\\Assets\\Textures\\box_office.png")));
        }

        public Estate UnlockEstate()
        {
            Estate estate = LockedEstates.Dequeue();
            Estates.Add(estate);
            return estate;
        }
    }
}