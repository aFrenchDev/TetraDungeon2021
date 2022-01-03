using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;

namespace TetraDungeon
{
    class Collision
    {
        public static bool IsCollision(ushort x, ushort y, TiledMapTileLayer mapLayer)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
    }
}
