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
        public int currentPlayerIndex;

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
                players[i].token.position = Constant.PlayerStartPositions[i];
            }

            var wallCountPerPlayer = Constant.WallTokenCount / playerCount;
            var totalWallCount = wallCountPerPlayer * playerCount;
            wallTokens = new WallToken[totalWallCount];
            for (int i = 0; i < totalWallCount; i++)
            {
                var ownerId = i / wallCountPerPlayer;
                wallTokens[i] = new WallToken(i, ownerId);
            }
            
            board = new Board(players, wallTokens);
            
            commandQueue.Clear();
            currentPlayerIndex = 0;
        }
        
        public void TryCommand(Command command)
        {
            if (command.IsValid())
            {
                ProcessCommand(command);
                commandQueue.Enqueue(command);
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
            }
        }

        public void ProcessCommand(Command command)
        { 
            switch(command.type)
            {
                case CommandType.Move:
                    var playerToken = players[command.targetId].token;
                    board.Move(playerToken, command.targetPosition);
                    break;
                case CommandType.PlaceWall:
                    var wallToken = wallTokens[command.targetId];
                    board.PlaceWall(wallToken, command.targetPosition);
                    break;
            }
        }
    }
}