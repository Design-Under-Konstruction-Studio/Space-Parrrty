using System.Collections;
using System.Collections.Generic;
using Board;
using TileController;
using UnityEngine;
using UnityEngine.InputSystem;

public class Picker : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _position;

    [SerializeField]
    private BoardController board;

    private void Awake()
    {
        board = GetComponentInParent<BoardController>();
        transform.localPosition = new Vector3(_position.x + 0.5f, -_position.y, transform.localPosition.z);
    }

    #region Movimentação
    bool canMovePosition(Vector2Int newPosition)
    {
        if (newPosition.x < board.boardSize.x - 1 &&
          newPosition.y < board.bottomOfBoard &&
          newPosition.x >= 0 &&
          newPosition.y >= board.topOfBoard &&
          board.boardStatusTypes == BoardStatusTypes.start
        )
        {
            return true;
        }

        return false;
    }

    public void move(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Vector2Int movementDirection = new Vector2Int((int)ctx.ReadValue<Vector2>().x, -(int)ctx.ReadValue<Vector2>().y);
            Vector2Int newPosition = getNewPosition(movementDirection);
            if (canMovePosition(newPosition))
            {
                _position = newPosition;
                transform.localPosition = new Vector3(_position.x + 0.5f, -_position.y, transform.localPosition.z);
            }
        }
    }
    public Vector2Int getNewPosition(Vector2Int movementDirection)
    {
        return _position + movementDirection;
    }
    #endregion

    #region ChangeTilePosition
    public void changeTilePosition(InputAction.CallbackContext ctx)
    {
        if(ctx.performed) {
            Vector2Int rightPosition = new Vector2Int(_position.x + 1, _position.y);
            Vector2Int leftPosition = new Vector2Int(_position.x, _position.y);

            Tile tileRight = board.getTileComponent(rightPosition);
            Tile tileLeft = board.getTileComponent(leftPosition);

            if ((tileLeft != null && tileLeft.canMove) && (tileRight != null && tileRight.canMove))
            {
                tileLeft.setPosition(rightPosition);
                tileRight.setPosition(leftPosition);

                tileRight.findMatch();
                tileLeft.findMatch();
            }
            else if (tileLeft == null && (tileRight != null && tileRight.canMove))
            {
                board.boardTiles[_position.x + 1, _position.y] = null;
                tileRight.setPosition(leftPosition);
                tileRight.findMatch();
            }
            else if ((tileLeft != null && tileLeft.canMove) && tileRight == null)
            {
                board.boardTiles[_position.x, _position.y] = null;
                tileLeft.setPosition(rightPosition);
                tileLeft.findMatch();
            }

            Tile tileRightUp = board.getTileComponent(rightPosition + Vector2Int.down);
            Tile tileLeftUp = board.getTileComponent(leftPosition + Vector2Int.down);
            if (tileRightUp != null)
            {
                tileRightUp.fallTile();
                tileRightUp.findMatch();
            }
            if (tileLeftUp != null)
            {
                tileLeftUp.fallTile();
                tileLeftUp.findMatch();
            }
        }
    }
    #endregion
}