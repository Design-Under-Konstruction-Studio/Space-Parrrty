using System.Collections;
using System.Collections.Generic;
using Board.Elements.Tile;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Board.Behaviour
{
    public class BoardGenerate : MonoBehaviour
    {
        #region Properties
        public int CreatedLines
        {
            get => createdLines;
            private set => createdLines = value;
        }
        #endregion

        // Script References
        BoardController boardController;

        [Header("Prefabs Imports")]
        [SerializeField]
        public List<GameObject> _tilesPrefabs;
        public GameObject _obstaclePrefab;

        [Header("Board Settings")]
        public Vector2Int startTileSize;

        [Header("Internal State")]
        public int createdLines;

        private void Awake()
        {
            boardController = GetComponent<BoardController>();
        }

        private void Start()
        {
            CreateBoard();
            CreatedLines = 0;
        }

        public void CreateBoard()
        {
            boardController.boardElements = new GameObject[boardController.boardSize.x, 1000];
            boardController.bottomOfBoardOffSet = boardController.boardSize.y + 2;

            boardController.tilesObjects = new GameObject();
            boardController.tilesObjects.name = "tilesObjects";
            boardController.tilesObjects.transform.SetParent(boardController.boardTransform.transform);

            GameObject[] lastLine = new GameObject[boardController.boardSize.x];
            GameObject lastTile = null;

            for (int y = boardController.boardSize.y - startTileSize.y; y < boardController.bottomOfBoardOffSet; y++)
            {
                for (int x = 0; x < boardController.boardSize.x; x++)
                {
                    List<GameObject> possibleTile = new List<GameObject>();
                    possibleTile.AddRange(_tilesPrefabs);

                    possibleTile.Remove(lastLine[x]);
                    possibleTile.Remove(lastTile);

                    int randomTile = Random.Range(0, possibleTile.Count);
                    GameObject tile = Instantiate(possibleTile[randomTile], boardController.tilesObjects.transform);
                    tile.GetComponent<Tile>().Position = new Vector2Int(x, y);
                    boardController.boardElements[x, y] = tile;

                    lastLine[x] = possibleTile[randomTile];
                    lastTile = possibleTile[randomTile];
                }
            }

            boardController.boardTransform.transform.localPosition = new Vector3(-boardController.boardSize.x / 2, boardController.boardSize.y / 2, 0);

            boardController.bottomOfBoard = boardController.boardSize.y;
        }

        public void createLine(int positionY)
        {
            for (int x = 0; x < boardController.boardSize.x; x++)
            {
                List<GameObject> possibleTile = new List<GameObject>();

                possibleTile.AddRange(_tilesPrefabs);

                int randomTile = Random.Range(0, possibleTile.Count);
                GameObject tile = Instantiate(possibleTile[randomTile], new Vector3(100, 100, 100), Quaternion.identity, boardController.tilesObjects.transform);
                tile.GetComponent<Tile>().Position = new Vector2Int(x, positionY);
                boardController.boardElements[x, positionY] = tile;
                tile.GetComponent<Tile>().fall();
            }
        }

        // public void createObstacle()
        // {
        //     GameObject obstacleGO = Instantiate(_obstaclePrefab, new Vector3(100, 100, 100), Quaternion.identity, boardController.tilesObjects.transform);
        //     Obstacle obstacle = obstacleGO.GetComponent<Obstacle>();

        //     Vector2 obstacleScale = obstacleGO.transform.localScale;
        //     obstacleScale.x *= obstacle.ObstacleSize;
        //     obstacleGO.transform.localScale = obstacleScale;

        //     obstacle.Position = new Vector2Int(0, boardController.topOfBoard);
        //     obstacle.fall();
        // }

        public void createDownLine()
        {
            List<GameObject> lastLine = boardController.getTileLineGameObject(boardController.bottomOfBoardOffSet - 1);
            GameObject lastTile = null;

            for (int x = 0; x < boardController.boardSize.x; x++)
            {
                List<GameObject> possibleTile = new List<GameObject>();

                possibleTile.AddRange(_tilesPrefabs);
                possibleTile.Remove(lastLine[x]);
                possibleTile.Remove(lastTile);

                int randomTile = Random.Range(0, possibleTile.Count);
                GameObject tile = Instantiate(possibleTile[randomTile], new Vector3(100, 100, 100), Quaternion.identity, boardController.tilesObjects.transform);
                tile.GetComponent<Tile>().Position = new Vector2Int(x, boardController.bottomOfBoardOffSet);
                boardController.boardElements[x, boardController.bottomOfBoardOffSet] = tile;

                lastLine[x] = possibleTile[randomTile];
                lastTile = possibleTile[randomTile];
            }

            boardController.bottomOfBoardOffSet++;
            boardController.bottomOfBoard++;
            boardController.topOfBoard++;
            CreatedLines++;
        }

    }
}
