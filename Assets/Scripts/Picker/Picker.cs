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
    getInputKeyDownMoving();

    if (Input.GetKeyDown(KeyCode.Space)) {
      changeTilePosition();
    }

    #endregion
  }
  
  #region Movimentação
  bool canMovePosition(Vector2Int newPosition) {
    if(newPosition.x < board.boardSize.x - 1 && 
      newPosition.y < board.bottomOfBoard && 
      newPosition.x >= 0 && 
      newPosition.y >= board.topOfBoard
    ) {
      return true;
    }
    
    return false;
  }

  void getInputKeyDownMoving() {
    Vector2Int newPosition = new Vector2Int(_position.x, _position.y);
    
    if (Input.GetKeyDown(KeyCode.UpArrow))
      newPosition = moveUp();
    if (Input.GetKeyDown(KeyCode.LeftArrow))
      newPosition = moveLeft();
    if (Input.GetKeyDown(KeyCode.DownArrow))
      newPosition = moveDown();
    if (Input.GetKeyDown(KeyCode.RightArrow))
      newPosition = moveRight();

    if(canMovePosition(newPosition)){
      _position = newPosition;
    }
  }

  public Vector2Int moveLeft() {
    return new Vector2Int(_position.x-1, _position.y);
  }

  public Vector2Int moveRight() {
    return new Vector2Int(_position.x+1, _position.y);
  }

  public Vector2Int moveUp() {
    return new Vector2Int(_position.x, _position.y-1);
  }

  public Vector2Int moveDown() {
    return new Vector2Int(_position.x, _position.y+1);
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