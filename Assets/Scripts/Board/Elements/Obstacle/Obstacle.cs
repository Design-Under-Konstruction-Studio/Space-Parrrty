using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Board.Elements.Base;

namespace Board.Elements.Obstacle {
    public class Obstacle : BoardElement
    {
        override protected void setElementName() {
            gameObject.name = $"Tile {_position.x}-{Mathf.Abs(_position.y)}";
        }
        override protected void updateCurrentPosition() {
            transform.localPosition = new Vector3(_position.x, -(_position.y), 0);
        }
        override protected void onPositionSet() {
            if (boardController != null)
            {
                boardController.boardElements[_position.x, _position.y] = gameObject;
            }
        }
        override public void onPickerActivated(Vector2Int selectedNeighbourPosition) {

        }
        
        override public void onDestructionBelow() {

        }
        override public void fall() {
            Vector2Int bottomPosition = _position + Vector2Int.up;
            if (bottomPosition.y < boardController.bottomOfBoard)
            {
                BoardElement bottomTile = boardController.getTileComponent(_position + Vector2Int.up);
                BoardElement upTile = boardController.getTileComponent(_position + Vector2Int.down);
                if (bottomTile == null)
                {
                    boardController.boardElements[_position.x, _position.y] = null;
                    Position = bottomPosition;
                    if (upTile != null)
                    {
                        upTile.fall();
                    }
                    fall();
                }
            }
        }
        
        override protected void onBeforeDestroy() {

        }
        override protected void showDestructionAppearance() {

        }
        override protected void onDestroy() {

        }
        override protected void onAfterDestroy() {

        }
        
    }
}