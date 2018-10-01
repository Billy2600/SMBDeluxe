using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
                if (tile.Hitbox.Intersects(objRect))
                    return true;
            }

            return false;
        }

        // Draw all on-screen tiles
        public void Draw(SpriteBatch spriteBatch, FloatRect camera)
        {
            foreach (var tile in tiles)
            {
                spriteBatch.Draw(texture, tile.Hitbox.GetPos(), null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            }
        }
    }
}
