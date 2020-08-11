using UnityEngine;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Powers/Shield", order = 1)]
    public class ShieldPower : Power
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