using System.Collections;
using System.Collections.Generic;
using BoardController;
using UnityEngine;

namespace TileController {
  public class Tile : MonoBehaviour {

    [SerializeField]
    private Vector2Int _position;

    public bool canMove = true;

    public TileTypes typeTile;

    Board board;

    // Start is called before the first frame update
    void Start() {
      board = transform.parent.parent.gameObject.GetComponent<Board>();
    }

    // Update is called once per frame
    void Update() {
      setTileName();

      transform.localPosition = new Vector3(_position.x, -(_position.y), transform.localPosition.y);
    }

    #region Set/GetPosition
    public void setPosition(Vector2Int position) {
      _position = position;

      if (board != null) {
        board.boardTiles[position.x, position.y] = gameObject;
      }

    }

    public Vector2Int getPosition() {
      return _position;
    }
    #endregion

    #region SetTileName
    void setTileName() {
      gameObject.name = $"Tile {_position.x}-{Mathf.Abs(_position.y)}";
    }
    #endregion

    #region Find Match
    public void findMatch() {
      StartCoroutine(findMatchRoutine());
    }

    IEnumerator findMatchRoutine() {
      yield return new WaitForSeconds(0.1f);
      List<Tile> tilesMatch = new List<Tile>();

      tilesMatch.AddRange(findTileDirection(new Vector2Int[2] { Vector2Int.left, Vector2Int.right }));
      tilesMatch.AddRange(findTileDirection(new Vector2Int[2] { Vector2Int.up, Vector2Int.down }));

      foreach (var item in tilesMatch) {
        StartCoroutine(item.destroyTile());
      }
    }

    List<Tile> findTileDirection(Vector2Int[] directions) {
      List<Tile> tilesMatch = new List<Tile>();
      tilesMatch.Add(this);

      // Um Pequeno LOG
      // Debug.Log("Tile: " + nextTile.name + " type: " + nextTile.typeTile + " - Match: " + 1 + " " + directions[i].ToString());

      // Me superei xD
      for (int i = 0; i < directions.Length; i++) {
        List<Tile> tilesDirectionMatch = new List<Tile>();
        Tile nextTile = this;
        nextTile = board.getTileComponent(nextTile._position + directions[i]);
        if (nextTile == null) {
          continue;
        }
        if (nextTile.typeTile == this.typeTile) {
          tilesDirectionMatch.Add(nextTile);
        }
        for (int e = 0; e < tilesDirectionMatch.Count; e++) {
          nextTile = board.getTileComponent(nextTile._position + directions[i]);
          if (nextTile == null) {
            break;
          }
          if (nextTile.typeTile == this.typeTile) {
            tilesDirectionMatch.Add(nextTile);
          }
        }
        tilesMatch.AddRange(tilesDirectionMatch);
      }
      if (tilesMatch.Count > 2) {
        return tilesMatch;
      } else {
        return new List<Tile>();
      }
    }
    #endregion

    #region FallTile
    void fallTile() {
      // #ToDo
    }
    #endregion

    #region DestroyObject
    IEnumerator destroyTile() {
      canMove = false;
      Color tileColor = GetComponent<SpriteRenderer>().color;
      GetComponent<SpriteRenderer>().color = new Color(tileColor.r, tileColor.g, tileColor.b, 0.5f);
      yield return new WaitForSeconds(5);
      board.boardTiles[_position.x, _position.y] = null;
      Destroy(gameObject);
    }
    #endregion
  }
}