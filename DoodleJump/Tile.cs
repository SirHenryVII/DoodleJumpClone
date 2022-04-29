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

        Texture2D image;
        public Vector2 Position;
        public Rectangle BoundingBox = new Rectangle();
        public int TileType;

        public Tile(Vector2 Pos, int tileType, Texture2D imageP)
        {
            image = imageP;
            TileType = tileType;
            Position = Pos;
            BoundingBox.X = (int)Pos.X;
            BoundingBox.Y = (int)Pos.Y;
            BoundingBox.Width = image.Width;
            BoundingBox.Height = image.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, BoundingBox, Color.White);
        }
    }
}
