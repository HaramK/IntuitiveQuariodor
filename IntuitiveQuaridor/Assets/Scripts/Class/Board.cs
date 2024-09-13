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
            if (playerCoord.ContainsKey(targetPosition))
            {
                Debug.Log("이미 플레이어가 있는 위치입니다.");
                return;
            }
            
            playerCoord.Remove(playerToken.position);
            playerToken.position = targetPosition;
            playerCoord.Add(playerToken.position, playerToken);
        }
        
        public void PlaceWall(WallToken wallToken, Vector2Int position, RotationType rotationType)
        {
            if (wallCoord.ContainsKey(position))
            {
                Debug.Log("이미 벽이 있는 위치입니다.");
                return;
            }
            
            wallToken.position = position;
            wallToken.isPlaced = true;
            wallToken.rotationType = rotationType;
            wallCoord.Add(position, wallToken);
        }

        public void RemoveWall(Vector2Int position)
        {
            if (!wallCoord.ContainsKey(position))
            {
                Debug.Log("벽이 없는 위치입니다.");
                return;
            }
            
            wallCoord.Remove(position);
        }
        
        public bool IsInnerPlayerPos(Vector2Int pos) => pos.x >= 0 && pos.x < Constant.BoardSize && pos.y >= 0 && pos.y < Constant.BoardSize;
        
        public bool IsInnerWallPos(Vector2Int pos) => pos.x >= 0 && pos.x < Constant.BoardSize - 1 && pos.y >= 0 && pos.y < Constant.BoardSize - 1;
        
        public bool IsWallExist(Vector2Int posA, Vector2Int posB)
        {
            Debug.Assert(Mathf.Abs(posA.x - posB.x) + Mathf.Abs(posA.y - posB.y) == 1);
            if (posA.x == posB.x)
            {
                var x = posA.x;
                var minY = Mathf.Min(posA.y, posB.y);
                // left Wall
                if (wallCoord.TryGetValue(new Vector2Int(x - 1, minY), out var wall) &&
                    wall.rotationType == RotationType.Horizontal)
                {
                    return true;
                }
                // right Wall
                if (wallCoord.TryGetValue(new Vector2Int(x, minY), out wall) &&
                    wall.rotationType == RotationType.Horizontal)
                {
                    return true;
                }
                return false;
            }
            else if (posA.y == posB.y)
            {
                var minX = Mathf.Min(posA.x, posB.x);
                var y = posA.y;
                // down Wall
                if (wallCoord.TryGetValue(new Vector2Int(minX, y - 1), out var wall) &&
                    wall.rotationType == RotationType.Vertical)
                {
                    return true;
                }
                // up Wall
                if (wallCoord.TryGetValue(new Vector2Int(minX, y), out wall) &&
                    wall.rotationType == RotationType.Vertical)
                {
                    return true;
                }
                return false;
            }
            
            Debug.LogError("Invalid wall position");
            return false;
        }

        public bool CanHop(Vector2Int currentPos, Vector2Int targetPos)
        {
            Debug.Assert(Mathf.Abs(currentPos.x - targetPos.x) + Mathf.Abs(currentPos.y - targetPos.y) == 1);
            bool isTargetPosInner = IsInnerPlayerPos(targetPos);
            bool isWallExistBetween = IsWallExist(currentPos, targetPos);
            bool isTargetPosEmpty = !playerCoord.ContainsKey(targetPos);
            
            return isTargetPosInner && isTargetPosEmpty && !isWallExistBetween;
        }
    }
}