using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public class WallToken
    {
        public int id;
        public int ownerId;
        
        public RotationType rotationType;
        
        public bool isPlaced;
        public Vector2Int position;
        
        public WallToken(int id, int ownerId)
        {
            this.id = id;
            this.ownerId = ownerId;
        }
    }
}
