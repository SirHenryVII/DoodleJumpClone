using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DoodleJump
{
    class Player
    {
        public Vector2 Velocity = new Vector2(0, 0);
        public Vector2 Position = new Vector2(0, 0);
        public Rectangle boundingBox = new Rectangle(0, 0, 145, 35);
        
        public void drawBoundingBox()
        {
            boundingBox.X = (int)(float)Position.X;
            boundingBox.Y = (int)(float)Position.Y;
        }



    }
}
