using System.Collections;
using System.Collections.Generic;
using Board.Behaviour;
using UnityEngine;

namespace Board.Elements.Base
{
    public abstract class BoardElement : MonoBehaviour
    {
        #region Constants
        private const float DESTRUCTION_DELAY = 2;
        #endregion

        #region Visible references
        #endregion

        #region Properties
        public Vector2Int Position
        {
            get => _position;
            set
            {
                _position = value;
                onPositionSet();
            }
        }
        public bool CanMove
        {
            get => canMove;
            set
            {
                canMove = value;
            }
        }
        #endregion

        #region Internal references
        [SerializeField]
        protected BoardController boardController;
        [SerializeField]
        protected BoardGenerate boardGenerate;
        #endregion

        #region Internal state
        [SerializeField]
        protected Vector2Int _position;
        [SerializeField]
        protected bool canMove = true;
        [SerializeField]
        protected bool isDestroying = false;
        #endregion


        #region MonoBehaviour
        protected void Awake()
        {
            boardController = GetComponentInParent<BoardController>();
            boardGenerate = GetComponentInParent<BoardGenerate>();
            boardController = transform.parent.parent.parent.gameObject.GetComponent<BoardController>();
            boardGenerate = transform.parent.parent.parent.gameObject.GetComponent<BoardGenerate>();
        }

        protected void Update()
        {
            updateCurrentPosition();
            setElementName();
        }
        #endregion

        #region Public interface
        public void destroy()
        {
            StartCoroutine(destroyCR());
        }
        #endregion

        #region Internal methods
        private IEnumerator destroyCR()
        {
            canMove = false;
            isDestroying = true;

            onBeforeDestroy();

            showDestructionAppearance();

            yield return new WaitForSeconds(DESTRUCTION_DELAY);

            onDestroy();

            onAfterDestroy();

            Destroy(gameObject);
        }
        #endregion


        #region Abstraction layer
        abstract public void onPickerActivated(Vector2Int selectedNeighbourPosition);
        abstract public void onDestructionBelow();
        abstract public void fall();
        abstract public void findMatch();
        abstract protected void updateCurrentPosition();
        abstract protected void onPositionSet();
        abstract protected void setElementName();
        abstract protected void showDestructionAppearance();
        abstract protected void onBeforeDestroy();
        abstract protected void onDestroy();
        abstract protected void onAfterDestroy();
        #endregion
    }
}