namespace Quaridor
{
    using UnityEngine;
    
    public class Command
    {
        public int playerID;
        public CommandType type;
        public int targetId;
        public Vector2Int targetPosition;
        
        public bool IsValid()
        {
            // 이동 : 이동 규칙을 따르는지
            // 벽 : 벽을 놓을 수 있는지
            
            return true;
        }
    }
}