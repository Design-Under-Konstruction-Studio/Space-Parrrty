using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections.Generic;

using PowerModule.Base;
using PowerModule.Events;

namespace Player
{
    public class PlayerHub : MonoBehaviour
    {
        [SerializeField]
        private List<PlayerObject> playerObjects = new List<PlayerObject>();

        [SerializeField]
        private OnDarkPowerReleased onDarkPowerReleased;

        public void onPlayerJoined(PlayerInput player)
        {
            playerObjects.Add(PlayerObject.from(player));
        }

        public void onPlayerLeft(PlayerInput player)
        {
            playerObjects.Remove(PlayerObject.from(player));
        }

        private void spreadDarkPower(PlayerObject actingPlayer, DarkPower power)
        {
            foreach (PlayerObject player in playerObjects)
            {
                if (player != actingPlayer)
                {
                    power.releasePower(player);
                }
            }
        }

        public void testPowerOnPlayer(Power power)
        {
            if (playerObjects.Count > 0)
            {
                power.onPowerReleased(playerObjects[0]);
            }
        }
    }
}