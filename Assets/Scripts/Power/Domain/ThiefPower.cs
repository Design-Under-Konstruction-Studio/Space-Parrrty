using UnityEngine;

using Player;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Thief", menuName = "Powers/Thief", order = 1)]
    public class ThiefPower : Power
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