// using System.Collections;
// using System.Collections.Generic;
// using Board.Behaviour;
// using Board.Elements.Base;
// using UnityEngine;

// public class Obstacle : BoardElement
// {
//     public int ObstacleSize
//     {
//         get
//         {
//             return obstacleSize;
//         }
//         private set { }
//     }

//     int obstacleSize = 5;

//     override protected void onPositionSet()
//     {
//         transform.localPosition = new Vector3(_position.x + obstacleSize / 2, -(_position.y), 0);
//     }

//     // override public void setPosition(Vector2Int position)
//     // {
//     //     _position = position;
//     //     if (boardController != null)
//     //     {
//     //         for (int i = 0; i < obstacleSize; i++)
//     //         {
//     //             boardController.boardTiles[i, position.y] = gameObject;
//     //         }
//     //     }
//     // }

//     override public void setElementName()
//     {
//         gameObject.name = $"Obstacle {_position.x}-{Mathf.Abs(_position.y)}";
//     }

//     override public void fall()
//     {
//         Vector2Int bottomPosition = this._position + Vector2Int.up;
//         if (bottomPosition.y < boardController.bottomOfBoard)
//         {
//             List<Tile> upTileList = new List<Tile>();
//             for (int i = 0; i < obstacleSize; i++)
//             {
//                 Tile bottomTile = boardController.getTileComponent(new Vector2Int(i, this._position.y) + Vector2Int.up);
//                 upTileList.Add(boardController.getTileComponent(new Vector2Int(i, this._position.y) + Vector2Int.down));
//                 if (bottomTile != null)
//                 {
//                     return;
//                 }
//             }

//             for (int i = 0; i < obstacleSize; i++)
//             {
//                 boardController.boardTiles[i, _position.y] = null;
//             }

//             setPosition(new Vector2Int(0, _position.y) + Vector2Int.up);

//             upTileList.ForEach(delegate (Tile upTile)
//             {
//                 if (upTile != null)
//                 {
//                     upTile.fallTile();
//                 }
//             });

//             fallTile();
//         }
//     }

//     protected override void onDestroy()
//     {
//         boardGenerate.createLine(_position.y);

//         Tile upTile = board.getTileComponent(this._position + Vector2Int.down);
//         if (upTile != null && upTile.typeTile == TileTypes.Obstacle && !upTile.inMatch)
//         {
//             StartCoroutine(upTile.destroyTile());
//         }
//     }
// }