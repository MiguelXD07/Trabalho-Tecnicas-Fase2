using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Models;
using System.IO;
using Projeto;
using Projeto.Scenes;

namespace Projeto.Scenes
{
	internal class EndingScene : GameScene
    {
        private Texture2D tex;
        private SpriteBatch sb;
        private SpriteFont font;
        private Vector2 position;
        private Color scoreColor = Color.Purple;

        private int finalScore;
        private string scoreText;

        private Button backButton;

        private Game1 g;

        // Variaveis para salvar o nome do jogador
        private string playerName = "";
		private bool nameEntered = false;
		private KeyboardState oldState;

		public EndingScene(Game game) : base(game)
        {
            this.g = (Game1)game;
            sb = g._spriteBatch;
            tex = game.Content.Load<Texture2D>("images/EndingPage_Background");
            font = game.Content.Load<SpriteFont>("fonts/FinalFont");
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);

            Texture2D buttonTex = game.Content.Load<Texture2D>("images/ArrowButton");
            Vector2 buttonPosition = new Vector2(10, 10);
            int buttonDelay = 10;
            backButton = new Button(game, g._spriteBatch, buttonTex, buttonPosition, buttonDelay);
            Components.Add(backButton);
        }

        public void SetFinalScore(int score)
        {
            finalScore = score;
            scoreText = $"Final Score: {finalScore}\nTo Exit, press the esc";
        }

		private void SaveScore()
		{
			// Salva o jogo num ficheiro de texto gameScore.txt
			string filePath = Path.Combine(Environment.CurrentDirectory, "gameScores.txt");
			string userScore= $"{playerName}|{finalScore}\n";
			File.AppendAllText(filePath, userScore);
		}

		public override void Update(GameTime gameTime)
        {
            backButton.Update(gameTime);
            MouseState mouseState = Mouse.GetState();
            if (backButton.IsClicked(mouseState))
            {
                g.ClickBackButton();
            }

			KeyboardState ks = Keyboard.GetState();
			if (!nameEntered)
			{
				if (ks.IsKeyDown(Keys.Back) && oldState.IsKeyUp(Keys.Back) && playerName.Length > 0)
				{
					playerName = playerName.Remove(playerName.Length - 1);
				}
				else if (ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter) && playerName.Length > 2)
				{
					nameEntered = true;
					SaveScore();
				}
				else
				{
					Keys[] keys = ks.GetPressedKeys();
					foreach (var key in keys)
					{
						if (playerName.Length < 6 && key >= Keys.A && key <= Keys.Z && oldState.IsKeyUp(key))
						{
							playerName += key.ToString();
						}
					}
				}
			}
			oldState = ks;
			base.Update(gameTime);
        }

		public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, Vector2.Zero, Color.White);
            sb.DrawString(font, scoreText, position, scoreColor);
			if (!nameEntered)
			{
				string inputText = $"Enter Your Name: {playerName}";
				sb.DrawString(font, inputText, new Vector2(100, 150), Color.Black);
			}
			sb.End();

            base.Draw(gameTime);
        }
    }
}
