using UnityEngine;

using Player;

namespace PowerModule.Domain
{
    [CreateAssetMenu(fileName = "Ice", menuName = "Powers/Ice", order = 1)]
    public class IcePower : Power
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