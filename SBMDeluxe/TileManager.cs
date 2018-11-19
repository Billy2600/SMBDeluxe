using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System.Reflection;

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

        public void LoadFromFile(string filename, ContentManager content, EntityManager entityManager)
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

                var tileTypes = new Dictionary<int, TileType>();
                var entityTypes = new Dictionary<int, string>();

                using (XmlReader tilemapReader = XmlReader.Create("Content\\" + tilemap))
                {
                    tilemapReader.ReadToFollowing("image");
                    string textureName = tilemapReader.GetAttribute("source");
                    texture = content.Load<Texture2D>(textureName.Substring(0, textureName.Length - 4)); // Strip file extension

                    if (tilemapReader.ReadToNextSibling("tile"))
                    {
                        do
                        {
                            int gid = System.Convert.ToInt32(tilemapReader.GetAttribute("id")) + 1;
                            TileType tileType = new TileType();

                            string tileTypeName = tilemapReader.GetAttribute("type");
                            switch (tileTypeName)
                            {
                                case "Solid":
                                    tileType = TileType.Solid;
                                    break;
                                case "NotSolid":
                                    tileType = TileType.NotSolid;
                                    break;
                                case "Entity":
                                    tileType = TileType.Entity;
                                    tilemapReader.ReadToDescendant("property");
                                    if(tilemapReader.GetAttribute("name") == "EntityName")
                                        entityTypes.Add(gid, tilemapReader.GetAttribute("value"));
                                    break;
                                default:
                                    tileType = TileType.Solid;
                                    break;
                            }
                            tileTypes.Add(gid, tileType); // Tile id's are offset from gid's by one
                        } while (tilemapReader.ReadToNextSibling("tile"));
                    }
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

                        int gid = System.Convert.ToInt32(reader.GetAttribute("gid"));
                        if (tileTypes.ContainsKey(gid))
                            tile.Type = tileTypes[gid];
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

                reader.ReadToFollowing("objectgroup");
                if (reader.ReadToDescendant("object"))
                {
                    do
                    {
                        int gid = System.Convert.ToInt32(reader.GetAttribute("gid"));
                        Vector2 pos = new Vector2(System.Convert.ToInt32(reader.GetAttribute("x")) - 16, System.Convert.ToInt32(reader.GetAttribute("y")) - 16);

                        if (entityTypes.ContainsKey(gid))
                        {
                            // If we come across a player, change position don't spawn it
                            if (entityTypes[gid] == "Player")
                                entityManager.SetPlayerPos(pos);
                            // Spawn based on name provided
                            else
                                entityManager.Add(entityTypes[gid]);
                        }
                    } while (reader.ReadToNextSibling("object"));

                }

                loaded = true;
            }
        }

        public bool CheckCollision(FloatRect objRect, FloatRect camera = null)
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
                Vector2 destPos = tile.Hitbox.GetPos();
                destPos.X -= camera.X;
                destPos.Y -= camera.Y;

                spriteBatch.Draw(texture, destPos, tile.SourceRect, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
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
