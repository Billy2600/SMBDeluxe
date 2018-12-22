using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SMBDeluxe.Entities
{
    class Goomba : Enemy
    {
        private Texture2D texture;
        private static float gravity = 5.5f; // Gravity added per frame
        private static float walkSpeed = -0.25f;
        private bool dead;
        private long timeOfDeath;
        private static long deathDelay = 700; // How long until goomba is deleted

        public Goomba(TileManager tileManagerRef, AnimManager animManagerRef, Vector2 pos) : base(tileManagerRef, animManagerRef, pos)
        {
            Hitbox = new FloatRect(pos.X, pos.Y, 16, 16);
            dead = false;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("sprites");
        }

        public override void Think(float dt)
        {
            if (dead)
            {
                if ((DateTime.Now.Ticks / 1000) - timeOfDeath >= deathDelay)
                    DeleteMe = true;

                return;
            }

            // We will always move left
            Move(new Vector2(walkSpeed, 0), dt);

            if (tileManager.CheckCollision(Hitbox, null))
            {
                Move(new Vector2(-walkSpeed, 0), dt);
            }

            // Fall if there's nothing under us
            Move(new Vector2(0, gravity), dt);
            if (tileManager.CheckCollision(Hitbox, null))
            {
                Move(new Vector2(0, -gravity), dt);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, FloatRect camera)
        {
            string currentAnim = "goomba_walk";
            if (dead) currentAnim = "goomba_dead";

            spriteBatch.Draw(texture, GetRealPos(camera), animManager.Animate(currentAnim), Color.White);
        }

        public override void HandleCollision(Entity other)
        {
            if(other is Player)
            {
                // Kill the goomba if the player falls or jumps on it
                Player player = other as Player;
                if( (player.Jumping || player.Falling) && player.Hitbox.Y < Hitbox.Y)
                {
                    dead = true;
                    timeOfDeath = DateTime.Now.Ticks / 1000;
                    player.Bounce();
                }
            }
        }
    }
}
