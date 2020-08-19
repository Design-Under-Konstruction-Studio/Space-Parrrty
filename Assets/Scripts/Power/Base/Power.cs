using UnityEngine;

using Player;

namespace PowerModule.Base
{
    public abstract class Power : ScriptableObject
    {
        #region Testing only - remove before merge!
        public bool readyForTesting;
        #endregion

        #region Abstraction Layer
        public abstract void onPowerReleased(PlayerObject player);

        public abstract Power clone();
        #endregion
    }
}