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
        Texture2D image;
        Vector2 position;
        Color tint;

        public Vector2 velocity = new Vector2(0, 0);
        public Rectangle boundingBox => new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);

        SpriteEffects direction;

        public Player(Texture2D Image, Vector2 Position, Color Tint)
        {
            image = Image;
            position = Position;
            tint = Tint;
        }

        public void Update()
        {

            //Update Keyboard State
            KeyboardState keyboardState = Keyboard.GetState();

            //Player Position Update
            velocity.Y += 1;
            position.X += velocity.X;
            position.Y += velocity.Y;

            if (velocity.X >= 2)
            {
                velocity.X -= 0.8f;
            }
            else if (velocity.X <= 2)
            {
                velocity.X += 0.8f;
            }

            //Floor
            if (position.Y > Game1._graphics.PreferredBackBufferHeight)
            {
                velocity.Y = -35;
            }


            //Player Input
            int maxSpeed = 40;
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                if (velocity.X > -maxSpeed && velocity.X < maxSpeed)
                {
                    velocity.X += 2;
                    direction = SpriteEffects.None;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                if (velocity.X > -maxSpeed && velocity.X < maxSpeed)
                {
                    velocity.X -= 2;
                    direction = SpriteEffects.FlipHorizontally;
                }
            }

            if (position.X > Game1._graphics.PreferredBackBufferWidth)
            {
                position.X = -145;
            }
            else if (position.X < -145)
            {
                position.X = Game1._graphics.PreferredBackBufferWidth;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, null, Color.White, 0f, new Vector2(image.Width/2, image.Height/2), new Vector2(0.6f, 0.6f), direction, 0f);
        }

    }
}
