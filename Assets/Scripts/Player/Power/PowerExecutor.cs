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
        private PowerInventory powerInventory;

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

        public PowerExecutor(PlayerObject player, PlayerHub hub, BoardController boardController, PowerInventory powerInventory)
        {
            this.player = player;
            this.hub = hub;
            this.boardController = boardController;
            this.powerInventory = powerInventory;
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

        public int getTopmostLineIndex()
        {
            GameObject[] topMostLine;
            int lineIndex = -1;
            do
            {
                lineIndex++;
                topMostLine = boardController.getTileLineGameObjectAsArray(lineIndex);
            } while (topMostLine.Length == 0);

            return lineIndex;
        }

        public void createObstacle() {
            boardController.boardGenerate.createObstacle();
        }

        public void destroyLine(int breakableLineIndex)
        {
            boardController.destroyLine(breakableLineIndex);
        }
        
        public int accelerateBoard(float speedMultiplier)
        {
            boardController.accelerate(speedMultiplier);
            return boardController.CreatedLines;
        }

        public void retardBoard(float speedDivider)
        {
            boardController.retard(speedDivider);
        }

        public bool boardReachedHasteLimit(int originalNumberOfLines, int hasteNewLineLimit)
        {
            return boardController.CreatedLines >= originalNumberOfLines + hasteNewLineLimit;
        }

        public void losePowers(bool shouldLoseLightPower, bool shouldLoseDarkPower)
        {
            powerInventory.clean(shouldLoseLightPower, shouldLoseDarkPower);
        }
        #endregion
    }
}