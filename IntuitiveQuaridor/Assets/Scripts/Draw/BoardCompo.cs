using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public class BoardCompo : MonoBehaviour
    {
        [SerializeField] private Transform wallRoot;
        [SerializeField] private GameObject wallPrefab;
        
        [SerializeField] private Transform playerRoot;
        [SerializeField] private GameObject playerPrefab;

        [SerializeField] private GameObject potentialWall;
        [SerializeField] private GameObject potentialPlayer;

        private List<GameObject> walls = new List<GameObject>();
        private List<GameObject> players = new List<GameObject>();

        private Quaridor _quaridor;

        public void Set(Quaridor quaridor)
        {
            ClearBoard();
            
            _quaridor = quaridor;
            for (int i = 0; i < _quaridor.players.Length; i++)
            {
                var player = _quaridor.players[i];
                var playerCompo = Instantiate(playerPrefab, playerRoot).GetComponent<PlayerTokenCompo>();
                playerCompo.transform.position = Utils.SlotToWorld(player.token.position);
                //playerCompo.Set(player);
            }
        }
        
        private void ClearBoard()
        {
            foreach (var wall in walls)
            {
                Destroy(wall);
            }
            walls.Clear();
            
            foreach (var player in players)
            {
                Destroy(player);
            }
            players.Clear();
        }
    }
}
