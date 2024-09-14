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
        [SerializeField] private GameObject winnerCrown;

        public Player player;
        
        public void Init(Player player)
        {
            this.player = player;
            SetWallLeftCount(player.WallLeftCount);
            outLine.SetActive(player.id == 0);
            winnerCrown.SetActive(false);
        }

        public void UpdatePlayer(int currentPlayerId)
        {
            SetWallLeftCount(player.WallLeftCount);
            outLine.SetActive(player.id == currentPlayerId);
        }
        
        public void SetWinner(int winnerId)
        {
            winnerCrown.SetActive(player.id == winnerId);
        }

        private void SetWallLeftCount(int wallLeftCount)
        {
            wallLeftText.text = $"Walls left : {wallLeftCount}";
        }
    }
}