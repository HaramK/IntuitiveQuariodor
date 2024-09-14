using System.Linq;

namespace Quaridor
{
    using System.Collections.Generic;
    using UnityEngine;
    
    public class Quaridor
    {
        public Board board;
        public Player[] players;
        public WallToken[] wallTokens;
        public Queue<Command> commandQueue = new Queue<Command>();
        public CommandValidator commandValidator = new CommandValidator();
        public int currentPlayerId;

        public bool isEnd = false;
        public int winnerId = -1;
        
        public Quaridor(int playerCount)
        {
            Init(playerCount);
        }

        public void Init(int playerCount)
        {
            if (playerCount is not (2 or 4))
            {
                Debug.LogError("Player count must be 2 or 4");
                return;
            }
            
            players = new Player[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                players[i] = new Player(i);
                players[i].token.Init(Constant.PlayerStartPositions[i]);
            }

            var wallCountPerPlayer = Constant.WallTokenCount / playerCount;
            var totalWallCount = wallCountPerPlayer * playerCount;
            wallTokens = new WallToken[totalWallCount];
            for (int i = 0; i < totalWallCount; i++)
            {
                var ownerId = i / wallCountPerPlayer;
                wallTokens[i] = new WallToken(i, ownerId);
                players[ownerId].AddWall(wallTokens[i]);
            }
            
            board = new Board(players, wallTokens);
            
            commandQueue.Clear();
            currentPlayerId = 0;
            commandValidator.UpdateAvailablePlayerPos(board, players[currentPlayerId].token.position);
        }
        
        public bool CheckValid(Command command) => commandValidator.CheckValid(command, board, players);
        
        public bool TryCommand(Command command)
        {
            if (CheckValid(command))
            {
                ProcessCommand(command);
                if (isEnd)
                {
                    return true;
                }
                
                currentPlayerId = (currentPlayerId + 1) % players.Length;
                commandValidator.UpdateAvailablePlayerPos(board, players[currentPlayerId].token.position);
                
                return true;
            }

            return false;
        }

        public void ProcessCommand(Command command)
        { 
            switch(command.type)
            {
                case CommandType.Move:
                    var playerToken = players[command.playerID].token;
                    board.Move(playerToken, command.targetPosition);
                    CheckWin(playerToken);
                    break;
                case CommandType.PlaceWall:
                    var wallToken = wallTokens[command.targetWallId];
                    board.PlaceWall(wallToken, command.targetPosition, command.wallRotationType);
                    break;
                default:
                    Debug.LogError("Invalid command type");
                    break;
            }
            commandQueue.Enqueue(command);
        }
        
        private void CheckWin(PlayerToken playerToken)
        {
            if (playerToken.winPositions.Contains(playerToken.position))
            {
                winnerId = playerToken.ownerId;
                isEnd = true;
            }
        }
    }
}