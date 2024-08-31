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

        [Space]
        [Header("플레이어의 가능한 행동 표시용 오브젝트")]
        [SerializeField] private GameObject potentialWall;
        [SerializeField] private GameObject potentialPlayer;

        private List<WallTokenCompo> walls = new ();
        private List<PlayerTokenCompo> players = new ();

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
                playerCompo.Set(player);
                players.Add(playerCompo);
            }
        }

        public void UpdatePotentialCommand(Command command)
        {
            switch (command.type)
            {
                case CommandType.Move:
                    potentialPlayer.SetActive(true);
                    potentialWall.SetActive(false);
                    potentialPlayer.transform.position = Utils.SlotToWorld(command.targetPosition);
                    break;
                case CommandType.PlaceWall:
                    potentialPlayer.SetActive(false);
                    potentialWall.SetActive(true);
                    potentialWall.transform.position = Utils.WallToWorld(command.targetPosition);
                    break;
                default:
                    potentialPlayer.SetActive(false);
                    potentialWall.SetActive(false);
                    break;
            
            }
        }
        
        private void ClearBoard()
        {
            foreach (var wall in walls)
            {
                Destroy(wall.gameObject);
            }
            walls.Clear();
            
            foreach (var player in players)
            {
                Destroy(player.gameObject);
            }
            players.Clear();
        }
    }
}
