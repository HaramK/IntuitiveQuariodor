using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace Quaridor
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private GameObject outLine;

        [SerializeField] private TextMeshProUGUI wallLeftText;

        public Player player;
        
        public void Init(Player player)
        {
            this.player = player;
            SetWallLeftCount(player.WallLeftCount);
            outLine.SetActive(player.id == 0);
        }

        public void UpdatePlayer(bool isMyTurn)
        {
            SetWallLeftCount(player.WallLeftCount);
            outLine.SetActive(isMyTurn);
        }

        private void SetWallLeftCount(int wallLeftCount)
        {
            wallLeftText.text = $"Walls left : {wallLeftCount}";
        }
    }
}