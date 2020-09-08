using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections.Generic;
using System.Linq;

using Player.Base;

using Power.Base;

namespace Player.Multiplayer
{
    public class PlayerHub : MonoBehaviour
    {
        [SerializeField]
        private List<PlayerObject> playerObjects = new List<PlayerObject>();

        public void onPlayerJoined(PlayerInput player)
        {
            playerObjects.Add(PlayerObject.from(this, player));
            playerObjects[playerObjects.Count - 1].name = "Player " + playerObjects.Count;
        }

        public void onPlayerLeft(PlayerInput player)
        {
            playerObjects.Remove(PlayerObject.from(this, player));
        }

        public void executeDarkPower(DarkPower darkPower, PlayerObject caster)
        {
            foreach (PlayerObject player in playerObjects.Where(p => p != caster))
            {
                player.sufferDarkPower(darkPower);
            }
        }
    }
}