using System.Collections;
using System.Collections.Generic;
using BoardController;
using TileController;
using UnityEngine;

public class Picker : MonoBehaviour
{
  [SerializeField]
  private Vector2Int _position;

  Board board;

  // Start is called before the first frame update
  void Start()
  {
    board = GetComponentInParent<Board>();
  }

  // Update is called once per frame
  void Update()
  {
    transform.localPosition = new Vector3(_position.x, -(_position.y), transform.localPosition.z);

    #region Input temporario
    if (Input.GetKeyDown(KeyCode.UpArrow))
      moveUp();
    if (Input.GetKeyDown(KeyCode.LeftArrow))
      moveLeft();
    if (Input.GetKeyDown(KeyCode.DownArrow))
      moveDown();
    if (Input.GetKeyDown(KeyCode.RightArrow))
      moveRight();

    if (Input.GetKeyDown(KeyCode.Space))
      changeTilePosition();

    #endregion
  }

  #region Movimentação
  public void moveLeft()
  {
    _position = new Vector2Int(--_position.x, _position.y);
  }

  public void moveRight()
  {
    _position = new Vector2Int(++_position.x, _position.y);
  }

  public void moveUp()
  {
    _position = new Vector2Int(_position.x, --_position.y);
  }

  public void moveDown()
  {
    _position = new Vector2Int(_position.x, ++_position.y);
  }
  #endregion

  #region ChangeTilePosition
  void changeTilePosition()
  {
    board.boardTiles[_position.x, _position.y].GetComponent<Tile>().setPosition(new Vector2Int(_position.x + 1, Mathf.Abs(_position.y)));
    // board.boardTiles[]

    board.boardTiles[_position.x + 1, _position.y].GetComponent<Tile>().setPosition(new Vector2Int(_position.x, Mathf.Abs(_position.y)));
  }
  #endregion
}
