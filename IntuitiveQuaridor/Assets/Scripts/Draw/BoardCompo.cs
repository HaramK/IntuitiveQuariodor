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
        [SerializeField] private WallTokenCompo potentialWall;
        [SerializeField] private PlayerTokenCompo potentialPlayer;

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
                playerCompo.Set(player.id);
                players.Add(playerCompo);
            }
        }

        public void UpdatePotentialCommand(Command command)
        {
            switch (command.type)
            {
                case CommandType.Move:
                    potentialPlayer.gameObject.SetActive(true);
                    potentialWall.gameObject.SetActive(false);
                    potentialPlayer.transform.position = Utils.SlotToWorld(command.targetPosition);
                    potentialPlayer.Set(command.playerID);
                    break;
                case CommandType.PlaceWall:
                    potentialPlayer.gameObject.SetActive(false);
                    potentialWall.gameObject.SetActive(true);
                    potentialWall.transform.position = Utils.WallToWorld(command.targetPosition);
                    potentialWall.SetRotation(command.wallRotationType);
                    break;
                default:
                    potentialPlayer.gameObject.SetActive(false);
                    potentialWall.gameObject.SetActive(false);
                    break;
            }
        }
        
        public void UpdateProcessedCommand(Command command)
        {
            switch (command.type)
            {
                case CommandType.Move:
                    var player = players[command.playerID];
                    player.transform.position = Utils.SlotToWorld(command.targetPosition);
                    break;
                case CommandType.PlaceWall:
                    var wall = Instantiate(wallPrefab, wallRoot).GetComponent<WallTokenCompo>();
                    wall.transform.position = Utils.WallToWorld(command.targetPosition);
                    wall.SetRotation(command.wallRotationType);
                    walls.Add(wall);
                    break;
                default:
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
