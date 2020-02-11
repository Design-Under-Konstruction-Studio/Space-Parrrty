using System.Collections.Generic;
using TileController;
using UnityEngine;

namespace BoardController {
  public class Board : MonoBehaviour {
    [Header ("Prefabs Imports")]
    [SerializeField]
    private List<GameObject> _tilesPrefabs;

    [Header ("Board Settings")]
    public Vector2Int boardSize;

    public GameObject[, ] boardTiles;

    public bool isStart;

    // Start is called before the first frame update
    void Start () {
      CreateBoard ();
    }

    // Update is called once per frame
    void Update () {

    }

    void CreateBoard () {
      isStart = false;

      boardTiles = new GameObject[boardSize.x, boardSize.y];

      GameObject tilesObs = new GameObject ();
      tilesObs.name = "tilesObs";
      tilesObs.transform.SetParent (this.transform);

      GameObject[] lastLine = new GameObject[boardSize.x];
      GameObject lastTile = null;

      for (int y = 0; y < boardSize.y; y++) {
        for (int x = 0; x < boardSize.x; x++) {
          List<GameObject> possibleTile = new List<GameObject> ();
          possibleTile.AddRange (_tilesPrefabs);

          possibleTile.Remove (lastLine[x]);
          possibleTile.Remove (lastTile);

          int randomTile = Random.Range (0, possibleTile.Count);
          GameObject tile = Instantiate (possibleTile[randomTile], tilesObs.transform);
          tile.GetComponent<Tile> ().setPosition (new Vector2Int (x, y));
          boardTiles[x, y] = tile;

          lastLine[x] = possibleTile[randomTile];
          lastTile = possibleTile[randomTile];
        }
      }

      isStart = true;
    }

    public GameObject getTileGameObject (Vector2Int position) {
      if (position.x >= boardSize.x || position.y >= boardSize.y) {
        return null;
      }
      return boardTiles[position.x, position.y].gameObject;
    }

    public Tile getTileComponent (Vector2Int position) {
      if (position.x >= boardSize.x || position.y >= boardSize.y || position.x < 0 || position.y < 0) {
        return null;
      }
      GameObject tile = boardTiles[position.x, position.y];

      if (tile == null) {
        return null;
      }

      return tile.GetComponent<Tile> ();
    }

    public Tile getUpTileComponent (Vector2Int position) {
      return null;
    }

    public Tile getDownTileComponent (Vector2Int position) {
      return null;
    }

  }
}