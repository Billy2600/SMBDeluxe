using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SMBDeluxe
{
    enum Directions {  N, NE, E, SE, S, SW, W, NW };

    class Entity
    {
        public bool OnScreen { get; set; } = false;
        public bool DeleteMe { get; set; } = false; // Delete flag
        public FloatRect Hitbox { get; set; } // Used for positioning and collision
        public EntityManager EntityManager { get; set; } // So we can instantiate bullets, etc.
        public AnimManager animManager { get; set; }
        
        public virtual void LoadContent(ContentManager content) { } // Load content
        public virtual void Think(float dt) { } // Think every frame, 
        public virtual void Draw(SpriteBatch spriteBatch, FloatRect camera) { } // Draw every frame
        public virtual void HandleCollision(Entity other) { } // Called upon collision

        public virtual void Move(Vector2 move, float dt)
        {
            Hitbox.X += move.X;// * dt;
            Hitbox.Y += move.Y;// * dt;
        }

        // Get real position with camera taken into account
        public Vector2 GetRealPos(FloatRect camera)
        {
            Vector2 pos = Hitbox.GetPos();
            pos.X -= camera.X;
            return pos;
        }
    }
}
