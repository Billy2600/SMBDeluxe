using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SMBDeluxe.States
{
    class Gameplay : GameState
    {
        EntityManager entityManager;
        TileManager tileManager;
        FloatRect camera;

        public Gameplay(ContentManager contentMgr)
        {
            tileManager = new TileManager();
            entityManager = new EntityManager(contentMgr);
        }

        public override void Start(ContentManager content)
        {
            entityManager.Add(new Entities.Player());
            entityManager.LoadContent();
            tileManager.LoadFromFile("1-1.tmx");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            tileManager.Draw(spriteBatch, camera);
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
