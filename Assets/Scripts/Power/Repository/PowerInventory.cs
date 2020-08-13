using UnityEngine;

using Player;

using PowerModule.Enum;

namespace PowerModule.Repository
{
    [System.Serializable]
    public class PowerInventory
    {
        [SerializeField]
        private Power[] powers = new Power[2];

        public void addPowersToInventory(Power lightPower, Power darkPower)
        {
            if (lightPower.Alignment == PowerAlignment.LightPower)
            {
                powers[0] = lightPower;
            }

            if (darkPower.Alignment == PowerAlignment.DarkPower)
            {
                powers[1] = darkPower;
            }
        }

        public void usePower(PowerAlignment powerAlignment, PlayerObject player)
        {
            powers[(int)powerAlignment].onPowerReleased(player);
        }

        public void clear()
        {
            powers[0] = null;
            powers[1] = null;
        }
    }
}