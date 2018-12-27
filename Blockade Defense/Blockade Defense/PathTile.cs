using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blockade_Defense
{
    class PathTile : Tile
    {
        //Attributes
        //Constructor
        public PathTile(int tileX, int tileY, Texture2D texture)
            : base(tileX, tileY, texture)
        {
            occupied = true;
        }

    }
}
