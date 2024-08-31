namespace Quaridor
{
    using UnityEngine;
    
    public struct Command
    {
        public int playerID;
        public CommandType type;
        public Vector2Int targetPosition;
        public int targetWallId;
        public RotationType wallRotationType;
        
        public bool IsValid()
        {
            if(type == CommandType.None)
            {
                return false;
            }
            // 이동 : 이동 규칙을 따르는지
            // 벽 : 벽을 놓을 수 있는지
            
            return true;
        }
    }
}