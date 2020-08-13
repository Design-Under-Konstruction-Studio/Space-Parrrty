using UnityEngine;

namespace Power.Events
{
    [CreateAssetMenu(fileName = "OnDarkPowerSuffered", menuName = "Powers/Events/On Dark Power Suffered", order = 1)]
    public class OnDarkPowerSuffered : ScriptableObject
    {
        public delegate void OnDarkPowerSufferedDelegate();
        private OnDarkPowerSufferedDelegate onDarkPowerSufferedDelegate;

        public void trigger()
        {
            if (onDarkPowerSufferedDelegate != null)
            {
                onDarkPowerSufferedDelegate();
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