using UnityEngine;

using PowerModule.Events;
using Player;

namespace PowerModule.Base
{
    public abstract class DarkPower : Power
    {
        [SerializeField]
        protected OnDarkPowerReleased onDarkPowerReleased;

        override public void onPowerReleased(PlayerObject player)
        {
            onDarkPowerReleased.trigger(player, this);
        }

        public abstract void releasePower(PlayerObject player);
    }
}