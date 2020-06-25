using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{

  public class BoardBehaviour : MonoBehaviour
  {
    BoardController boardController;
    BoardGenerate boardGenerate;

    private void Awake()
    {
      boardController = GetComponent<BoardController>();
      boardGenerate = GetComponent<BoardGenerate>();
    }

    private void FixedUpdate()
    {
      moveBoardUp();
    }

    private void moveBoardUp()
    {
      if (boardController.boardStatusTypes == BoardStatusTypes.start)
      {
        boardController.boardTransform.transform.Translate(0, 1 * boardController.moveUpSpeed * Time.deltaTime, 0);

        GameObject lowerTile = boardController.boardTiles[0, boardController.bottomOfBoard - 1];
        GameObject bottomLowerTile = boardController.boardTiles[0, boardController.bottomOfBoard];

        if (lowerTile == null || bottomLowerTile == null)
          return;

        if (boardController.lowerTilePosition == Vector3.zero)
          boardController.lowerTilePosition = lowerTile.transform.position;

        if (bottomLowerTile.transform.position.y >= boardController.lowerTilePosition.y)
        {
          boardController.lowerTilePosition = Vector3.zero;
          boardGenerate.createDownLine();
          boardController.checkTileAtTop();
        }
      }
    }
  }
}
