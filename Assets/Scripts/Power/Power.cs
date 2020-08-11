using UnityEngine;

using Power.Enum;

namespace Power
{
    public abstract class Power : ScriptableObject
    {
        #region Properties
        public PowerAlignment Alignment
        {
            get
            {
                return powerAlignment;
            }
            private set { }
        }
        #endregion

        [SerializeField]
        private PowerAlignment powerAlignment;

        #region Abstraction Layer
        public abstract void onPowerGained();
        public abstract void onPowerLost();
        public abstract void onPowerReleased();
        public abstract Power clone();
        #endregion
    }
}