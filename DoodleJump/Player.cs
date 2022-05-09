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
        Color tint;

        public Vector2 velocity = new Vector2(0, 0);
        public Rectangle boundingBox;

        SpriteEffects direction;
        Texture2D playerSprite;

        public Player(Vector2 Position, Color Tint)
        {
            playerSprite = Game1.Doodler;
            boundingBox.X = (int)Position.X;
            boundingBox.Y = (int)Position.Y;
            boundingBox.Width = (int)playerSprite.Width/2;
            boundingBox.Height = (int)playerSprite.Height/2;
            tint = Tint;
        }

        public void Update()
        {

            //Update Keyboard State
            KeyboardState keyboardState = Keyboard.GetState();

            //Player Position/Velocity Update
            velocity.Y += 0.7f;
            boundingBox.X += (int)velocity.X;
            boundingBox.Y += (int)velocity.Y;

            if (velocity.X >= 1)
            {
                velocity.X -= 0.6f;
            }
            else if (velocity.X <= -1)
            {
                velocity.X += 0.6f;
            }
            else
            {
                velocity.X = 0;
            }

            //Floor
            if (boundingBox.Y > Game1._graphics.PreferredBackBufferHeight)
            {
                velocity.Y = -23;
            }


            //Player Input
            int maxSpeed = 20;
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                if (velocity.X > -maxSpeed && velocity.X < maxSpeed)
                {
                    velocity.X += (float)(1.4);
                    direction = SpriteEffects.None;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                if (velocity.X > -maxSpeed && velocity.X < maxSpeed)
                {
                    velocity.X -= (float)(1.4);
                    direction = SpriteEffects.FlipHorizontally;
                }
            }


            //BIDEN MODE
            if (keyboardState.IsKeyDown(Keys.LeftControl) && keyboardState.IsKeyDown(Keys.B))
            {
                playerSprite = Game1.biden;
            }


            //Spill Over
            if (boundingBox.X > Game1._graphics.PreferredBackBufferWidth + 65)
            {
                boundingBox.X = -70;
            }
            else if (boundingBox.X < -70)
            {
                boundingBox.X = Game1._graphics.PreferredBackBufferWidth + 65;
            }


            //Tile Collisions
            Tile TileToRemove = null;
            foreach(Tile tile in Tile.TileList)
            {
                if(new Rectangle(boundingBox.X, boundingBox.Y, boundingBox.Width, 10).Intersects(tile.BoundingBox) && velocity.Y >= 0)
                {
                    velocity.Y = -23;

                    if(tile.TileType == 4)
                    {
                        TileToRemove = tile;
                    }

                }
            }

            if(TileToRemove != null)
            {
                Tile.TileList.Remove(TileToRemove);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerSprite, boundingBox, null, Color.White, 0f, new Vector2(0), direction, 0f);
        }

        public void GameOver(Camera camera)
        {
            Tile.TileList.Clear();
            boundingBox.X = Game1._graphics.PreferredBackBufferWidth / 2;
            boundingBox.Y = Game1._graphics.PreferredBackBufferHeight + 10;
            velocity = new Vector2(0, 0);
            camera.cameraPos = new Vector2(0, 0);
            Game1.Score = 0;
            Tile.Innit();
        }

    }
}
