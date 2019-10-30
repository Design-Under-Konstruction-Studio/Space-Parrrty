using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
  [Header ("Prefabs Imports")]
  [SerializeField]
  private List<GameObject> _tilesPrefabs;

  [Header ("Board Settings")]
  [SerializeField]
  private Vector2Int _boardSize;

  private GameObject[, ] _boardTiles;

  // Start is called before the first frame update
  void Start () {
    CreateBoard ();
  }

  // Update is called once per frame
  void Update () {

  }

  void CreateBoard () {
    _boardTiles = new GameObject[_boardSize.x, _boardSize.y];

    GameObject tilesObs = new GameObject ();
    tilesObs.name = "tilesObs";
    tilesObs.transform.SetParent (this.transform);
    for (int y = 0; y < _boardSize.y; y++) {
      for (int x = 0; x < _boardSize.x; x++) {
        int randomNumber = Random.Range (0, 4);
        GameObject tile = Instantiate (_tilesPrefabs[randomNumber], tilesObs.transform);
        tile.name = $"Tile {x}-{y}";
        tile.transform.position = new Vector2 (x, -y);
        _boardTiles[x, y] = tile;
      }
    }
  }
}