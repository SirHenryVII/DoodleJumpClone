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
        public Vector2 position;
        Color tint;

        public Vector2 velocity = new Vector2(0, 0);
        public Rectangle boundingBox => new Rectangle((int)position.X - 55, (int)position.Y, image.Width/2, image.Height/4);

        SpriteEffects direction;
        Texture2D playerSprite;

        public Player(Vector2 Position, Color Tint)
        {
            position = Position;
            tint = Tint;
            playerSprite = Game1.Doodler;
        }

        public void Update()
        {

            //Update Keyboard State
            KeyboardState keyboardState = Keyboard.GetState();

            //Player Position/Velocity Update
            velocity.Y += 0.7f;
            position.X += velocity.X;
            position.Y += velocity.Y;

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
            if (position.Y > Game1._graphics.PreferredBackBufferHeight)
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

            //BIDEN MODE
            if (keyboardState.IsKeyDown(Keys.LeftControl) && keyboardState.IsKeyDown(Keys.B))
            {
                playerSprite = Game1.biden;
            }

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                if (velocity.X > -maxSpeed && velocity.X < maxSpeed)
                {
                    velocity.X -= (float)(1.4);
                    direction = SpriteEffects.FlipHorizontally;
                }
            }


            //Spill Over
            if (position.X > Game1._graphics.PreferredBackBufferWidth + 65)
            {
                position.X = -70;
            }
            else if (position.X < -70)
            {
                position.X = Game1._graphics.PreferredBackBufferWidth + 65;
            }

            //Tile Collisions
            Tile TileToRemove = null;
            foreach(Tile tile in Tile.TileList)
            {
                if(boundingBox.Intersects(tile.BoundingBox) && velocity.Y >= 0)
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
            spriteBatch.Draw(playerSprite, position, null, Color.White, 0f, new Vector2(image.Width/2, image.Height/2), new Vector2(0.4f, 0.4f), direction, 0f);
        }

        public void GameOver(Camera camera)
        {
            Tile.TileList.Clear();
            position = new Vector2(Game1._graphics.PreferredBackBufferWidth / 2, Game1._graphics.PreferredBackBufferHeight + 10);
            velocity = new Vector2(0, 0);
            camera.cameraPos = new Vector2(0, 0);
            Game1.Score = 0;
            Tile.Innit();
        }

    }
}
