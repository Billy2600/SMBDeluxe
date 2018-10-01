using Microsoft.Xna.Framework;

namespace SMBDeluxe
{
    enum TileType
    {
        Solid,
        NotSolid
    }

    class Tile
    {
        public TileType Type { get; set; }
        public FloatRect Hitbox { get; set; }
        public static int TileWidth = 16;
        public static int TileHeight = 16;
        public Rectangle sourceRect;

        public Tile()
        {
            Hitbox.X = 0;
            Hitbox.Y = 0;
            Hitbox.width = TileWidth;
            Hitbox.height = TileHeight;
        }
    }
}
