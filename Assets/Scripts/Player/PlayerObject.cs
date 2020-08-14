using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections.Generic;

using PowerModule;
using PowerModule.Repository;
using PowerModule.Events;
using PowerModule.Enum;

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

        [SerializeField]
        private OnDarkPowerSuffered onDarkPowerSuffered;
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
            onDarkPowerSuffered.subscribe(sufferDarkPower);
        }

        private void OnDestroy()
        {
            onPowerObtained.unsubscribe(addPowersToInventory);
            onDarkPowerSuffered.unsubscribe(sufferDarkPower);
        }
        #endregion

        #region Input callbacks
        public void useLightPower(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                powerInventory.usePower(PowerAlignment.LightPower, this);
            }
        }

        public void useDarkPower(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                powerInventory.usePower(PowerAlignment.LightPower, this);
            }
        }
        #endregion

        #region Event callbacks
        private void addPowersToInventory()
        {
            powerInventory.addPowersToInventory(
                powerRepository.getRandomPowerByAlignment(PowerAlignment.LightPower),
                powerRepository.getRandomPowerByAlignment(PowerAlignment.DarkPower));
        }

        public void sufferDarkPower(Power power)
        {
            if (!stats.HasShield)
            {
                //TODO: Suffer dark power
            }
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
    }
}