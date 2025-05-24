using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tecnicas_2.Models;

namespace Tecnicas_2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Mapa _map;
        private Player _player;
        private Camera _camera;
        private Texture2D _whitePixel;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1366;
            _graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _map = new Mapa();
            _camera = new Camera(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            // Player rectangle: X, Y, Width, Height
            _player = new Player(new Vector2(100, 150), 96, 128);
            _player.Scale = 1f; // or 1.5f for 50% bigger

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _map.Load(Content);

            // Create a 1x1 white texture for drawing rectangles
            _whitePixel = new Texture2D(GraphicsDevice, 1, 1);
            _whitePixel.SetData(new[] { Color.White });

            _camera.SetWorldBounds(_map.PisoLeft, _map.PisoRight, _map.PisoY);

            // Load the idle, walk, and jump sprites
            _player.IdleTexture = Content.Load<Texture2D>("Sprites/rogue_idle");
            _player.WalkTexture = Content.Load<Texture2D>("Sprites/rogue_run");
            _player.JumpTexture = Content.Load<Texture2D>("Sprites/rogue_jump");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime, _map.LeftTree.Hitbox, _map.RightTree.Hitbox);
            _camera.Follow(_player.Rect);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            _map.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}