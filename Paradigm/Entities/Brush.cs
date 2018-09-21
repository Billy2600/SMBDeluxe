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
        public Brush(float x = 0, float y = 0, float width = 0, float height = 0)
        {
            Hitbox.X = x;
            Hitbox.Y = y;
            Hitbox.width = width;
            Hitbox.height = height;
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void Think(float dt)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

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
