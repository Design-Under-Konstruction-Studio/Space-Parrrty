using System.Collections;
using System.Collections.Generic;
using BoardController;
using TileController;
using UnityEngine;

public class Picker : MonoBehaviour {
  [SerializeField]
  private Vector2Int _position;

  Board board;

  private void Awake() {
    board = GetComponentInParent<Board>();
  }

  // Start is called before the first frame update
  void Start() { }

  // Update is called once per frame
  void Update() {
    transform.localPosition = new Vector3(_position.x + 0.5f, -(_position.y), transform.localPosition.z);

    #region Input temporario
    if (Input.GetKeyDown(KeyCode.UpArrow))
      moveUp();
    if (Input.GetKeyDown(KeyCode.LeftArrow))
      moveLeft();
    if (Input.GetKeyDown(KeyCode.DownArrow))
      moveDown();
    if (Input.GetKeyDown(KeyCode.RightArrow))
      moveRight();

    if (Input.GetKeyDown(KeyCode.Space)) {
      changeTilePosition();
    }

    #endregion
  }

  #region Movimentação
  public void moveLeft() {
    _position = new Vector2Int(--_position.x, _position.y);
  }

  public void moveRight() {
    _position = new Vector2Int(++_position.x, _position.y);
  }

  public void moveUp() {
    _position = new Vector2Int(_position.x, --_position.y);
  }

  public void moveDown() {
    _position = new Vector2Int(_position.x, ++_position.y);
  }
  #endregion

  #region ChangeTilePosition
  void changeTilePosition() {
    Vector2Int rightPosition = new Vector2Int(_position.x + 1, _position.y);
    Vector2Int leftPosition = new Vector2Int(_position.x, _position.y);

    Tile tileRight = board.getTileComponent(rightPosition);
    Tile tileLeft = board.getTileComponent(leftPosition);

    if ((tileLeft != null && tileLeft.canMove) && (tileRight != null && tileRight.canMove)) {
      tileLeft.setPosition(rightPosition);
      tileRight.setPosition(leftPosition);

      tileRight.findMatch();
      tileLeft.findMatch();
    } else if (tileLeft == null && (tileRight != null && tileRight.canMove)) {
      board.boardTiles[_position.x + 1, _position.y] = null;
      tileRight.setPosition(leftPosition);
      tileRight.findMatch();
    } else if ((tileLeft != null && tileLeft.canMove) && tileRight == null) {
      board.boardTiles[_position.x, _position.y] = null;
      tileLeft.setPosition(rightPosition);
      tileLeft.findMatch();
    }

    Tile tileRightUp = board.getTileComponent(rightPosition + Vector2Int.down);
    Tile tileLeftUp = board.getTileComponent(leftPosition + Vector2Int.down);
    if (tileRightUp != null) {
      tileRightUp.fallTile();
      tileRightUp.findMatch();
    }
    if (tileLeftUp != null) {
      tileLeftUp.fallTile();
      tileLeftUp.findMatch();
    }

  }
  #endregion
}