using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D9;

namespace Projeto.Models
{
    public class Spike : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;

        private Game g;

        public Spike(Game game, SpriteBatch sb, Texture2D tex, Vector2 speed) : base(game)
        {
            this.g = game;
            this.sb = sb;
            this.tex = tex;
            this.position = new Vector2(Shared.stage.X, Shared.stage.Y - tex.Height - 60);
            this.speed = speed;
        }
        public override void Update(GameTime gameTime)
        {
            position += speed;
            if (position.X < -tex.Width)
            {
                position.X = Shared.stage.X;
                speed = Vector2.Zero;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
