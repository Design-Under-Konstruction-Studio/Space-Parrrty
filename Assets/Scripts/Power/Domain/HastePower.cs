using UnityEngine;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Haste", menuName = "Powers/Haste", order = 1)]
    public class HastePower : Power
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