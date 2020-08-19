using UnityEngine;
using UnityEngine.InputSystem;

using PowerModule.Repository;
using PowerModule.Events;

using Board;

namespace Player
{
    public class PlayerObject : MonoBehaviour
    {
        #region Assignables
        [Header("Assign these to Scriptables and Prefabs")]
        [SerializeField]
        private PowerRepository powerRepository;

        [SerializeField]
        private OnPowerObtained onPowerObtained;
        #endregion

        #region Internal state
        [Header("Do not assign these")]
        [SerializeField]
        private PlayerStats stats;

        [SerializeField]
        private PowerInventory powerInventory;

        [SerializeField]
        private BoardController boardController;
        #endregion

        #region Monobehaviour
        private void Awake()
        {
            boardController = transform.GetComponentInChildren<BoardController>();

            onPowerObtained.subscribe(addPowersToInventory);
        }

        private void OnDestroy()
        {
            onPowerObtained.unsubscribe(addPowersToInventory);
        }
        #endregion

        #region Input callbacks
        public void useLightPower(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                powerInventory.useLightPower(this);
            }
        }

        public void useDarkPower(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                powerInventory.useDarkPower(this);
            }
        }
        #endregion

        #region Event callbacks
        private void addPowersToInventory()
        {
            powerInventory.addPowersToInventory(
                powerRepository.getRandomLightPower(),
                powerRepository.getRandomDarkPower());
        }
        #endregion

        #region Power methods
        //TODO: Improve security! Anyone with reference to this player may manipulate their shield status.
        public void turnOnShield(bool turnOnShield)
        {
            stats.HasShield = turnOnShield;
        }

        public void destroyTopmostLine()
        {
            GameObject[] topMostLine;
            int lineIndex = -1;
            do
            {
                lineIndex++;
                topMostLine = boardController.getTileLineGameObjectAsArray(lineIndex);
            } while (topMostLine.Length == 0);
            boardController.destroyLine(lineIndex);
        }
        #endregion

        #region Static methods
        public static PlayerObject from(PlayerInput playerInput)
        {
            return playerInput.GetComponentInParent<PlayerObject>();
        }

        #endregion
    }
}