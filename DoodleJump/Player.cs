﻿using System;
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

            //Player Position/Velocity Update
            velocity.Y += 1;
            position.X += velocity.X;
            position.Y += velocity.Y;

            if (velocity.X >= 1)
            {
                velocity.X -= 0.8f;
            }
            else if (velocity.X <= -1)
            {
                velocity.X += 0.8f;
            }
            else
            {
                velocity.X = 0;
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
                    velocity.Y = -35;

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
            spriteBatch.Draw(image, position, null, Color.White, 0f, new Vector2(image.Width/2, image.Height/2), new Vector2(0.6f, 0.6f), direction, 0f);
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
