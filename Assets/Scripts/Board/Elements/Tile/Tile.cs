using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Board.Elements.Base;

namespace Board.Elements.Tile
{
    public class Tile : BoardElement
    {
        [SerializeField]
        private TileType tileType;

        #region Implementation layer
        override public void onPickerActivated(Vector2Int selectedNeighbourPosition)
        {

        }
        override public void onDestructionBelow()
        {
            fall();
        }
        override public void fall()
        {
            Vector2Int bottomPosition = _position + Vector2Int.up;
            if (bottomPosition.y < boardController.bottomOfBoard)
            {
                Tile bottomTile = boardController.getTileComponent(_position + Vector2Int.up);
                Tile upTile = boardController.getTileComponent(_position + Vector2Int.down);
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
        override protected void updateCurrentPosition()
        {
            transform.localPosition = new Vector3(_position.x, -(_position.y), 0);
        }
        override protected void onPositionSet()
        {
            if (boardController != null)
            {
                boardController.boardElements[_position.x, _position.y] = gameObject;
            }
        }
        override protected void setElementName()
        {
            gameObject.name = $"Tile {_position.x}-{Mathf.Abs(_position.y)}";
        }
        override protected void showDestructionAppearance()
        {
            Color tileColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(tileColor.r, tileColor.g, tileColor.b, 0.5f);
        }
        override protected void onBeforeDestroy()
        {

        }
        override protected void onDestroy()
        {
            boardController.boardElements[_position.x, _position.y] = null;
        }
        override protected void onAfterDestroy()
        {
            BoardElement upperElement = boardController.getBoardElement(this._position + Vector2Int.down);
            if (upperElement != null)
            {
                upperElement.onDestructionBelow();
            }
        }
        #endregion

        #region Matchmaking
        public void findMatch()
        {
            StartCoroutine(findMatchCR());
        }

        private IEnumerator findMatchCR()
        {
            yield return new WaitForSeconds(0.1f);

            fall();

            List<Tile> matchedTiles = new List<Tile>();
            matchedTiles.AddRange(findTileDirection(new Vector2Int[2] { Vector2Int.left, Vector2Int.right }));
            matchedTiles.AddRange(findTileDirection(new Vector2Int[2] { Vector2Int.up, Vector2Int.down }));

            if (matchedTiles.Count >= 3)
            {
                onMatchFound(matchedTiles.Count - 3);
            }

            foreach (var item in matchedTiles)
            {
                item.destroy();
            }
        }

        List<Tile> findTileDirection(Vector2Int[] directions)
        {
            List<Tile> matchedTiles = new List<Tile>();
            matchedTiles.Add(this);

            for (int i = 0; i < directions.Length; i++)
            {
                Tile nextTile = this;
                for (int e = 0; e < matchedTiles.Count; e++)
                {
                    nextTile = boardController.getTileComponent(nextTile._position + directions[i]);
                    if (nextTile != null && nextTile.tileType == this.tileType && !nextTile.isDestroying)
                    {
                        matchedTiles.Add(nextTile);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return matchedTiles.Count > 2 ? matchedTiles : new List<Tile>();
        }
        #endregion

        #region Hooks
        virtual protected void onMatchFound(int level) { }
        #endregion
    }
}