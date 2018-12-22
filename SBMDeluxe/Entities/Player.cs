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
        private static int bounceDiff = 5; // Difference between max jump height and max height for bounce
        private float startJumpY; // Y position where we started jumping
        private bool flip; // Flip sprite flag; right by default
        private TileManager tileManager;
        private string currentAnim;
        private Inputs inputs;
        private Inputs inputsLastFrame;
        private long lastJumpPress;

        // Other entities will need to know if we're jumping or falling
        public bool Jumping { get; private set; }
        public bool Bouncing { get; private set; }
        public bool Falling { get; private set; }
        
        // Fireball stuff
        private long lastFire;
        private static long fireDelay = 1000;

        public Player(TileManager tileManagerRef, AnimManager animManagerRef, Vector2 pos)
        {
            tileManager = tileManagerRef;
            animManager = animManagerRef;
            Hitbox = new FloatRect(pos.X, pos.Y, 16, 16);
            inputsLastFrame = new Inputs { Jump = false }; // We won't be able to jump the first time if this isn't already false
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("sprites");
        }

        public void SetInputs(Inputs newInputs)
        {
            inputsLastFrame = inputs;
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

            // Take down when jump button was last pressed
            if (inputs.Jump && !inputsLastFrame.Jump)
                lastJumpPress = DateTime.Now.Ticks / 1000;

            // Begin a jump
            if(inputs.Jump && !Jumping && lastJumpPress == DateTime.Now.Ticks / 1000)
            {
                Jumping = true;
                startJumpY = Hitbox.Y;
            }
            
            // Fall after we let go of the button
            if (!inputs.Jump)
            {
                Jumping = false;
                Bouncing = false;
                Falling = true;
            }

            // Set velocity upwards if jumping or bouncing
            if((Jumping || Bouncing) && !Falling)
                velocity.Y -= jumpAccel;

            // Make us fall at the top of a jump or bounce
            if (startJumpY - Hitbox.Y > maxJumpHeight)
            {
                Jumping = false;
                Bouncing = false;
                Falling = true;
            }
            // Gravity
            if (!Jumping) velocity.Y += gravity;

            if (inputs.Run && (DateTime.Now.Ticks / 1000) - lastFire >= fireDelay)
            {
                EntityManager.Add(new Bullet(this, tileManager, Hitbox.X + (Hitbox.width /2), Hitbox.Y + (Hitbox.height /2), flip));
                lastFire = DateTime.Now.Ticks / 1000;
            }

            // Move up/down
            Move(new Vector2(0, velocity.Y), dt);
            if(tileManager.CheckCollision(Hitbox, null))
            {
                // Land on ground
                if (velocity.Y > 0 && Falling)
                {
                    Falling = false;
                }
                // Hit head on ceiling
                if(velocity.Y < 0)
                {
                    Jumping = false;
                    Falling = true;
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

            if (Jumping || Falling)
                currentAnim = "mario_jump";
            else if (velocity.X != 0.0f)
                currentAnim = "mario_run";
            else
                currentAnim = "mario_stand";
        }

        public override void Draw(SpriteBatch spriteBatch, FloatRect camera)
        {
            SpriteEffects spriteEffect = SpriteEffects.None;
            if (flip) spriteEffect = SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(texture, GetRealPos(camera), animManager.Animate(currentAnim), Color.White, 0f, new Vector2(0, 0), Vector2.One, spriteEffect, 0f);
        }

        public override void HandleCollision(Entity other)
        {
            
        }

        public void SetCamera(FloatRect camera)
        {
            camera.X = (int)((Hitbox.X + Hitbox.width / 2) - camera.width / 2); // Converted to int to prevent graphical errors via rounding

            // keep camera in bounds
            if (camera.X < 0) camera.X = 0;
        }

        // Make player bounce after landing on an enemy
        public void Bounce()
        {
            Bouncing = true;
            startJumpY = Hitbox.Y + bounceDiff;
        }
    }
}
