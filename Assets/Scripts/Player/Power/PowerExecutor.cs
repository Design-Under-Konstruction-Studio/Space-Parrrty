using UnityEngine;

using Power.Base;

using Player.Base;
using Player.Multiplayer;

using Board;

namespace Player.Power
{
    [System.Serializable]
    public class PowerExecutor
    {
        #region Properties
        public bool IsShielded
        {
            get => isShielded;
            private set => isShielded = value;
        }

        public bool IsFrozen
        {
            get => isFrozen;
            private set => isFrozen = value;
        }
        #endregion

        #region Internal references
        [Header("Internal references - do not assign these")]
        [SerializeField]
        private PlayerObject player;

        [SerializeField]
        private BoardController boardController;

        [SerializeField]
        private PlayerHub hub;
        #endregion

        #region Internal State
        [Header("Internal State - do not assign these")]
        [SerializeField]
        private bool isShielded = false;

        [SerializeField]
        private bool isFrozen = false;
        #endregion

        public PowerExecutor(PlayerObject player, PlayerHub hub, BoardController boardController)
        {
            this.player = player;
            this.hub = hub;
            this.boardController = boardController;
        }

        #region Power Usage
        public void usePowerOnSelf(LightPower lightPower)
        {
            if (lightPower != null)
            {
                player.StartCoroutine(lightPower.execute(this));
            }
        }

        public void usePowerOnSelf(DarkPower darkPower)
        {
            if (darkPower != null)
            {
                player.StartCoroutine(darkPower.execute(this));
            }
        }

        public void usePowerOnOthers(DarkPower darkPower)
        {
            if (darkPower != null)
            {
                hub.executeDarkPower(darkPower, player);
            }
        }
        #endregion

        #region Power Specifics
        public void switchShield(bool isShieldOn)
        {
            isShielded = isShieldOn;
        }

        public void switchFreeze(bool isFreezeOn)
        {
            isFrozen = isFreezeOn;
        }

        public void destroyTopmostLines(int breakableLines)
        {
            GameObject[] topMostLine;
            int lineIndex = -1;
            do
            {
                lineIndex++;
                topMostLine = boardController.getTileLineGameObjectAsArray(lineIndex);
            } while (topMostLine.Length == 0);

            for (int lineCount = 0; lineCount < breakableLines; lineCount++)
            {
                boardController.destroyLine(lineIndex);
                lineIndex++;
            }
        }
        #endregion
    }
}