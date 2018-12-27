using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blockade_Defense
{
    class Tile
    {
        //Attributes
        private Rectangle tileRectangle;
        private Texture2D tileTexture;
        protected bool occupied;
        //Constructor
        public Tile(int tileX, int tileY, Texture2D texture)
        {
            tileRectangle.X = tileX;
            tileRectangle.Y = tileY;
            tileRectangle.Width = 80;
            tileRectangle.Height = 80;
            tileTexture = texture;
        }

        public Rectangle TileRectangle
        {
            get { return tileRectangle; }
            set { tileRectangle = value; }
        }
        public Texture2D TileTexture
        {
            get { return tileTexture; }
            set { tileTexture = value; }
        }
        public bool Occupied
        {
            get { return occupied; }
            set { occupied = value; }
        }
    }
}
