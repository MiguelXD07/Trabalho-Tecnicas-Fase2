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
    internal class Collisions : GameComponent
    {
        private Player player;
        private List<Spike> spikes;
        private SoundEffect hitSound;
        public bool isCollided = false;

        public Collisions(Game game, Player player, List<Spike> spikes) : base(game)
        {
            this.player = player;
            this.spikes = spikes;

            // Sound effect for collision
            hitSound = game.Content.Load<SoundEffect>("sounds/Hit");
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = player.getBounds();
            foreach (var spike in spikes)
            {
                Rectangle spikeRect = spike.getBounds();
                if (playerRect.Intersects(spikeRect))
                {
                    hitSound.Play();
                    isCollided = true;
                }
            }

            base.Update(gameTime);
        }
    }
}
