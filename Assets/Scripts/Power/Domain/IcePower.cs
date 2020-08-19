using UnityEngine;

using Player;

using PowerModule.Base;

namespace PowerModule.Domain
{
    [CreateAssetMenu(fileName = "Ice", menuName = "Powers/Ice", order = 1)]
    public class IcePower : DarkPower
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