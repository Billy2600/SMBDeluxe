using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SMBDeluxe.Entities
{
    class Bullet : Entity
    {
        private Texture2D texture = null;
        private float speed = 5f;
        private TileManager tileManager;
        bool direction; // Should be same as player flip flag

        public Entity Owner { get; set; }

        public Bullet(Entity owner, TileManager tileManagerRef, float x, float y, bool direction)
        {
            Owner = owner;
            tileManager = tileManagerRef;
            Hitbox = new FloatRect(new Vector2(x, y), new Vector2(5, 5));
            this.direction = direction;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("red");
        }

        public override void Think(float dt)
        {
            if (tileManager.CheckCollision(Hitbox, null))
                DeleteMe = true;

            int direc = -1;
            if (!direction) direc = 1;

            Vector2 moveVec = new Vector2(speed * direc, 0);
            Move(moveVec, dt);
        }

        public override void Draw(SpriteBatch spriteBatch, FloatRect camera)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, new Rectangle(

                  new Point((int)(Hitbox.X - camera.X), (int)Hitbox.Y),
                  new Point((int)Hitbox.width, (int)Hitbox.height)
                  ), Color.White);
            }
        }

        public override void HandleCollision(Entity other)
        {
            DeleteMe = true;
        }
    }
}
