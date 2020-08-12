using UnityEngine;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Saboteur", menuName = "Powers/Saboteur", order = 1)]
    public class SaboteurPower : Power
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