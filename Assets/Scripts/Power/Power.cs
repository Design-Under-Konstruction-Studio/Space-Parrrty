using UnityEngine;

using Power.Enum;
using Player;

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
        public abstract void onPowerReleased(PlayerObject player);

        public abstract Power clone();
        #endregion
    }
}