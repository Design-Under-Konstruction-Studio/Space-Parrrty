using UnityEngine;

namespace PowerModule.Events
{
    [CreateAssetMenu(fileName = "OnDarkPowerSuffered", menuName = "Powers/Events/On Dark Power Suffered", order = 1)]
    public class OnDarkPowerSuffered : ScriptableObject
    {
        public delegate void OnDarkPowerSufferedDelegate(Power power);
        private OnDarkPowerSufferedDelegate onDarkPowerSufferedDelegate;

        public void trigger(Power power)
        {
            if (onDarkPowerSufferedDelegate != null)
            {
                onDarkPowerSufferedDelegate(power);
            }
        }

        public void subscribe(OnDarkPowerSufferedDelegate func)
        {
            onDarkPowerSufferedDelegate += func;
        }

        public void unsubscribe(OnDarkPowerSufferedDelegate func)
        {
            onDarkPowerSufferedDelegate -= func;
        }
    }
}