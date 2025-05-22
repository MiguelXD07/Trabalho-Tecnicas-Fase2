using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Projeto.Scenes;

namespace Projeto.Scenes
{
	public class HighScoreScene : GameScene
	{
		private Texture2D tex;
		private SpriteBatch sb;

		private SpriteFont scoreFont;
		private List<(string Name, int Score)> highScores;


		public HighScoreScene(Game game) : base(game)
		{
			Game1 g = (Game1)game;
			sb = g._spriteBatch;
			tex = game.Content.Load<Texture2D>("images/EndingPage_Background");

			scoreFont = game.Content.Load<SpriteFont>("fonts/RegularFont");
			LoadHighScores();
		}

		public void LoadHighScores()
		{
			highScores = new List<(string Name, int Score)>();
			string filePath = Path.Combine(Environment.CurrentDirectory, "gameScores.txt");
			string[] lines = File.ReadAllLines(filePath);
			foreach (var line in lines)
			{
				var parts = line.Split('|');
				if (parts.Length == 2)
				{
					string name = parts[0];
					if (int.TryParse(parts[1], out int score))
					{
						highScores.Add((name, score));
					}
				}
			}
			highScores = highScores.OrderByDescending(hs => hs.Score).Take(5).ToList();
		}

		public override void Draw(GameTime gameTime)
		{
			sb.Begin();
			sb.Draw(tex, Vector2.Zero, Color.White);

			Vector2 scorePosition = new Vector2(Shared.stage.X / 3, Shared.stage.Y / 3);
			for (int i = 0; i < highScores.Count; i++)
			{
				string scoreText = $"{i + 1}. {highScores[i].Name} - {highScores[i].Score}";
				sb.DrawString(scoreFont, scoreText, scorePosition, Color.Black);
				scorePosition.Y += 30;
			}
			sb.End();

			base.Draw(gameTime);
		}
	}
}
