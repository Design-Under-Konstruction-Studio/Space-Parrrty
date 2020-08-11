using UnityEngine;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Anti-Blocks", menuName = "Powers/Anti-Blocks", order = 1)]
    public class AntiBlocksPower : Power
    {
        override public void onPowerGained()
        {

        }
        override public void onPowerLost()
        {

        }
        override public void onPowerReleased()
        {

        }
        override public Power clone()
        {
            return this;
        }
    }
}