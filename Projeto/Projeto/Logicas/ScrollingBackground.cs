using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Logicas
{
    internal class ScrollingBackground : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position1, position2;
        private Vector2 speed;

        public ScrollingBackground(Game game, SpriteBatch sb, Texture2D tex, Vector2 position, Vector2 speed) : base(game)
        {
            this.sb = sb;
            this.tex = tex;
            position1 = position;
            position2 = new Vector2(position1.X + tex.Width, position1.Y);
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            position1 -= speed;
            position2 -= speed;

            if (position1.X < -tex.Width)
            {
                position1.X = position2.X + tex.Width;
            }

            if (position2.X < -tex.Width)
            {
                position2.X = position1.X + tex.Width;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, position1, Color.White);
            sb.Draw(tex, position2, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
