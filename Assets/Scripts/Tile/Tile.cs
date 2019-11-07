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

    private Board board;

    // Start is called before the first frame update
    void Start()
    {
      board = transform.parent.parent.GetComponent<Board>();
    }

    // Update is called once per frame
    void Update()
    {
      setTileName();

      transform.localPosition = new Vector3(_position.x, -(_position.y), transform.localPosition.y);
    }

    #region SetGetPosition
    public void setPosition(Vector2Int position)
    {
      board.boardTiles[position.x, position.y] = gameObject;
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

  }
}