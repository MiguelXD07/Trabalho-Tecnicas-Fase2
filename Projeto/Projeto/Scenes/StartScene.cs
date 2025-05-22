using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Projeto;

namespace Projeto.Scenes
{
    public class StartScene : GameScene
    {
        private MenuComponent menu;

        public MenuComponent Menu { get => menu; set => menu = value; }

        public StartScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            string[] menuItems = { "Start game", "Help", "High Score", "Quit" };

            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/RegularFont");
            SpriteFont hilightFont = g.Content.Load<SpriteFont>("fonts/HilightFont");

            Menu = new MenuComponent(game, g._spriteBatch, regularFont, hilightFont, menuItems);
            Components.Add(Menu);
        }
    }
}
