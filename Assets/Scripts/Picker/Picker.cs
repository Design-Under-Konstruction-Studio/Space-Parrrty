using Board.Behaviour;
using Board.Elements.Tile;
using Board.Elements.Base;
using UnityEngine;

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

    public Vector2Int getNewPosition(Vector2Int movementDirection)
    {
        return _position + movementDirection;
    }

    public void move(Vector2Int newPosition)
    {
        _position = newPosition;
        transform.localPosition = new Vector3(_position.x + 0.5f, -_position.y, transform.localPosition.z);
    }
    #endregion

    #region ChangeTilePosition
    public void changeTilePosition()
    {
        if (board.boardStatusTypes != BoardStatusTypes.start) return;

        Vector2Int rightPosition = new Vector2Int(_position.x + 1, _position.y);
        Vector2Int leftPosition = new Vector2Int(_position.x, _position.y);

        Tile tileRight = board.getTileComponent(rightPosition);
        Tile tileLeft = board.getTileComponent(leftPosition);

        if ((tileLeft != null && tileLeft.CanMove) && (tileRight != null && tileRight.CanMove))
        {
            tileLeft.Position = rightPosition;
            tileRight.Position = leftPosition;

            tileRight.findMatch();
            tileLeft.findMatch();
        }
        else if (tileLeft == null && (tileRight != null && tileRight.CanMove))
        {
            board.boardElements[_position.x + 1, _position.y] = null;
            tileRight.Position = leftPosition;
            tileRight.findMatch();
        }
        else if ((tileLeft != null && tileLeft.CanMove) && tileRight == null)
        {
            board.boardElements[_position.x, _position.y] = null;
            tileLeft.Position = rightPosition;
            tileLeft.findMatch();
        }

        BoardElement tileRightUp = board.getBoardElement(rightPosition + Vector2Int.down);
        BoardElement tileLeftUp = board.getBoardElement(leftPosition + Vector2Int.down);
        if (tileRightUp != null)
        {
            tileRightUp.fall();
            tileRightUp.findMatch();
        }
        if (tileLeftUp != null)
        {
            tileLeftUp.fall();
            tileLeftUp.findMatch();
        }
    }
    #endregion
}