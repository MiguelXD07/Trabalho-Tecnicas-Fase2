using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Projeto.Scenes;

namespace Projeto
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        //declare all scenes
        private StartScene startScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        private HighScoreScene highScoreScene;
        private EndingScene endingScene;

        // declare all background musics
        private Song startSceneBackgroundMusic;
        private Song backgroundMusic;
        private Song currentMusic;

        private Texture2D backgroundTitleTex;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTitleTex = this.Content.Load<Texture2D>("images/StartPage_Title");

            //instantiate all scenes here
            startScene = new StartScene(this);
            this.Components.Add(startScene);

            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            highScoreScene = new HighScoreScene(this);
            this.Components.Add(highScoreScene);

            endingScene = new EndingScene(this);
            this.Components.Add(endingScene);


            //make ONLY startscene active
            startScene.show();

            // start scene background music
            startSceneBackgroundMusic = this.Content.Load<Song>("sounds/StartSceneBackgroundMusic");
            MediaPlayer.IsRepeating = true;

            // background music
            backgroundMusic = this.Content.Load<Song>("sounds/BackgroundMusic");
        }

        protected override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    actionScene.show();
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    helpScene.show();
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    highScoreScene.LoadHighScores();
                    highScoreScene.show();
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            if (actionScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    actionScene.hide();
                    startScene.show();

                }
            }
            if (helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    helpScene.hide();
                    startScene.show();
                }
            }
            if (highScoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    highScoreScene.hide();
                    startScene.show();
                }
            }
            if (endingScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
            }

            UpdateMusic();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTitleTex, Vector2.Zero, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void UpdateMusic()
        {
            if (startScene.Enabled && currentMusic != startSceneBackgroundMusic)
            {
                MediaPlayer.Stop();
                currentMusic = startSceneBackgroundMusic;
                MediaPlayer.Play(currentMusic);
            }
            else if (actionScene.Enabled && currentMusic != backgroundMusic)
            {
                MediaPlayer.Stop();
                currentMusic = backgroundMusic;
                MediaPlayer.Play(currentMusic);
            }
        }

        public void EndActionSceneGame(int score)
        {
            Components.Remove(actionScene);
            endingScene.SetFinalScore(score);
            endingScene.show();
            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);
        }

        public void ClickBackButton()
        {
            endingScene.hide();
            Components.Remove(endingScene);
            startScene.show();
            endingScene = new EndingScene(this);
            this.Components.Add(endingScene);
        }
    }
}
