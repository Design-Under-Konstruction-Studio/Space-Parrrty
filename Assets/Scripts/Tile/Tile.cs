using System.Collections;
using System.Collections.Generic;
using Board;
using UnityEngine;

namespace TileController
{
  public class Tile : MonoBehaviour
  {

    public Vector2Int _position;

    public bool canMove = true;

    public bool inMatch = false;

    public TileTypes typeTile;

    public BoardController board;
    public BoardGenerate boardGenerate;

    private void Awake() {
      board = transform.parent.parent.parent.gameObject.GetComponent<BoardController>();
      boardGenerate = transform.parent.parent.parent.gameObject.GetComponent<BoardGenerate>();
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      setTileName();

      transform.localPosition = new Vector3(_position.x, -(_position.y), 0);
    }

    #region Set/GetPosition
    public virtual void setPosition(Vector2Int position)
    {
      _position = position;

      if (board != null)
      {
        board.boardTiles[position.x, position.y] = gameObject;
      }
    }

    public Vector2Int getPosition()
    {
      return _position;
    }
    #endregion

    #region SetTileName
    public virtual void setTileName()
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
      fallTile();
      List<Tile> tilesMatch = new List<Tile>();

      tilesMatch.AddRange(findTileDirection(new Vector2Int[2] { Vector2Int.left, Vector2Int.right }));
      tilesMatch.AddRange(findTileDirection(new Vector2Int[2] { Vector2Int.up, Vector2Int.down }));

      foreach (var item in tilesMatch)
      {
        StartCoroutine(item.destroyTile());
      }
    }

    List<Tile> findTileDirection(Vector2Int[] directions)
    {
      List<Tile> tilesMatch = new List<Tile>();
      tilesMatch.Add(this);
      
      for (int i = 0; i < directions.Length; i++)
      {
        List<Tile> tilesDirectionMatch = new List<Tile>();
        Tile nextTile = this;
        for (int e = 0; e < tilesMatch.Count; e++)
        {
          nextTile = board.getTileComponent(nextTile._position + directions[i]);
          if (nextTile == null || nextTile.typeTile != this.typeTile || nextTile.inMatch || nextTile.typeTile == TileTypes.Obstacle)
          {
            if (nextTile != null && nextTile.typeTile == TileTypes.Obstacle) {
              if (tilesMatch.Count > 2) StartCoroutine(nextTile.destroyTile());
            }
            break;
          }
          tilesMatch.Add(nextTile);
        }
      }
      if (tilesMatch.Count > 2)
      {
        if(directions[0].Equals(Vector2Int.left)){
          Tile upTile = board.getTileComponent(_position + Vector2Int.down);
          if(upTile != null && upTile.typeTile == TileTypes.Obstacle) {
            StartCoroutine(upTile.destroyTile());
          }
        }
        return tilesMatch;
      }
      else
      {
        return new List<Tile>();
      }
    }
    #endregion

    #region FallTile
    public virtual void fallTile()
    {
      Vector2Int bottomPosition = this._position + Vector2Int.up;
      if (bottomPosition.y < board.bottomOfBoard)
      {
        Tile bottomTile = board.getTileComponent(this._position + Vector2Int.up);
        Tile upTile = board.getTileComponent(this._position + Vector2Int.down);
        if (bottomTile == null)
        {
          board.boardTiles[_position.x, _position.y] = null;
          setPosition(bottomPosition);
          
          if (upTile != null)
          {
            upTile.fallTile();
          }
          fallTile();
        }
      }
    }
    #endregion

    #region DestroyObject
    IEnumerator destroyTile()
    {
      canMove = false;
      inMatch = true;
      Color tileColor = GetComponent<SpriteRenderer>().color;
      GetComponent<SpriteRenderer>().color = new Color(tileColor.r, tileColor.g, tileColor.b, 0.5f);
      yield return new WaitForSeconds(2);
      board.boardTiles[_position.x, _position.y] = null;
      Tile upTile = board.getTileComponent(this._position + Vector2Int.down);

      if (upTile != null)
      {
        upTile.fallTile();
      }
      if(typeTile == TileTypes.Obstacle) {
        boardGenerate.createLine(_position.y);
      }
      Destroy(gameObject);
    }
    #endregion
  }
}