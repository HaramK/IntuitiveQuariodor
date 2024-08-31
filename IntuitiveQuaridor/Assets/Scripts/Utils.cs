using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public static class Utils
    {
        public static Vector2Int ToUpperRightWallCoord(Vector2Int playerCoord)
        {
            return playerCoord;
        }
        public static Vector2Int ToLowerLeftWallCoord(Vector2Int playerCoord)
        {
            return playerCoord + new Vector2Int(-1, -1);
        }
        public static Vector2Int ToUpperLeftWallCoord(Vector2Int playerCoord)
        {
            return playerCoord + new Vector2Int(0, -1);
        }
        public static Vector2Int ToLowerRightWallCoord(Vector2Int playerCoord)
        {
            return playerCoord + new Vector2Int(-1, 0);
        }
    }
}
