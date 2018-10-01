using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Manages everything to do with Tiles
// Loads from file, collisions, etc.

namespace SMBDeluxe
{
    class TileManager
    {
        private List<Tile> tiles;
        private bool loaded;
        private Texture2D texture;

        public bool Loaded { get { return loaded; } }

        public TileManager()
        {
            loaded = false;
        }

        public void LoadFromFile(string filename)
        {
            loaded = true;
        }

        public bool CheckCollision(FloatRect objRect, FloatRect camera)
        {
            foreach(var tile in tiles)
            {
                if (tile.HitBox.Intersects(objRect))
                    return true;
            }

            return false;
        }

        // Draw all on-screen tiles
        public void Draw(SpriteBatch spriteBatch, FloatRect camera)
        {
            
        }
    }
}
