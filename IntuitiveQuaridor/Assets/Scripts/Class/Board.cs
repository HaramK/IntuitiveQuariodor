using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public class Board
    {
        public Dictionary<Vector2Int, PlayerToken> playerCoord = new Dictionary<Vector2Int, PlayerToken>();
        public Dictionary<Vector2Int, WallToken> wallCoord = new Dictionary<Vector2Int, WallToken>();
        
        // 시작 시 벽이 배치되는 슬롯
        public WallToken[] wallTokenSlots;
        
        public Board(Player[] players, WallToken[] wallTokens) 
        {
            for (int i = 0; i < players.Length; i++)
            {
                playerCoord.Add(players[i].token.position, players[i].token);
            }
            
            wallTokenSlots = new WallToken[Constant.WallTokenCount];
            for (int i = 0; i < wallTokens.Length; i++)
            {
                wallTokenSlots[i] = wallTokens[i];
            }
        }

        public void Move(PlayerToken playerToken, Vector2Int targetPosition)
        {
            playerCoord.Remove(playerToken.position);
            playerToken.position = targetPosition;
            playerCoord.Add(playerToken.position, playerToken);
        }
        
        public void PlaceWall(WallToken wallToken, Vector2Int position)
        {
            wallCoord.Add(position, wallToken);
            wallToken.position = position;
            wallToken.isPlaced = true;
        }
    }
}