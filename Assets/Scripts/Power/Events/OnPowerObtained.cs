using UnityEngine;

namespace PowerModule.Events
{
    [CreateAssetMenu(fileName = "OnPowerObtained", menuName = "Powers/Events/On Power Obtained", order = 1)]
    public class OnPowerObtained : ScriptableObject
    {
        public delegate void OnPowerObtainedDelegate();
        private OnPowerObtainedDelegate onPowerObtainedDelegate;

        public void trigger()
        {
            if (onPowerObtainedDelegate != null)
            {
                onPowerObtainedDelegate();
            }
        }

        public void subscribe(OnPowerObtainedDelegate func)
        {
            onPowerObtainedDelegate += func;
        }

        public void unsubscribe(OnPowerObtainedDelegate func)
        {
            onPowerObtainedDelegate -= func;
        }
    }
}