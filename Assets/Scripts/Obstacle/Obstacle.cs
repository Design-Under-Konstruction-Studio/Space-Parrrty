using System.Collections;
using System.Collections.Generic;
using Board;
using TileController.Base;
using UnityEngine;

public class Obstacle : Tile
{
    public int ObstacleSize
    {
        get
        {
            return obstacleSize;
        }
        private set { }
    }

    BoardController boardController;

    int obstacleSize = 5;

    private void Awake()
    {
        boardController = transform.parent.parent.parent.gameObject.GetComponent<BoardController>();
        board = transform.parent.parent.parent.gameObject.GetComponent<BoardController>();
        boardGenerate = transform.parent.parent.parent.gameObject.GetComponent<BoardGenerate>();
    }

    override protected void updateCurrentPosition()
    {
        setTileName();

        transform.localPosition = new Vector3(_position.x + obstacleSize / 2, -(_position.y), 0);
    }

    override public void setPosition(Vector2Int position)
    {
        _position = position;
        if (boardController != null)
        {
            for (int i = 0; i < obstacleSize; i++)
            {
                boardController.boardTiles[i, position.y] = gameObject;
            }
        }
    }

    override public void setTileName()
    {
        gameObject.name = $"Obstacle {_position.x}-{Mathf.Abs(_position.y)}";
    }

    override public void fallTile()
    {
        Vector2Int bottomPosition = this._position + Vector2Int.up;
        if (bottomPosition.y < boardController.bottomOfBoard)
        {
            List<Tile> upTileList = new List<Tile>();
            for (int i = 0; i < obstacleSize; i++)
            {
                Tile bottomTile = boardController.getTileComponent(new Vector2Int(i, this._position.y) + Vector2Int.up);
                upTileList.Add(boardController.getTileComponent(new Vector2Int(i, this._position.y) + Vector2Int.down));
                if (bottomTile != null)
                {
                    return;
                }
            }

            for (int i = 0; i < obstacleSize; i++)
            {
                boardController.boardTiles[i, _position.y] = null;
            }

            setPosition(new Vector2Int(0, _position.y) + Vector2Int.up);

            upTileList.ForEach(delegate (Tile upTile)
            {
                if (upTile != null)
                {
                    upTile.fallTile();
                }
            });

            fallTile();
        }
    }
}