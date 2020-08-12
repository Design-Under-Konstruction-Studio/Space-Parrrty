using UnityEngine;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Powers/Shield", order = 1)]
    public class ShieldPower : Power
    {
        #region Properties
        public float Duration
        {
            get
            {
                return durationInSeconds;
            }
            private set { }
        }

        #endregion
        [SerializeField]
        private float durationInSeconds;

        override public void onPowerReleased()
        {

        }
        override public Power clone()
        {
            return new ShieldPower(durationInSeconds);
        }

        private ShieldPower(float duration)
        {
            durationInSeconds = duration;
        }
    }
}