using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

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
            texture = null;
            tiles = new List<Tile>();
            loaded = false;
        }

        public void LoadFromFile(string filename, ContentManager content)
        {
            string tilemap = null;

            using (XmlReader reader = XmlReader.Create(filename))
            {
                reader.ReadToFollowing("tileset");
                tilemap = reader.GetAttribute("source");

                reader.ReadToFollowing("layer");
                // Tilemap width/height in tiles
                // We need these for determining the position of tiles
                int width = 0;
                //int height;

                if(reader.GetAttribute("name") == "Tile Layer 1")
                {
                    width = System.Convert.ToInt32(reader.GetAttribute("width"));
                    //height = System.Convert.ToInt32(reader.GetAttribute("height"));
                }

                if (width == 0)
                    return;

                // We need information from the tilemap
                if (tilemap == null)
                    return;

                using (XmlReader tilemapReader = XmlReader.Create("Content\\" + tilemap))
                {
                    tilemapReader.ReadToFollowing("image");
                    string textureName = tilemapReader.GetAttribute("source");
                    texture = content.Load<Texture2D>(textureName.Substring(0, textureName.Length - 4)); // Strip file extension
                }

                reader.ReadToFollowing("data");
                if(reader.ReadToDescendant("tile"))
                {
                    int x = 0;
                    int y = 0;
                    do
                    {
                        Vector2 clip = GetTileClip(System.Convert.ToInt32(reader.GetAttribute("gid")) - 1, texture.Width);

                        var tile = new Tile(new FloatRect(x * 16, y * 16, Tile.TileWidth, Tile.TileHeight), new Rectangle((int)clip.X, (int)clip.Y, Tile.TileWidth, Tile.TileHeight));
                        // TEMPORARY
                        if (System.Convert.ToInt32(reader.GetAttribute("gid")) == 44)
                            tile.Type = TileType.NotSolid;
                        else
                            tile.Type = TileType.Solid;

                        tiles.Add(tile);
                        x++;
                        if(x >= width)
                        {
                            x = 0;
                            y++;
                        }
                    } while (reader.ReadToNextSibling("tile"));
                }

                loaded = true;
            }
        }

        public bool CheckCollision(FloatRect objRect, FloatRect camera)
        {
            foreach(var tile in tiles)
            {
                // TODO: Check if object is inside camera area
                if (tile.Type == TileType.Solid && tile.Hitbox.Intersects(objRect))
                    return true;
            }

            return false;
        }

        // Draw all on-screen tiles
        public void Draw(SpriteBatch spriteBatch, FloatRect camera)
        {
            foreach (var tile in tiles)
            {
                spriteBatch.Draw(texture, tile.Hitbox.GetPos(), tile.SourceRect, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            }
        }

        private Vector2 GetTileClip(int tileNum, int textureWidth)
        {
            Vector2 clip = new Vector2();
            clip.X = tileNum % (textureWidth / Tile.TileWidth) * 16;
            clip.Y = tileNum / (textureWidth / Tile.TileWidth) * 16;
            return clip;
        }
    }
}
