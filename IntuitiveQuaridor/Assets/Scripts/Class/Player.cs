using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Quaridor
{
    public class Player
    {
        public int id;
        public string name;
        public PlayerToken token;
        public List<WallToken> walls = new List<WallToken>();
        public int WallLeftCount => walls?.Count(w => !w.isPlaced) ?? 0;
        
        public Player(int id)
        {
            this.id = id;
            this.name = "Player " + (id + 1);
            token = new PlayerToken(id);
        }
        
        public void AddWall(WallToken wall)
        {
            walls.Add(wall);
        }
    }
}
