using UnityEngine;

using Player;

using PowerModule.Base;

namespace PowerModule.Domain
{
    [CreateAssetMenu(fileName = "Anti-Blocks", menuName = "Powers/Anti-Blocks", order = 1)]
    public class AntiBlocksPower : LightPower
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