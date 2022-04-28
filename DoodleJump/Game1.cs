using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace DoodleJump
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player player;

        Texture2D Doodler;

        KeyboardState keyboardState;

        bool directionFacing = true;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            //Changing Resolution
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 1600;
            _graphics.ApplyChanges();

            player = new Player();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Doodler = this.Content.Load<Texture2D>("Doodler");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Update Keyboard State
            keyboardState = Keyboard.GetState();

            //Player Position Update
            player.Velocity.Y += 1;
            player.Position.X += player.Velocity.X;
            player.Position.Y += player.Velocity.Y;

            if(player.Velocity.X > 0)
            {
                player.Velocity.X -= 0.8f;
            }
            else if (player.Velocity.X < 0)
            {
                player.Velocity.X += 0.8f;
            }

            //Floor
            if (player.Position.Y > 1600)
            {
                player.Velocity.Y = -35;
            }

            //Player Input
            if(keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                if(player.Velocity.X > -5 || player.Velocity.X < 5)
                {
                    player.Velocity.X += 2;
                    directionFacing = true;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                if (player.Velocity.X > -5 || player.Velocity.X < 5)
                {
                    player.Velocity.X -= 2;
                    directionFacing = false;
                }
            }

            //Tile Collision
            //if collide with tile and velocity is poisitive
            //add 35 to y velocity

            //Spill Over
            if(player.Position.X > 900)
            {
                player.Position.X = -145;
            }
            else if (player.Position.X < -145)
            {
                player.Position.X = 900;
            }


            player.drawBoundingBox();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            float rotation = 0f;
            Vector2 origin = new Vector2(0, 240);
            Vector2 scale = new Vector2(0.7f, 0.7f);
            float layerDepth = 0f;


            _spriteBatch.Begin();

            if(directionFacing)
            {
                _spriteBatch.Draw(Doodler, player.Position, null, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
            }
            else
            {
                _spriteBatch.Draw(Doodler, player.Position, null, Color.White, rotation, origin, scale, SpriteEffects.FlipHorizontally, layerDepth);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
