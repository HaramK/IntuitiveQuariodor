using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public class PlayerSlotLayout : MonoBehaviour
    {
        [SerializeField] GameObject playerSlotPrefab;
        [SerializeField] private float scale = 0.75f;
        [SerializeField] private float space = 0.5f;
        
        void OnValidate()
        {
            var boardSize = Constant.BoardSize;
            var center = (boardSize - 1) / 2;

            if (transform.childCount == 0)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        var playerSlot = Instantiate(playerSlotPrefab, transform);
                        var x = (i - center) * (space);
                        var y = (j - center) * (space);
                        playerSlot.transform.localPosition = new Vector3(x, y, 0);
                        playerSlot.transform.localScale = new Vector3(scale, scale, 1);
                    }
                }
            }
        }
    }
}