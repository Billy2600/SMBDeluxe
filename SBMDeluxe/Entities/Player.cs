using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SMBDeluxe.Entities
{
    class Player : Entity
    {
        private static float speed = 350f;
        private Texture2D texture;

        // Physics stuff
        private Vector2 velocity;
        private static float walkAccel = 0.5f;
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
        private InputManager.Inputs inputs;

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

        public void SetInputs(InputManager.Inputs newInputs)
        {
            inputs = newInputs;
        }

        public override void Think(float dt)
        {
            Vector2 moveVec = new Vector2(0, 0);
            if (inputs.Up)
                moveVec.Y = -speed;

            if (inputs.Down)
                moveVec.Y = speed;

            if (inputs.Left)
                moveVec.X = -speed;

            if (inputs.Right)
                moveVec.X = speed;

            if (inputs.Run && (DateTime.Now.Ticks / 1000) - lastFire >= fireDelay)
            {
                EntityManager.Add(new Bullet(this, Hitbox.X, Hitbox.Y, 5, 5));
                lastFire = DateTime.Now.Ticks / 1000;
            }

            Move(moveVec, dt);
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
