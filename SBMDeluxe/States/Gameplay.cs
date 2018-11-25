using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SMBDeluxe.States
{
    class Gameplay : GameState
    {
        private EntityManager entityManager;
        private TileManager tileManager;
        private InputManager inputManager;
        private AnimManager animManager;

        public Gameplay(ContentManager contentMgr)
        {
            inputManager = new InputManager();
            tileManager = new TileManager();
            animManager = new AnimManager();
            entityManager = new EntityManager(contentMgr, ref animManager, tileManager);
        }

        public override void Start(ContentManager content)
        {
            animManager.LoadFromFile("Content\\animations.xml");
            tileManager.LoadFromFile("Content\\1-1.tmx", content, entityManager); // This will spawn entities from tilemap
            //entityManager.Add(player);
            entityManager.LoadContent();   
        }

        public override void Draw(SpriteBatch spriteBatch, FloatRect camera)
        {
            entityManager.player.SetCamera(camera);
            tileManager.Draw(spriteBatch, camera);
            entityManager.Draw(spriteBatch, camera);
            entityManager.CheckDelete();
        }

        public override void Update(float dt)
        {
            animManager.gameTime = gameTime;
            entityManager.player.SetInputs(inputManager.ReadInputs());
            entityManager.Think(dt);
        }

        public override void HandleInput()
        {

        }
    }
}
