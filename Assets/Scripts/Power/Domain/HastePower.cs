using UnityEngine;

using Player;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Haste", menuName = "Powers/Haste", order = 1)]
    public class HastePower : Power
    {
        override public void onPowerReleased(PlayerObject player)
        {

        }
        override public Power clone()
        {
            return this;
        }
    }
}