using System.Collections.Generic;
using UnityEngine;

namespace BoardController
{
  public class Board : MonoBehaviour
  {
    [Header("Prefabs Imports")]
    [SerializeField]
    private List<GameObject> _tilesPrefabs;

    [Header("Board Settings")]
    [SerializeField]
    private Vector2Int _boardSize;

    private GameObject[,] _boardTiles;

    // Start is called before the first frame update
    void Start()
    {
      CreateBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateBoard()
    {
      _boardTiles = new GameObject[_boardSize.x, _boardSize.y];

      GameObject tilesObs = new GameObject();
      tilesObs.name = "tilesObs";
      tilesObs.transform.SetParent(this.transform);

      GameObject[] lastLine = new GameObject[_boardSize.x];
      GameObject lastTile = null;

      for (int y = 0; y < _boardSize.y; y++)
      {
        for (int x = 0; x < _boardSize.x; x++)
        {
          List<GameObject> possibleTile = new List<GameObject>();
          possibleTile.AddRange(_tilesPrefabs);

          possibleTile.Remove(lastLine[x]);
          possibleTile.Remove(lastTile);

          int randomTile = Random.Range(0, possibleTile.Count);
          GameObject tile = Instantiate(possibleTile[randomTile], tilesObs.transform);
          tile.name = $"Tile {x}-{y}";
          tile.transform.position = new Vector2(x, -y);
          _boardTiles[x, y] = tile;

          lastLine[x] = possibleTile[randomTile];
          lastTile = possibleTile[randomTile];
        }
      }
    }
  }
}