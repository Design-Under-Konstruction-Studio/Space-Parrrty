using System.Collections;
using System.Collections.Generic;
using TileController;
using UnityEngine;

namespace Board
{
  public class BoardGenerate : MonoBehaviour
  {
    // Script References
    BoardController boardController;

    [Header("Prefabs Imports")]
    [SerializeField]
    public List<GameObject> _tilesPrefabs;

    [Header("Board Settings")]
    public Vector2Int startTileSize;

    private void Awake()
    {
      boardController = GetComponent<BoardController>();
    }

    private void Start()
    {
      CreateBoard();
    }

    public void CreateBoard()
    {
      boardController.boardTiles = new GameObject[boardController.boardSize.x, 1000];
      boardController.bottomOfBoardOffSet = boardController.boardSize.y + 2;

      boardController.tilesObjects = new GameObject();
      boardController.tilesObjects.name = "tilesObjects";
      boardController.tilesObjects.transform.SetParent(boardController.boardTransform.transform);
      boardController.tilesObjects.transform.localPosition = Vector3.zero;

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
          tile.GetComponent<Tile>().setPosition(new Vector2Int(x, y));
          boardController.boardTiles[x, y] = tile;

          lastLine[x] = possibleTile[randomTile];
          lastTile = possibleTile[randomTile];
        }
      }

      boardController.boardTransform.transform.localPosition = new Vector3(-boardController.boardSize.x / 2, boardController.boardSize.y / 2, 0);

      boardController.bottomOfBoard = boardController.boardSize.y;
    }

    public void createUpLine()
    {
      for (int x = 0; x < boardController.boardSize.x; x++)
      {
        List<GameObject> possibleTile = new List<GameObject>();

        possibleTile.AddRange(_tilesPrefabs);

        int randomTile = Random.Range(0, possibleTile.Count);
        GameObject tile = Instantiate(possibleTile[randomTile], new Vector3(100, 100, 100), Quaternion.identity, boardController.tilesObjects.transform);
        tile.GetComponent<Tile>().setPosition(new Vector2Int(x, boardController.bottomOfBoardOffSet));
        boardController.boardTiles[x, boardController.bottomOfBoardOffSet] = tile;
      }
    }

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
        tile.GetComponent<Tile>().setPosition(new Vector2Int(x, boardController.bottomOfBoardOffSet));
        boardController.boardTiles[x, boardController.bottomOfBoardOffSet] = tile;

        lastLine[x] = possibleTile[randomTile];
        lastTile = possibleTile[randomTile];
      }

      boardController.bottomOfBoardOffSet++;
      boardController.bottomOfBoard++;
      boardController.topOfBoard++;
    }

  }
}
