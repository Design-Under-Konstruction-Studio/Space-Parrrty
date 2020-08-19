using UnityEngine;

using Player;

using PowerModule.Base;

namespace PowerModule.Domain
{
    [CreateAssetMenu(fileName = "Saboteur", menuName = "Powers/Saboteur", order = 1)]
    public class SaboteurPower : DarkPower
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