using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blockade_Defense
{
    class Tower
    {
        // attibutes
        int towerX;
        int towerY;
        Rectangle towerRec;
        bool active;

        // constructor
        public Tower(int x, int y)
        {
            towerX = x;
            towerY = y;
            towerRec.X = towerX;
            towerRec.Y = towerY;
            towerRec.Width = 80;
            towerRec.Height = 80;
            active = true;
        }

        // Properties
        public int TowerX
        {
            get { return towerX; }
            set { towerX = value; }
        }
        public int TowerY
        {
            get { return towerY; }
            set { towerY = value; }
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public Rectangle TowerRec
        {
            get { return towerRec; }
            set { towerRec = value; }
        }


    }
}
