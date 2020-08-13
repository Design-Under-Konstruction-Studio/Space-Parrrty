using UnityEngine;

using Player;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Anti-Blocks", menuName = "Powers/Anti-Blocks", order = 1)]
    public class AntiBlocksPower : Power
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