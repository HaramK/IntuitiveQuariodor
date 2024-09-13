using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public static class Utils
    {
        public static readonly List<Vector2Int> AllDirections = new List<Vector2Int>
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0)
        };

        public static void GetNeighborWallPositions(Vector2Int targetPos, RotationType rotType, List<Vector2Int> outList)
        {
            outList.Clear();
            if (rotType == RotationType.Horizontal)
            {
                outList.Add(targetPos + Vector2Int.up);
                outList.Add(targetPos + Vector2Int.down);
            }
            else
            {
                outList.Add(targetPos + Vector2Int.right);
                outList.Add(targetPos + Vector2Int.left);
            }
        }

        public static void GetDiagonals(Vector2Int dir, List<Vector2Int> outList)
        {
            outList.Clear();
            outList.Add(dir.LeftDiagonal());
            outList.Add(dir.RightDiagonal());
        }

        public static Vector2Int LeftDiagonal(this Vector2Int dir)
        {
            return dir + dir.LeftRotation();
        }
        
        public static Vector2Int RightDiagonal(this Vector2Int dir)
        {
            return dir + dir.RightRotation();
        }
        
        public static Vector2Int LeftRotation(this Vector2Int dir)
        {
            return new Vector2Int(-dir.y, dir.x);
        }
        
        public static Vector2Int RightRotation(this Vector2Int dir)
        {
            return new Vector2Int(dir.y, -dir.x);
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
                Mathf.RoundToInt((worldPosition.x + interval * (centerIdx)) / interval),
                Mathf.RoundToInt((worldPosition.y + interval * (centerIdx)) / interval)
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
                interval * (wallPosition.x - centerIdx + 0.5f),
                interval * (wallPosition.y - centerIdx + 0.5f),
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
                Mathf.RoundToInt((worldPosition.x + interval * (centerIdx - 0.5f)) / interval),
                Mathf.RoundToInt((worldPosition.y + interval * (centerIdx - 0.5f)) / interval)
            );
            return wallCoord;
        }
    }
}
