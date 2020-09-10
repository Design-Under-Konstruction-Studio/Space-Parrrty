using UnityEngine;

namespace TileController.Power
{
    [CreateAssetMenu(fileName = "On Power Acquired", menuName = "Powers/Events/On Power Acquired", order = 1)]
    public class OnPowerAcquired : ScriptableObject
    {
        public delegate void OnPowerAcquiredDelegate(int level);
        private OnPowerAcquiredDelegate onPowerAcquiredDelegate;

        public void trigger(int level)
        {
            if (onPowerAcquiredDelegate != null)
            {
                onPowerAcquiredDelegate(level);
            }
        }

        public void subscribe(OnPowerAcquiredDelegate func)
        {
            onPowerAcquiredDelegate += func;
        }

        public void unsubscribe(OnPowerAcquiredDelegate func)
        {
            onPowerAcquiredDelegate -= func;
        }
    }
}