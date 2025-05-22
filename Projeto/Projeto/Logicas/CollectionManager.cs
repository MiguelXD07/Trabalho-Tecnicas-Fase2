using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Logicas
{
    internal class CollectionManager : GameComponent
    {
        private Player player;
        private List<Fruit> fruits;
        private SoundEffect eatSound;
		public bool isCollided { get; set; }


		public CollectionManager(Game game, Player player, List<Fruit> fruits) : base(game)
        {
            this.player = player;
            this.fruits = fruits;
			isCollided = false;

			// Sound effect for collecting
			eatSound = game.Content.Load<SoundEffect>("sounds/Collect");
        }


        public override void Update(GameTime gameTime)
        {
			Rectangle playerRect = player.getBounds();
			for (int i = fruits.Count - 1; i >= 0; i--)
			{
				Rectangle fruitRect = fruits[i].getBounds();
				if (playerRect.Intersects(fruitRect))
				{
					eatSound.Play();
					isCollided = true;
                    // Remove a fruta
                    fruits[i].Visible = false;
					Game.Components.Remove(fruits[i]);
					fruits.RemoveAt(i);
				}
			}

			base.Update(gameTime);
        }
    }
}
