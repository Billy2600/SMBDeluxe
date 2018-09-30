using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Paradigm.States
{
    class Gameplay : GameState
    {
        EntityManager entityManager;

        public Gameplay(ContentManager contentMgr)
        {
            entityManager = new EntityManager(contentMgr);
        }

        public override void Start(ContentManager content)
        {
            entityManager.Add(new Entities.Player());
            entityManager.Add(new Entities.Brush(60, 60, 200, 50));
            entityManager.LoadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            entityManager.Draw(spriteBatch);
        }

        public override void Update(float dt)
        {
            entityManager.Think(dt);
        }

        public override void HandleInput()
        {

        }
    }
}
