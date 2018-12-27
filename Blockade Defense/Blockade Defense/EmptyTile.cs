using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blockade_Defense
{
    class EmptyTile : Tile
    {
        //Attributes
        //Constructor
        public EmptyTile(int tileX, int tileY, Texture2D texture)
            : base (tileX, tileY, texture)
        {
            occupied = false;
        }

    }
}
