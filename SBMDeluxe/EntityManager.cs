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
    class EntityManager
    {
        private List<Entity> entities;
        private List<Entity> entityQueue;
        private bool added = false; // Entity added flag
        private Entity player; // Keep track of the player
        private AnimManager animManager;
        private TileManager tileManager;

        public ContentManager contentManager { get; set; }

        public EntityManager(ContentManager contentMgr, AnimManager animManagerRef, TileManager tileManagerRef)
        {
            entities = new List<Entity>();
            entityQueue = new List<Entity>();
            contentManager = contentMgr;
            animManager = animManagerRef;
            tileManager = tileManagerRef;
        }

        // Add new entity
        public void Add(Entity entity)
        {
            added = true;
            entity.EntityManager = this;
            entityQueue.Add(entity);

            // Single out and keep track of player
            if (entity is Entities.Player)
                player = entity;
        }

        // Add based on name
        public void Add(string entityName)
        {
            // I'll find a more dynamic way to do this later
            if (entityName == "Goomba")
                entityQueue.Add(new Entities.Goomba(tileManager, animManager));
        }

        // Check all collisions
        public void CheckCollisions()
        {
            foreach(var entityA in entities)
            {
                foreach(var entityB in entities)
                {
                    if (entityA == entityB)
                        continue;

                    if(entityA.Hitbox.Intersects(entityB.Hitbox))
                    {
                        entityA.HandleCollision(entityB);
                        entityB.HandleCollision(entityA);
                    }
                }
            }
        }

        // Check if any need to be deleted and delete them
        public void CheckDelete()
        {
            foreach(var entity in entities)
            {
                if(entity.DeleteMe)
                {
                    entities.Remove(entity);
                    break;
                }
            }
        }

        // Load content for all entities
        public void LoadContent()
        {
            foreach (var entity in entities)
            {
                entity.LoadContent(contentManager);
            }
        }

        // All entities will think
        public void Think(float dt)
        {
            foreach(var entity in entities)
            {
                entity.Think(dt);
            }

            if (added)
            {
                MergeEntityLists();
            }
        }

        // All entities will be drawn to the screen
        public void Draw(SpriteBatch spriteBatch, FloatRect camera)
        {
            foreach(var entity in entities)
            {
                entity.Draw(spriteBatch, camera);
            }
        }

        public void SetPlayerPos(Vector2 pos)
        {
            player.Hitbox.X = pos.X;
            player.Hitbox.Y = pos.Y;
        }

        private void MergeEntityLists()
        {
            foreach(var entity in entityQueue)
            {
                entity.LoadContent(contentManager);
            }

            entities.AddRange(entityQueue);
            entityQueue.Clear();
            added = false;
        }
    }
}
