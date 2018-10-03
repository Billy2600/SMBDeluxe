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
        InputManager inputManager;
        FloatRect camera;
        Entities.Player player;

        public Gameplay(ContentManager contentMgr)
        {
            inputManager = new InputManager();
            tileManager = new TileManager();
            entityManager = new EntityManager(contentMgr);
        }

        public override void Start(ContentManager content)
        {
            player = new Entities.Player(tileManager);
            entityManager.Add(player);
            entityManager.LoadContent();
            tileManager.LoadFromFile("Content\\1-1.tmx", content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            tileManager.Draw(spriteBatch, camera);
            entityManager.Draw(spriteBatch);
        }

        public override void Update(float dt)
        {
            player.SetInputs(inputManager.ReadInputs());
            entityManager.Think(dt);
        }

        public override void HandleInput()
        {

        }
    }
}
