using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SMBDeluxe.Entities
{
    class Goomba : Enemy
    {
        private Texture2D texture;
        private static float gravity = 0.5f; // Gravity added per frame
        private static float walkSpeed = -0.25f;

        public Goomba(TileManager tileManagerRef, AnimManager animManagerRef, Vector2 pos) : base(tileManagerRef, animManagerRef, pos)
        {
            Hitbox = new FloatRect(pos.X, pos.Y, 16, 16);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("sprites");
        }

        public override void Think(float dt)
        {
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
            spriteBatch.Draw(texture, Hitbox.GetPos(), animManager.Animate("goomba_walk"), Color.White);
        }

        public override void HandleCollision(Entity other)
        {

        }
    }
}
