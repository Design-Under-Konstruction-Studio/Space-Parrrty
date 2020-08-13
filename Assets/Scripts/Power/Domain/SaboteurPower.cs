using UnityEngine;

using Player;

namespace PowerModule.Domain
{
    [CreateAssetMenu(fileName = "Saboteur", menuName = "Powers/Saboteur", order = 1)]
    public class SaboteurPower : Power
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