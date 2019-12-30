using System.Collections;
using System.Collections.Generic;
using BoardController;
using UnityEngine;

namespace TileController
{
  public class Tile : MonoBehaviour
  {
    [SerializeField]
    private Vector2Int _position;

    public bool canMove = true;

    public TileTypes typeTile;

    Board board;

    // Start is called before the first frame update
    void Start()
    {
      board = transform.parent.parent.gameObject.GetComponent<Board>();
    }

    // Update is called once per frame
    void Update()
    {
      setTileName();

      transform.localPosition = new Vector3(_position.x, -(_position.y), transform.localPosition.y);

      if (board != null)
      {
        board.boardTiles[_position.x, _position.y] = gameObject;
      }
    }

    #region Set/GetPosition
    public void setPosition(Vector2Int position)
    {
      _position = position;
    }

    public Vector2Int getPosition()
    {
      return _position;
    }
    #endregion

    #region SetTileName
    void setTileName()
    {
      gameObject.name = $"Tile {_position.x}-{Mathf.Abs(_position.y)}";
    }
    #endregion

    #region Find Match
    public void findMatch()
    {
      StartCoroutine(findMatchRoutine());
    }

    IEnumerator findMatchRoutine()
    {
      yield return new WaitForSeconds(0.1f);
      List<Tile> tilesMatch = new List<Tile>();

      tilesMatch.AddRange(findTileDirection(new Vector2Int[2] { Vector2Int.left, Vector2Int.right }));
      tilesMatch.AddRange(findTileDirection(new Vector2Int[2] { Vector2Int.up, Vector2Int.down }));
    }

    List<Tile> findTileDirection(Vector2Int[] directions)
    {
      List<Tile> tilesMatch = new List<Tile>();
      tilesMatch.Add(this);
      for (int i = 0; i < directions.Length; i++)
      {
        Tile nextTile = this;
        for (int e = 0; e < tilesMatch.Count; e++)
        {
          nextTile = board.getTileComponent(nextTile._position + directions[i]);
          if (nextTile == null)
          {
            break;
          }
          if (nextTile.typeTile == this.typeTile)
          {
            tilesMatch.Add(nextTile);
          }
        }
      }
      if (tilesMatch.Count >= 3)
      {
        Debug.Log(tilesMatch.Count + " - " + tilesMatch[0].typeTile);
        return tilesMatch;
      }
      else
      {
        return new List<Tile>();
      }
    }
    #endregion
  }
}