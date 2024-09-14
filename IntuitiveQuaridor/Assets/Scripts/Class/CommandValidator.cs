using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Quaridor
{
    public class CommandValidator
    {
        private readonly HashSet<Vector2Int> _availablePlayerPos = new();
        
        public bool CheckValid(Command command, Board board, Player[] players, int currentPlayerID)
        {
            var currentPlayer = players[currentPlayerID];
            return command.type switch
            {
                CommandType.Move => CheckMoveValid(command.targetPosition),
                CommandType.PlaceWall => CheckPlaceWallValid(command.targetPosition, command.wallRotationType, board, players)
            };
        }
        
        private bool CheckMoveValid(Vector2Int targetPos)
        {
            return _availablePlayerPos.Contains(targetPos);
        }

        private bool CheckPlaceWallValid(Vector2Int targetPosition, RotationType rotationType, Board board, Player[] players)
        {
            if (board.wallCoord.ContainsKey(targetPosition))
                return false;

            if (!board.IsInnerWallPos(targetPosition))
                return false;
            
            var pooledObject = ListPool<Vector2Int>.Get(out var neighborWallPositions);
            Utils.GetNeighborWallPositions(targetPosition, rotationType, neighborWallPositions);
            foreach (var neighborWallPos in neighborWallPositions)
            {
                if (board.wallCoord.TryGetValue(neighborWallPos, out var wall) && wall.rotationType == rotationType)
                    return false;
            }
            
            // BFS로 각 플레이어가 목적지에 도달 가능한지 확인
            var testWall = new WallToken(-1, -1);
            board.PlaceWall(testWall, targetPosition, rotationType);
            foreach (var player in players)
            {
                using var _ = HashSetPool<Vector2Int>.Get(out var reachablePos);
                BFS(board, player.token.position, reachablePos);
                var isGoalReachable = false;
                foreach (var winPos in player.token.winPositions)
                {
                    if (reachablePos.Contains(winPos))
                    {
                        isGoalReachable = true;
                        break;
                    }
                }
                if (!isGoalReachable)
                {
                    board.RemoveWall(targetPosition);
                    return false;
                }
            }
            board.RemoveWall(targetPosition);
            return true;
        }

        public void UpdateAvailablePlayerPos(Board board, Vector2Int currentPos)
        {
            _availablePlayerPos.Clear();
            GetMovablePositions(board, currentPos, _availablePlayerPos);
        }

        private void BFS(Board board, Vector2Int currentPos, HashSet<Vector2Int> reachablePos)
        {
            using var _ = ListPool<Vector2Int>.Get(out var queue);
            
            queue.Add(currentPos);
            reachablePos.Add(currentPos);
            
            while (queue.Count > 0)
            {
                var pos = queue[0];
                queue.RemoveAt(0);
                
                using var pooledObject = HashSetPool<Vector2Int>.Get(out var movablePos);
                GetMovablePositions(board, pos, movablePos);
                foreach (var nextPos in movablePos)
                {
                    if (reachablePos.Contains(nextPos))
                        continue;
                    
                    queue.Add(nextPos);
                    reachablePos.Add(nextPos);
                }
            }
        }
        
        public void GetMovablePositions(Board board, Vector2Int currentPos, HashSet<Vector2Int> outList)
        {
            foreach (var dir in Utils.AllDirections)
            {
                var singleMovePos = currentPos + dir;
                if (!board.IsInnerPlayerPos(singleMovePos))
                    continue;

                if (board.IsWallExist(currentPos, singleMovePos))
                    continue;

                if (!board.playerCoord.ContainsKey(singleMovePos))
                {
                    outList.Add(singleMovePos);
                    continue;
                }

                var doubleMovePos = singleMovePos + dir;
                if (board.CanHop(singleMovePos, doubleMovePos))
                {
                    outList.Add(doubleMovePos);
                    continue;
                }

                using var _ = ListPool<Vector2Int>.Get(out var diagonalDirs);
                Utils.GetDiagonals(dir, diagonalDirs);
                foreach (var diagonalDir in diagonalDirs)
                {
                    var diagonalPos = currentPos + diagonalDir;
                    if (board.CanHop(singleMovePos, diagonalPos))
                    {
                        outList.Add(diagonalPos);
                    }
                }
            }
        }
    }
}