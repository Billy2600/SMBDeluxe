using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Paradigm.Entities
{
    class Bullet : Entity
    {
        private Texture2D texture = null;
        private float speed = 350f;

        public Entity Owner { get; set; }

        public Bullet(Entity owner, float x = 0, float y = 0, float width = 0, float height = 0)
        {
            Hitbox = new FloatRect(new Vector2(x, y), new Vector2(width, height));
            Owner = owner;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("red");
        }

        public override void Think(float dt)
        {
            Vector2 moveVec = new Vector2(speed, 0);
            Move(moveVec, dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, new Rectangle(

                  new Point((int)Hitbox.X, (int)Hitbox.Y),
                  new Point((int)Hitbox.width, (int)Hitbox.height)
                  ), Color.White);
            }
        }

        public override void HandleCollision(Entity other)
        {

        }
    }
}
