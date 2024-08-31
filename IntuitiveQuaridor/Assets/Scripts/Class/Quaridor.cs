namespace Quaridor
{
    using System.Collections.Generic;
    
    public class Quaridor
    {
        public Board board;
        public Player[] players;
        public Queue<Command> commandQueue = new Queue<Command>();
        public int currentPlayerIndex;

        public void Init(int playerCount)
        {
            var wallCountPerPlayer = Constant.WallTokenCount / playerCount;
            
            players = new Player[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                players[i] = new Player(i);
            }
            board = new Board(players, wallCountPerPlayer);
            
            commandQueue.Clear();
            currentPlayerIndex = 0;
        }
        
        public void TryAddCommand(Command command)
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
            
        }
    }
}