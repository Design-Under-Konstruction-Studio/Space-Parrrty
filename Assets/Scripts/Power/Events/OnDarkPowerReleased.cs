using UnityEngine;

using Player;

using PowerModule.Base;

namespace PowerModule.Events
{
    [CreateAssetMenu(fileName = "OnDarkPowerReleased", menuName = "Powers/Events/On Dark Power Released", order = 1)]
    public class OnDarkPowerReleased : ScriptableObject
    {
        public delegate void OnDarkPowerReleasedDelegate(PlayerObject player, DarkPower power);
        private OnDarkPowerReleasedDelegate onDarkPowerReleasedDelegate;

        public void trigger(PlayerObject player, DarkPower power)
        {
            if (onDarkPowerReleasedDelegate != null)
            {
                onDarkPowerReleasedDelegate(player, power);
            }
        }

        public void subscribe(OnDarkPowerReleasedDelegate func)
        {
            onDarkPowerReleasedDelegate += func;
        }

        public void unsubscribe(OnDarkPowerReleasedDelegate func)
        {
            onDarkPowerReleasedDelegate -= func;
        }
    }
}