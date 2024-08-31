using UnityEngine;

namespace Quaridor
{
    public static class Constant
    {
        public static readonly int BoardSize = 9;
        public static readonly int WallTokenCount = 20;
        public static readonly float playerSlotScale = 0.75f;
        public static readonly float plyaerSlotSpace = 0.15f;
        public static Vector2Int[] PlayerStartPositions = new Vector2Int[]
        {
            new Vector2Int(BoardSize/2, 0),
            new Vector2Int(BoardSize/2, BoardSize-1),
            new Vector2Int(0, BoardSize/2),
            new Vector2Int(BoardSize-1, BoardSize/2)
        };
    }
}