using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Quaridor
{
    public class QuaridorInterface : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private BoardCompo boardCompo;

        private Quaridor quaridor;
        private Command currentCommand;
        private RotationType wallSetRotation = RotationType.Horizontal;

        private GameState _state = GameState.Ready;
        public enum GameState
        {
            Ready,
            Play,
            End,
        }

        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
        }

        private void Update()
        {
            UpdateCommand();
            ProcessInput();
            if (quaridor.isEnd)
                EndGame();
        }

        private void StartGame()
        {
            quaridor = new Quaridor(2);
            _state = GameState.Play;
            startButton.gameObject.SetActive(false);
        }

        private void EndGame()
        {
            _state = GameState.End;
            startButton.gameObject.SetActive(true);
        }

        private void UpdateCommand()
        {
            currentCommand = new Command();
            
            var curPlayerId = quaridor.currentPlayerId;
            var currentPlayer = quaridor.players[curPlayerId];
            currentCommand.playerID = curPlayerId;
            var wallCount = currentPlayer.walls.Count(w => !w.isPlaced);
            
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit2, 100f, LayerMask.GetMask("PlayerSlot")))
            {
                var slotCoord = Utils.ToSlotCoord(mouseWorldPosition);
                currentCommand.targetPosition = slotCoord;
                currentCommand.targetWallId = -1;
                currentCommand.type = CommandType.Move;
            }
            else if (wallCount > 0 && Physics.Raycast(ray, out var hit, 100f, LayerMask.GetMask("Board")))
            {
                var wallCoord = Utils.ToWallCoord(mouseWorldPosition);
                currentCommand.targetPosition = wallCoord;
                currentCommand.targetWallId = currentPlayer.walls.First(w => !w.isPlaced).id;
                currentCommand.wallRotationType = wallSetRotation;
                currentCommand.type = CommandType.PlaceWall;
            }
            else
            {
                currentCommand.type = CommandType.None;
            }
        }

        private void ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                wallSetRotation = wallSetRotation == RotationType.Horizontal ? RotationType.Vertical : RotationType.Horizontal;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (_state == GameState.Play)
                {
                    if (quaridor.TryCommand(currentCommand))
                    {
                        UpdateDrawCompo(currentCommand);
                    };
                }
            }
        }

        private void UpdateDrawCompo(Command command)
        {
            
        }
    }
}
