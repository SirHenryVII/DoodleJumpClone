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

        Camera camera;

        Texture2D Doodler;
        public static Texture2D tile_green;
        public static Texture2D tile_blue;
        public static Texture2D tile_dark_blue;
        public static Texture2D tile_white;
        public static Texture2D tile_broken_1;
        public static Texture2D tile_broken_2;
        public static Texture2D tile_broken_3;
        public static Texture2D tile_broken_4;

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

            //Innit Camera
            camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load Player Sprite
            Doodler = this.Content.Load<Texture2D>("Doodler");

            //Load Tiles
            tile_green = this.Content.Load<Texture2D>("tile_normal");
            tile_blue = this.Content.Load<Texture2D>("tile_blue");
            tile_dark_blue = this.Content.Load<Texture2D>("tile_dark_blue");
            tile_white = this.Content.Load<Texture2D>("tile_white");
            tile_broken_1 = this.Content.Load<Texture2D>("tile_broken_1");
            tile_broken_2 = this.Content.Load<Texture2D>("tile_broken_2");
            tile_broken_3 = this.Content.Load<Texture2D>("tile_broken_3");
            tile_broken_4 = this.Content.Load<Texture2D>("tile_broken_4");

            //Innit Player
            player = new Player(Doodler, new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight + 10), Color.White);

            //Innit Tiles
            Tile.Innit();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Game Over Logic
            if(player.position.Y > camera.cameraPos.Y + _graphics.PreferredBackBufferHeight + 200)
            {
                player.GameOver(camera);
            }

            //Camera Logic (I am putting it here instead of the Camera Class as I plan
            //to re-use the Camera Class for Future Projects)
            if(player.position.Y < camera.cameraPos.Y + _graphics.PreferredBackBufferHeight/2)
            {
                camera.setPos(new Vector2(0, player.position.Y - _graphics.PreferredBackBufferHeight/2));
            }
            else
            {
                camera.setPos(camera.cameraPos);
            }

            Tile.Update(camera);
            player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null, null, null, null, camera.transform);

            Tile.Draw(_spriteBatch);
            player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
