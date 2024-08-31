using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public class Board
    {
        public WallToken[] wallTokenSlots;
        
        public Board(Player[] players, int wallCountPerPlayer)
        {
            wallTokenSlots = new WallToken[Constant.WallTokenCount];
            for (int i = 0; i < Constant.WallTokenCount; i++)
            {
                var ownerId = i / wallCountPerPlayer;
                wallTokenSlots[i] = new WallToken(i, ownerId);
                players[ownerId].AddWall(wallTokenSlots[i]);
            }
        }
    }
}