using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Projeto.Models
{
    public class Button : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;

        private int delay;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = 0;

        private int delayCounter;

        private const int ROWS = 2;
        private const int COLS = 5;

        private Game g;

        public Button(Game game, SpriteBatch sb,
            Texture2D tex, Vector2 position, int delay) : base(game)
        {
            this.g = game;
            this.sb = sb;
            this.tex = tex;
            this.position = position;
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

        public bool IsClicked(MouseState mouseState)
        {
            Rectangle buttonRect = new Rectangle((int)position.X, (int)position.Y,
                                                 tex.Width / COLS,
                                                 tex.Height / ROWS);
            return buttonRect.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed;
        }
    }

}
