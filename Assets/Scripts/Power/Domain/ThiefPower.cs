using UnityEngine;

using Player;

using PowerModule.Base;

namespace PowerModule.Domain
{
    [CreateAssetMenu(fileName = "Thief", menuName = "Powers/Thief", order = 1)]
    public class ThiefPower : DarkPower
    {
        override public void onPowerReleased(PlayerObject player)
        {

        }
        override public Power clone()
        {
            return this;
        }

        override public void releasePower(PlayerObject player)
        {

        }
    }
}