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
    class GameState
    {
        public GameTime gameTime;

        public virtual void Start(ContentManager content) { } // Load content and start state
        public virtual void Draw(SpriteBatch spriteBatch, FloatRect camera) { }
        public virtual void Update(float dt) { }
        public virtual void HandleInput() { }

    }
}
