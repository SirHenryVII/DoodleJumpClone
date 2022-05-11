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
        public bool Left = true;
        public Texture2D image;
        public Rectangle BoundingBox = new Rectangle();
        public int TileType;
        public static List<Tile> TileList = new List<Tile>();
        public static Random random = new Random();

        public Tile(Vector2 Pos, int tileType, Texture2D newImage)
        {
            image = newImage;
            TileType = tileType;
            BoundingBox.X = (int)Pos.X;
            BoundingBox.Y = (int)Pos.Y;
            BoundingBox.Width = (int)(image.Width / 1.7);
            BoundingBox.Height = (int)(image.Height / 1.7);
        }

        public static void Innit()
        {
            //Generate Initial Tiles
            for(int i = Game1._graphics.PreferredBackBufferHeight - 250; i > 0; i -= 250)
            {
                TileList.Add(new Tile(new Vector2(random.Next(0, Game1._graphics.PreferredBackBufferWidth - Game1.tile_green.Width - 3), i), 1, Game1.tile_green));
            }
        }

        public static void Update(Camera camera)
        {
            //Remove Unseen Tiles
            List<Tile> tilesToRemove = new List<Tile>();
            foreach (Tile tile in TileList)
            {
                if(tile.BoundingBox.Y > camera.cameraPos.Y + Game1._graphics.PreferredBackBufferHeight + 20)
                {
                    tilesToRemove.Add(tile);
                }
            }

            foreach (Tile tile in tilesToRemove)
            {
                TileList.Remove(tile);
            }

            //Spawn Tiles
            if (TileList[TileList.Count - 1].BoundingBox.Y > camera.cameraPos.Y + 250)
            {
                int tempTileNum = random.Next(0, 5);
                Texture2D tempTile = Game1.tile_green;


                switch (tempTileNum)
                {

                    case 1:
                        tempTile = Game1.tile_green;
                        break;

                    case 2:
                        tempTile = Game1.tile_blue;
                        break;

                    case 3:
                        tempTile = Game1.tile_dark_blue;
                        break;

                    case 4:
                        tempTile = Game1.tile_white;
                        break;
                    
                }

                TileList.Add(new Tile(new Vector2(random.Next(0, Game1._graphics.PreferredBackBufferWidth - Game1.tile_blue.Width - 3), camera.cameraPos.Y - 20), tempTileNum, tempTile));
            }

            //Tile Events
            foreach(Tile tile in TileList)
            {
                switch (tile.TileType)
                {

                    case 3:
                        tile.BoundingBox.Y += 1;
                        break;

                    case 2:
                        if(tile.Left)
                        {
                            if(tile.BoundingBox.X > 0)
                            {
                                tile.BoundingBox.X -= 3;
                            }
                            else
                            {
                                tile.Left = false;
                                tile.BoundingBox.X += 3;
                            }
                        }
                        else
                        {
                            if (tile.BoundingBox.X < Game1._graphics.PreferredBackBufferWidth - tile.image.Width)
                            {
                                tile.BoundingBox.X += 3;
                            }
                            else
                            {
                                tile.Left = true;
                                tile.BoundingBox.X -= 3;
                            }
                        }
                        break;


                }
            }

        }


        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile tile in TileList)
            {
                spriteBatch.Draw(tile.image, tile.BoundingBox, Color.White);
            }
        }
    }
}
