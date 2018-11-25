using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMBDeluxe.Entities
{
    class Enemy : Entity
    {
        protected TileManager tileManager;

        public Enemy(TileManager tileManagerRef, AnimManager animManagerRef, Vector2 pos)
        {
            tileManager = tileManagerRef;
            animManager = animManagerRef;
        }
    }
}
