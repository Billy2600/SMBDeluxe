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

        public Enemy(TileManager tileManagerRef, AnimManager animManagerRef)
        {
            tileManager = tileManagerRef;
            animManager = animManagerRef;
        }
    }
}
