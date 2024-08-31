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

        public static Vector3 SlotToWorld(Vector2Int slotPosition)
        {
            var scale = Constant.playerSlotScale;
            var space = Constant.plyaerSlotSpace;
            var interval = scale + space;
            var centerIdx = Constant.BoardSize / 2;
            var worldPosition = new Vector3(
                slotPosition.x * interval - interval * (centerIdx),
                slotPosition.y * interval - interval * (centerIdx),
                0
            );
            return worldPosition;
        }

        public static Vector2Int ToSlotCoord(Vector3 worldPosition)
        {
            var scale = Constant.playerSlotScale;
            var space = Constant.plyaerSlotSpace;
            var interval = scale + space;
            var centerIdx = Constant.BoardSize / 2;
            var slotCoord = new Vector2Int(
                Mathf.RoundToInt((worldPosition.x + interval * (centerIdx + 0.5f)) / interval),
                Mathf.RoundToInt((worldPosition.y + interval * (centerIdx + 0.5f)) / interval)
            );
            return slotCoord;
        }
        
        public static Vector3 WallToWorld(Vector2Int wallPosition)
        {
            var scale = Constant.playerSlotScale;
            var space = Constant.plyaerSlotSpace;
            var interval = scale + space;
            var centerIdx = Constant.BoardSize / 2;
            var worldPosition = new Vector3(
                wallPosition.x * interval - interval * (centerIdx + 1f),
                wallPosition.y * interval - interval * (centerIdx + 1f),
                0
            );
            return worldPosition;
        }

        public static Vector2Int ToWallCoord(Vector3 worldPosition)
        {
            var scale = Constant.playerSlotScale;
            var space = Constant.plyaerSlotSpace;
            var interval = scale + space;
            var centerIdx = Constant.BoardSize / 2;
            var wallCoord = new Vector2Int(
                Mathf.RoundToInt((worldPosition.x + interval * (centerIdx + 1f)) / interval),
                Mathf.RoundToInt((worldPosition.y + interval * (centerIdx + 1f)) / interval)
            );
            return wallCoord;
        }
    }
}
