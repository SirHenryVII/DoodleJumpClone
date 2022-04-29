using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DoodleJump
{
    class Tile
    {


        public Vector2 Position;
        public Rectangle boundingBox = new Rectangle();
        public int tileType;

        public Tile(Vector2 Pos, int TileType)
        {
            tileType = TileType;
            Position = Pos;
            boundingBox.X = (int)Pos.X;
            boundingBox.Y = (int)Pos.Y;
        }

        public void Update()
        { 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
