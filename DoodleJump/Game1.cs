using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace DoodleJump
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player player;

        Texture2D Doodler;
        Texture2D pixel;

        KeyboardState keyboardState;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Doodler = this.Content.Load<Texture2D>("Doodler");
            pixel = this.Content.Load<Texture2D>("pixel");

            player = new Player(Doodler, new Vector2(0), Color.White);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Tile Collision
            //if collide with tile and velocity is poisitive
            //add 35 to y velocity

            player.Update();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            player.Draw(_spriteBatch);
            _spriteBatch.Draw(pixel, player.boundingBox, Color.Red);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
