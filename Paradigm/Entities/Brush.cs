using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Paradigm.Entities
{
    // Much like the Brush object in the Quake and Source engines
    // Used for level geometry
    class Brush : Entity
    {
        private Texture2D texture;

        public Brush(float x = 0, float y = 0, float width = 0, float height = 0)
        {
            Hitbox = new FloatRect(new Vector2(x, y), new Vector2(width, height));
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("red");
        }

        public override void Think(float dt)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, Hitbox.GetPos(), null, Color.White, 0f, new Vector2(Hitbox.width / 2, Hitbox.height / 2), Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, new Rectangle(
                new Point((int)Hitbox.X, (int)Hitbox.Y),
                new Point((int)Hitbox.width, (int)Hitbox.height)
                ), Color.White);
        }

        public override void HandleCollision(Entity other)
        {

        }

        public override void Move(Vector2 move, float dt)
        {
            // Do nothing by default
        }
    }
}
