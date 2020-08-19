using UnityEngine;

using Player;

using PowerModule.Base;

namespace PowerModule.Repository
{
    [System.Serializable]
    public class PowerInventory
    {
        [SerializeField]
        private DarkPower darkPower;

        [SerializeField]
        private LightPower lightPower;

        public void addPowersToInventory(LightPower lightPower, DarkPower darkPower)
        {
            this.lightPower = lightPower;
            this.darkPower = darkPower;
        }

        public void useLightPower(PlayerObject player)
        {
            if (lightPower != null)
            {
                lightPower.onPowerReleased(player);
            }
            clear();
        }

        public void useDarkPower(PlayerObject player)
        {
            if (darkPower != null)
            {
                darkPower.onPowerReleased(player);
            }
            clear();
        }

        public void clear()
        {
            lightPower = null;
            darkPower = null;
        }
    }
}