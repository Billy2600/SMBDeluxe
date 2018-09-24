﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Paradigm
{
    class EntityManager
    {
        private List<Entity> entities;
        private bool added = false; // Entity added flag
        private Entity player; // Keep track of the player

        public ContentManager contentManager { get; set; }

        public EntityManager(ContentManager contentMgr)
        {
            entities = new List<Entity>();
            contentManager = contentMgr;
        }

        // Add new entity
        public void Add(Entity entity)
        {
            added = true;
            entity.EntityManager = this;
            entities.Add(entity);

            // Single out and keep track of player
            if (entity is Entities.Player)
                player = entity;
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
            added = false;

            foreach(var entity in entities)
            {
                entity.Think(dt);
                if (added)
                {
                    entities.Last().LoadContent(contentManager);
                    return;
                }
            }
        }

        // All entities will be drawn to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var entity in entities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
