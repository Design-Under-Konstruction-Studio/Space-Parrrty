using UnityEngine;

using Player;

using PowerModule.Base;

namespace PowerModule.Domain
{
    [CreateAssetMenu(fileName = "Haste", menuName = "Powers/Haste", order = 1)]
    public class HastePower : DarkPower
    {
        override public Power clone()
        {
            return this;
        }

        override public void releasePower(PlayerObject player)
        {

        }
    }
}