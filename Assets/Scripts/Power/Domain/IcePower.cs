using UnityEngine;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Ice", menuName = "Powers/Ice", order = 1)]
    public class IcePower : Power
    {
        override public void onPowerReleased()
        {

        }
        override public Power clone()
        {
            return this;
        }
    }
}