using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Scenes;

namespace Projeto.Scenes
{
    public class HelpScene : GameScene
    {
        private Texture2D tex;
        private SpriteBatch sb;

        public HelpScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            sb = g._spriteBatch;
            tex = game.Content.Load<Texture2D>("images/HelpPage_Background");
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, Vector2.Zero, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
