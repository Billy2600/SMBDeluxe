﻿using System;
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
    class Player : Entity
    {
        private float speed = 350f;
        private Texture2D texture;

        public Player()
        {
            Hitbox = new FloatRect(30, 30, 16, 16);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ball");
        }

        public override void Think(float dt)
        {
            var kstate = Keyboard.GetState();

            Vector2 moveVec = new Vector2(0, 0);
            if (kstate.IsKeyDown(Keys.Up))
                moveVec.Y = -speed;

            if (kstate.IsKeyDown(Keys.Down))
                moveVec.Y = speed;

            if (kstate.IsKeyDown(Keys.Left))
                moveVec.X = -speed;

            if (kstate.IsKeyDown(Keys.Right))
                moveVec.X = speed;

            EntityManager.Add(new Brush(Hitbox.X + 100, Hitbox.Y + 100, 50, 50));

            Move(moveVec, dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Hitbox.GetPos(), null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
        }

        public override void HandleCollision(Entity other)
        {

        }
    }
}
