using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public FloatRect HitBox { get; set; }
        public static int TileWidth = 16;
        public static int TileHeight = 16;

        public Tile()
        {
            HitBox.X = 0;
            HitBox.Y = 0;
            HitBox.width = TileWidth;
            HitBox.height = TileHeight;
        }
    }
}
