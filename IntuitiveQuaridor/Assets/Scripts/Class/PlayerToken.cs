using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public class PlayerToken
    {
        public int ownerId;
        public Vector2Int position;
        public Vector2Int[] winPositions;
        
        public PlayerToken(int ownerId)
        {
            this.ownerId = ownerId;
        }

        public void Init(Vector2Int position)
        {
            this.position = position;
            winPositions = new Vector2Int[Constant.BoardSize];
            switch (position)
            {
                case { y: (0 or Constant.BoardSize-1)}:
                    var isDown = position.y == 0;
                    for (int i = 0; i < Constant.BoardSize; i++)
                    {
                        winPositions[i] = new Vector2Int(i, isDown ? Constant.BoardSize - 1 : 0);
                    }
                    break;
                case { x: (0 or Constant.BoardSize-1)}:
                    var isLeft = position.x == 0;
                    for (int i = 0; i < Constant.BoardSize; i++)
                    {
                        winPositions[i] = new Vector2Int(isLeft ? Constant.BoardSize - 1 : 0, i);
                    }
                    break;
                default:
                    Debug.LogError("Invalid player position");
                    break;
            }
        }
    }
}