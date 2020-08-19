using UnityEngine;

using Player;

using PowerModule.Base;

namespace PowerModule.Domain
{
    [CreateAssetMenu(fileName = "Line Breaker", menuName = "Powers/Line Breaker", order = 1)]
    public class LineBreakerPower : LightPower
    {
        override public void onPowerReleased(PlayerObject player)
        {
            player.destroyTopmostLine();
        }
        override public Power clone()
        {
            return new LineBreakerPower();
        }
    }
}