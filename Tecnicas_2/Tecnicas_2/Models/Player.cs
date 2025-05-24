using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tecnicas_2.Models
{
    internal class Player
    {
        // Animation textures (sprite sheets)
        public Texture2D IdleTexture { get; set; }
        public Texture2D WalkTexture { get; set; }
        public Texture2D JumpTexture { get; set; }

        // Animation info
        private int idleFrames = 1;   // Set to your idle frame count
        private int walkFrames = 6;   // Set to your walk frame count
        private int jumpFrames = 1;   // Usually 1 for jump pose
        private int currentFrame = 0;
        private float frameTimer = 0f;
        private float frameSpeed = 0.12f; // Seconds per frame

        // State
        private enum PlayerState { Idle, Walk, Jump }
        private PlayerState state = PlayerState.Idle;
        private PlayerState lastState = PlayerState.Idle;

        // Position and movement
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public float Speed { get; set; } = 400f;
        public float JumpVelocity { get; set; } = -500f;
        public bool IsOnGround { get; private set; }
        public float Scale { get; set; } = 1f;
        private bool facingRight = true;

        private const float Gravity = 1200f;
        private const int GroundY = 668; // Adjust as needed

        // Sprite size (assumes all frames are the same size)
        private int spriteWidth;
        private int spriteHeight;

        public Player(Vector2 startPosition, int spriteWidth, int spriteHeight)
        {
            Position = startPosition;
            Velocity = Vector2.Zero;
            IsOnGround = false;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
        }

        public void Update(GameTime gameTime, Rectangle leftTreeHitbox, Rectangle rightTreeHitbox)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboard = Keyboard.GetState();

            // Horizontal movement
            float move = 0;
            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
                move -= 1;
            if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
                move += 1;

            Velocity = new Vector2(move * Speed, Velocity.Y);

            // Update facing direction
            if (move > 0)
                facingRight = true;
            else if (move < 0)
                facingRight = false;

            // Jump
            if (IsOnGround && (keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up)))
            {
                Velocity = new Vector2(Velocity.X, JumpVelocity);
                IsOnGround = false;
            }

            // Gravity
            if (!IsOnGround)
                Velocity = new Vector2(Velocity.X, Velocity.Y + Gravity * dt);

            // Predict new position
            Rectangle newRect = new Rectangle((int)(Position.X + Velocity.X * dt),(int)(Position.Y + Velocity.Y * dt),spriteWidth,spriteHeight);

            // Check collision with left and right trees (walls)
            if (Velocity.X < 0 && newRect.Intersects(leftTreeHitbox))
            {
                newRect.X = leftTreeHitbox.Right;
                Velocity = new Vector2(0, Velocity.Y);
            }
            else if (Velocity.X > 0 && newRect.Intersects(rightTreeHitbox))
            {
                newRect.X = rightTreeHitbox.Left - spriteWidth;
                Velocity = new Vector2(0, Velocity.Y);
            }

            Position = new Vector2(newRect.X, newRect.Y);

            // Simple ground collision
            if (newRect.Bottom >= GroundY)
            {
                Position = new Vector2(newRect.X, GroundY - spriteHeight);
                Velocity = new Vector2(Velocity.X, 0);
                IsOnGround = true;
            }
            else
            {
                IsOnGround = false;
            }

            // Animation state
            if (!IsOnGround)
                state = PlayerState.Jump;
            else if (move != 0)
                state = PlayerState.Walk;
            else
                state = PlayerState.Idle;

            // Reset animation frame if state changed
            if (state != lastState)
            {
                currentFrame = 0;
                frameTimer = 0f;
                lastState = state;
            }

            // Animation frame update
            frameTimer += dt;
            int frameCount;
            if (state == PlayerState.Idle)
                frameCount = idleFrames;
            else if (state == PlayerState.Walk)
                frameCount = walkFrames;
            else
                frameCount = jumpFrames;
            if (frameTimer >= frameSpeed)
            {
                frameTimer = 0f;
                currentFrame = (currentFrame + 1) % frameCount;
            }
            if (state == PlayerState.Jump)
                currentFrame = 0; // Only one frame for jump
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture;
            if (state == PlayerState.Idle)
                texture = IdleTexture;
            else if (state == PlayerState.Walk)
                texture = WalkTexture;
            else
                texture = JumpTexture;

            Rectangle sourceRect = new Rectangle(
                currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

            var effects = SpriteEffects.None;
            if (!facingRight)
                effects = SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0f, Vector2.Zero, Scale, effects, 0f);
        }

        // For collision
        public Rectangle Rect
        {
            get
            {
                int width = (int)(spriteWidth * Scale);
                int height = (int)(spriteHeight * Scale);
                return new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }
    }
}
