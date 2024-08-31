using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public class PlayerToken
    {
        public int ownerId;
        public Vector2Int position;
        
        public PlayerToken(int ownerId)
        {
            this.ownerId = ownerId;
        }
    }
}