using UnityEngine;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Line Breaker", menuName = "Powers/Line Breaker", order = 1)]
    public class LineBreakerPower : Power
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