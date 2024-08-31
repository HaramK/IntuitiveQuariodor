using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public class Player
    {
        public int id;
        public string name;
        public PlayerToken token = new PlayerToken();
        public List<WallToken> walls = new List<WallToken>();
        
        public Player(int id)
        {
            this.id = id;
            this.name = "Player " + (id + 1);
        }
        
        public void AddWall(WallToken wall)
        {
            walls.Add(wall);
        }
    }
}
