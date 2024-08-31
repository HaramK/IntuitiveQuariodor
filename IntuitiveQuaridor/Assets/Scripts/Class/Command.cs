namespace Quaridor
{
    using UnityEngine;
    
    public class Command
    {
        public int playerID;
        public CommandType type;
        public int id;
        public Vector2Int position;
        
        public bool IsValid()
        {
            return false;
        }
    }
}