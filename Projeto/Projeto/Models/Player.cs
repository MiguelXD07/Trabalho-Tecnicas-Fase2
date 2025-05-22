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
    public class Player : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;

        private int delay;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = 0;

        private int delayCounter;

        private const int ROWS = 1;
        private const int COLS = 12;

        private bool isJumping;
        private float jumpSpeed;
        private float verticalVelocity;
        private KeyboardState oldState;

        private Game g;

        public Player(Game game, SpriteBatch sb,Texture2D tex, Vector2 position, int delay) : base(game)
        {
            this.g = game;
            this.sb = sb;
            this.tex = tex;
            this.position = position;
            this.delay = delay;
            this.dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);
            createFrames();


            isJumping = false;
            jumpSpeed = 10f;
            verticalVelocity = 0f;
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


            KeyboardState ks = Keyboard.GetState();

            // jumping logic
            if (!isJumping && ks.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {
                isJumping = true;
                verticalVelocity = -jumpSpeed;

                jumpSound.Play();
            }

            if (isJumping)
            {
                position.Y += verticalVelocity;
                verticalVelocity += 0.5f;

                if (position.Y >= Shared.stage.Y - tex.Height - 60)
                {
                    isJumping = false;
                    position.Y = Shared.stage.Y - tex.Height - 60;

                }
            }

            oldState = ks;

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
