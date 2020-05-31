using System.Collections.Generic;
using TileController;
using UnityEngine;

namespace BoardController {
  public class Board : MonoBehaviour {
    [Header("Prefabs Imports")]
    [SerializeField]
    private List<GameObject> _tilesPrefabs;

    [Header("Board Settings")]
    public Vector2Int boardSize;
    public Vector2Int startTileSize;
    private GameObject tilesObjects;

    public int topOfBoard;
    public int bottomOfBoard;

    public GameObject[, ] boardTiles;

    private bool isStart;

    public GameObject boardTransform;
    public float moveUpSpeed;

    // Start is called before the first frame update
    void Start() {
      CreateBoard();
    }

    // Update is called once per frame
    void Update() {
      // moveBoardUp();

      if(Input.GetKeyDown(KeyCode.Z)) {
        createDownLine();
      }
    }

    void CreateBoard() {
      isStart = false;

      boardTiles = new GameObject[boardSize.x, 1000];

      tilesObjects = new GameObject();
      tilesObjects.name = "tilesObjects";
      tilesObjects.transform.SetParent(boardTransform.transform);

      GameObject[] lastLine = new GameObject[boardSize.x];
      GameObject lastTile = null;

      for (int y = boardSize.y - startTileSize.y; y < boardSize.y; y++) {
        for (int x = 0; x < boardSize.x; x++) {
          List<GameObject> possibleTile = new List<GameObject>();
          possibleTile.AddRange(_tilesPrefabs);

          possibleTile.Remove(lastLine[x]);
          possibleTile.Remove(lastTile);

          int randomTile = Random.Range(0, possibleTile.Count);
          GameObject tile = Instantiate(possibleTile[randomTile], tilesObjects.transform);
          tile.GetComponent<Tile>().setPosition(new Vector2Int(x, y));
          boardTiles[x, y] = tile;

          lastLine[x] = possibleTile[randomTile];
          lastTile = possibleTile[randomTile];
        }
      }
      
      boardTransform.transform.localPosition = new Vector3(-boardSize.x/2,boardSize.y/2,0);
      isStart = true;

      bottomOfBoard = boardSize.y - 1;
    }

    private void moveBoardUp() {
      boardTransform.transform.Translate(0, 1*moveUpSpeed*Time.deltaTime, 0);
    }
    
    public void createDownLine() {
      List<GameObject> lastLine = getTileLineGameObject(bottomOfBoard);
      GameObject lastTile = null;

      bottomOfBoard++;
      
      for (int x = 0; x < boardSize.x; x++) {
        List<GameObject> possibleTile = new List<GameObject>();

        possibleTile.AddRange(_tilesPrefabs);
        possibleTile.Remove(lastLine[x]);
        possibleTile.Remove(lastTile);

        int randomTile = Random.Range(0, possibleTile.Count);
        GameObject tile = Instantiate(possibleTile[randomTile], new Vector3(100, 100, 100), Quaternion.identity, tilesObjects.transform);
        tile.GetComponent<Tile>().setPosition(new Vector2Int(x, bottomOfBoard));
        boardTiles[x, bottomOfBoard] = tile;

        lastLine[x] = possibleTile[randomTile];
        lastTile = possibleTile[randomTile];
      }
    }

    public void createUpLine() {

    }

    #region Gettter Tiles

    public GameObject getTileGameObject(Vector2Int position) {
      if (position.x >= boardSize.x || position.y >= bottomOfBoard) {
        return null;
      }
      return boardTiles[position.x, position.y].gameObject;
    }

    public Tile getTileComponent(Vector2Int position) {
      if (position.x >= boardSize.x || position.y >= bottomOfBoard || position.x < 0 || position.y < 0) {
        return null;
      }
      GameObject tile = boardTiles[position.x, position.y];

      if (tile == null) {
        return null;
      }

      return tile.GetComponent<Tile>();
    }

    public Tile getUpTileComponent(Vector2Int position) {
      return null;
    }

    public Tile getDownTileComponent(Vector2Int position) {
      return null;
    }

    public List<GameObject> getTileLineGameObject(int positionY) {
      List<GameObject> lineTile = new List<GameObject>();
      for (int i = 0; i < boardSize.x; i++) {
        lineTile.Add(boardTiles[i, positionY]);
      }
      return lineTile;
    }
    #endregion
    
  }
}