using Microsoft.Xna.Framework;

namespace SMBDeluxe
{
    enum TileType
    {
        Solid,
        NotSolid,
        Entity
    }

    class Tile
    {
        public TileType Type { get; set; }
        public string objectName { get; set; }
        public FloatRect Hitbox { get; set; }
        public static int TileWidth = 16;
        public static int TileHeight = 16;
        public Rectangle SourceRect { get; set; }

        public Tile()
        {
            Hitbox.X = 0;
            Hitbox.Y = 0;
            Hitbox.width = TileWidth;
            Hitbox.height = TileHeight;
        }

        public Tile(FloatRect hitbox, Rectangle sourceRect)
        {
            Hitbox = hitbox;
            SourceRect = sourceRect;
        }
    }
}
