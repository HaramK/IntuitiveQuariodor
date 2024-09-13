namespace Quaridor
{
    using UnityEngine;
    
    // 커맨드 class로 만들어도 괜찮을 듯 (현중, 영진) -> 취소
    public struct Command
    {
        public int playerID;
        public CommandType type;
        public Vector2Int targetPosition;
        public int targetWallId;
        public RotationType wallRotationType;
    }
}