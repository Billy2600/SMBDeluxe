using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SMBDeluxe.Entities
{
    class Player : Entity
    {
        private Texture2D texture;

        // Physics stuff
        private Vector2 velocity;
        private static float walkAccel = 0.5f;
        private static float runAccel = 1f;
        private static float jumpAccel = 0.7f;
        private static float gravity = 0.5f; // Gravity added per frame
        private static float speedLimit = 3f; // Limit to running speed
        private static int maxJumpHeight = 30; // Maximum jump height (in pixels)
        private static float jumpDelay = 1000;
        private float startJumpY; // Y position where we started jumping
        private float lastJump; // Time of last jump
        private bool jumping;
        private bool falling;
        private bool flip; // Flip sprite flag
        private TileManager tileManager;
        private Inputs inputs;

        // Fireball stuff
        private long lastFire;
        private static long fireDelay = 500;

        public Player(TileManager tileManagerRef)
        {
            tileManager = tileManagerRef;
            Hitbox = new FloatRect(30, 30, 16, 16);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("sprites");
        }

        public void SetInputs(Inputs newInputs)
        {
            inputs = newInputs;
        }

        public override void Think(float dt)
        {
            float accel = walkAccel;
            if (inputs.Run)
                accel = runAccel;

            if (inputs.Left && velocity.X >= -speedLimit) velocity.X -= accel;
            if (inputs.Right && velocity.X <= speedLimit) velocity.X += accel;
            if (!inputs.Left && velocity.X < 0) velocity.X += accel;
            if (!inputs.Right && velocity.X > 0) velocity.X -= accel;
            // Flip sprite
            if (inputs.Left) flip = true;
            if (inputs.Right) flip = false;

            // Jump
            if(inputs.Jump && !falling)
            {
                // Begin moving upward
                velocity.Y -= jumpAccel;
                // Begin a jump
                if(!jumping)
                {
                    jumping = true;
                    startJumpY = Hitbox.Y;
                }
            }
            if (!inputs.Jump)
            {
                jumping = false;
                falling = true;
            }

            // Make us fall at the top of a jump
            if(startJumpY - Hitbox.Y > maxJumpHeight)
            {
                jumping = false;
                falling = true;
            }
            // Gravity
            if (!jumping) velocity.Y += gravity;

            if (inputs.Run && (DateTime.Now.Ticks / 1000) - lastFire >= fireDelay)
            {
                EntityManager.Add(new Bullet(this, Hitbox.X, Hitbox.Y, 5, 5));
                lastFire = DateTime.Now.Ticks / 1000;
            }

            // Move up/down
            Move(new Vector2(0, velocity.Y), dt);
            if(tileManager.CheckCollision(Hitbox, null))
            {
                // Land on ground
                if (velocity.Y > 0 && falling)
                    falling = false;
                // Hit head on ceiling
                if(velocity.Y < 0)
                {
                    jumping = false;
                    falling = true;
                }
                // Move back and stop velocity
                Move(new Vector2(0, -velocity.Y), dt);
                velocity.Y = 0.0f;
            }

            // Move left/right
            Move(new Vector2(velocity.X, 0), dt);
            if(tileManager.CheckCollision(Hitbox, null))
            {
                // Move back and stop velocity
                Move(new Vector2(-velocity.X, 0), dt);
                velocity.X = 0.0f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Hitbox.GetPos(), new Rectangle(0, 24, 16, 16), Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
        }

        public override void HandleCollision(Entity other)
        {

        }
    }
}
