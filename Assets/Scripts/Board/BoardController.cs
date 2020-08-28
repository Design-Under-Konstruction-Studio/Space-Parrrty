using System.Collections.Generic;
using System.Collections;
using TileController.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Board
{
    public class BoardController : MonoBehaviour
    {
        // Scripts References
        BoardGenerate boardGenerate;

        [Header("Board Settings")]
        public BoardStatusTypes boardStatusTypes;
        public Vector2Int boardSize;

        [HideInInspector]
        public GameObject tilesObjects;

        [HideInInspector]
        public int topOfBoard;
        [HideInInspector]
        public Vector3 higherTilePosition;
        [HideInInspector]
        public int bottomOfBoard;
        [HideInInspector]
        public Vector3 lowerTilePosition;
        [HideInInspector]
        public int bottomOfBoardOffSet;

        public GameObject[,] boardTiles;

        public GameObject boardTransform;
        public float moveUpSpeed;

        private void Awake()
        {
            boardGenerate = GetComponent<BoardGenerate>();
        }

        #region Getter Tiles
        public GameObject getTileGameObject(Vector2Int position)
        {
            if (position.x >= boardSize.x || position.y >= bottomOfBoard)
            {
                return null;
            }
            return boardTiles[position.x, position.y].gameObject;
        }

        public Tile getTileComponent(Vector2Int position)
        {
            if (position.x >= boardSize.x || position.y >= bottomOfBoard || position.x < 0 || position.y < 0)
            {
                return null;
            }
            GameObject tile = boardTiles[position.x, position.y];

            if (tile == null)
            {
                return null;
            }

            return tile.GetComponent<Tile>();
        }

        public Tile getUpTileComponent(Vector2Int position)
        {
            return null;
        }

        public Tile getDownTileComponent(Vector2Int position)
        {
            return null;
        }

        public List<GameObject> getTileLineGameObject(int positionY)
        {
            List<GameObject> lineTile = new List<GameObject>();
            for (int i = 0; i < boardSize.x; i++)
            {
                lineTile.Add(boardTiles[i, positionY]);
            }
            return lineTile;
        }

        public GameObject[] getTileLineGameObjectAsArray(int positionY)
        {
            List<GameObject> tileLine = getTileLineGameObject(positionY);
            tileLine.RemoveAll(tile => tile == null);
            return tileLine.ToArray();
        }
        #endregion

        public void changeBoardType()
        {
            switch (boardStatusTypes)
            {
                case BoardStatusTypes.loading:
                    boardStatusTypes = BoardStatusTypes.countdown;
                    StartCoroutine(boardCountdownCoroutine());
                    break;
                case BoardStatusTypes.countdown:
                    boardStatusTypes = BoardStatusTypes.start;
                    break;
                case BoardStatusTypes.start:
                    boardStatusTypes = BoardStatusTypes.finish;
                    break;
                case BoardStatusTypes.finish:
                    break;
                default:
                    break;
            }
            Debug.Log(boardStatusTypes.ToString());
        }

        private IEnumerator boardCountdownCoroutine()
        {
            int countdownTime = 5;

            while (countdownTime > 0)
            {
                yield return new WaitForSeconds(1f);
                // Adicionar update da Tela sobre a contagem
                countdownTime--;
            }
            changeBoardType();
        }

        public void finishBoardStatusType()
        {
            if (boardStatusTypes == BoardStatusTypes.start)
            {
                boardStatusTypes = BoardStatusTypes.finish;
            }
        }

        public void checkTileAtTop()
        {
            List<GameObject> topTileLine = getTileLineGameObject(topOfBoard);
            foreach (GameObject item in topTileLine)
            {
                if (item != null)
                {
                    finishBoardStatusType();
                    break;
                }
            }
        }

        public void destroyLine(int yIndex)
        {
            GameObject[] lineToBeDestroyed = getTileLineGameObjectAsArray(yIndex);
            foreach (GameObject tileGameObject in lineToBeDestroyed)
            {
                Tile tileToBeDestroyed = tileGameObject.GetComponent<Tile>();
                tileToBeDestroyed.destroy();
            }
        }

        public void accelerate(float speedMultiplier)
        {
            moveUpSpeed *= speedMultiplier;
        }

        public void retard(float speedDivider)
        {
            moveUpSpeed /= speedDivider;
        }

        public void checkMatchLine(int positionY)
        {
            List<GameObject> lineTiles = getTileLineGameObject(positionY);
            foreach (GameObject tile in lineTiles)
            {
                if (tile != null)
                {
                    tile.GetComponent<Tile>().findMatch();
                }
            }


        }
    }
}
