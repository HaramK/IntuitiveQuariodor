using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public class PlayerSlotLayout : MonoBehaviour
    {
        [SerializeField] GameObject playerSlotPrefab;
        
        void OnValidate()
        {
            var boardSize = Constant.BoardSize;
            var centerIdx = boardSize / 2;
            
            var scale = Constant.playerSlotScale;
            var space = Constant.plyaerSlotSpace;
            var interval = scale + space;

            if (transform.childCount == 0)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        var playerSlot = Instantiate(playerSlotPrefab, transform);
                        var x = (i - centerIdx) * (interval);
                        var y = (j - centerIdx) * (interval);
                        playerSlot.transform.localPosition = new Vector3(x, y, 0);
                        playerSlot.transform.localScale = new Vector3(scale, scale, 1);
                    }
                }
            }
        }
    }
}