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

        public void SetStartPosition(Vector2Int position)
        {
            this.position = position;
            winPositions = new Vector2Int[Constant.BoardSize];
            switch (position)
            {
                case var v when v.x == 0:
                    var isDown = position.y == 0;
                    for (int i = 0; i < Constant.BoardSize; i++)
                    {
                        winPositions[i] = new Vector2Int(i, isDown ? Constant.BoardSize - 1 : 0);
                    }
                    break;
                default:
                    var isLeft = position.x == 0;
                    for (int i = 0; i < Constant.BoardSize; i++)
                    {
                        winPositions[i] = new Vector2Int(isLeft ? Constant.BoardSize - 1 : 0, i);
                    }
                    break;
            }
        }
    }
}