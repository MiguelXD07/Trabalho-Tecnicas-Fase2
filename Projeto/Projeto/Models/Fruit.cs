using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
	public class Fruit : DrawableGameComponent
	{
		private SpriteBatch sb;
		private Texture2D tex;
		private Vector2 position;
		private Vector2 speed;

		private int delay;
		private Vector2 dimension;
		private List<Rectangle> frames;
		private int frameIndex = 0;

		private int delayCounter;

		private const int ROWS = 1;
		private const int COLS = 17;

		private Game g;

		public Fruit(Game game, SpriteBatch sb,
			Texture2D tex, Vector2 speed, int delay) : base(game)
		{
			this.g = game;
			this.sb = sb;
			this.tex = tex;
			this.position = new Vector2(Shared.stage.X, Shared.stage.Y - tex.Height - 170);
			this.speed = speed;
			this.delay = delay;
			this.dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);
			createFrames();
		}


		private void createFrames()
		{
			frames = new List<Rectangle>();
			for (int i = 0; i < ROWS; i++)
			{
				for (int j = 0; j < COLS; j++)
				{
					int x = j * (int)dimension.X;
					int y = i * (int)dimension.Y;
					Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
					frames.Add(r);
				}
			}
		}


		public override void Update(GameTime gameTime)
		{
			position += speed;
			if (position.X < -tex.Width)
			{
				position.X = Shared.stage.X;
				speed = Vector2.Zero;
			}

			delayCounter++;
			if (delayCounter > delay)
			{
				frameIndex++;
				if (frameIndex > ROWS * COLS - 1)
				{
					frameIndex = 0;
				}
				delayCounter = 0;
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			sb.Begin();
			if (frameIndex >= 0)
			{

				sb.Draw(tex, position, frames[frameIndex], Color.White);

			}
			sb.End();

			base.Draw(gameTime);
		}

		public Rectangle getBounds()
		{
			return new Rectangle((int)position.X, (int)position.Y, tex.Width / COLS, tex.Height);
		}
	}

}
