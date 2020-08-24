using UnityEngine;
using UnityEngine.InputSystem;

using Board;

using Power.Base;

using Player.Power;
using Player.Multiplayer;

namespace Player.Base
{
    public class PlayerObject : MonoBehaviour
    {
        #region Internal state
        [Header("Internal state - do not assign these")]
        [SerializeField]
        private BoardController boardController;

        [SerializeField]
        private BoardGenerate boardGenerator;

        [SerializeField]
        private Picker picker;

        [SerializeField]
        private PlayerHub playerHub;

        [SerializeField]
        private PowerInventory powerInventory;

        [SerializeField]
        private PowerExecutor powerExecutor;
        #endregion

        public void sufferDarkPower(DarkPower darkPower)
        {
            if (!powerExecutor.IsShielded)
            {
                powerExecutor.usePowerOnSelf(darkPower);
            }
        }

        #region Static methods
        public static PlayerObject from(PlayerHub playerHub, PlayerInput playerInput)
        {
            PlayerObject player = playerInput.GetComponentInParent<PlayerObject>();

            player.playerHub = playerHub;
            player.boardController = player.transform.GetComponentInChildren<BoardController>();
            player.boardGenerator = player.transform.GetComponentInChildren<BoardGenerate>();
            player.picker = player.transform.GetComponentInChildren<Picker>();
            player.powerInventory.setup();
            player.powerExecutor = new PowerExecutor(player, playerHub, player.boardController);

            return player;
        }
        #endregion

        #region Input Processing
        public void move(InputAction.CallbackContext ctx)
        {
            if (ctx.performed && !powerExecutor.IsFrozen)
            {
                Vector2Int movementDirection = new Vector2Int((int)ctx.ReadValue<Vector2>().x, -(int)ctx.ReadValue<Vector2>().y);
                Vector2Int newPosition = picker.getNewPosition(movementDirection);
                if (picker.canMovePosition(newPosition))
                {
                    picker.move(newPosition);
                }
            }
        }

        public void changeTilePosition(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                picker.changeTilePosition();
            }
        }

        public void changeBoardState(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                boardController.changeBoardType();
            }
        }

        public void createObstacle(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                boardGenerator.createObstacle();
            }
        }

        public void useLightPower(InputAction.CallbackContext ctx)
        {
            powerExecutor.usePowerOnSelf(powerInventory.LightPower);
            powerInventory.clean();
        }

        public void useDarkPower(InputAction.CallbackContext ctx)
        {
            powerExecutor.usePowerOnSelf(powerInventory.DarkPower);
            powerInventory.clean();
        }
        #endregion
    }
}