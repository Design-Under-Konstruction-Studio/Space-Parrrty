using UnityEngine;

using Power.Delegate;

namespace Board.Elements.Tile
{
    public class PowerTile : Tile
    {
        [SerializeField]
        private OnPowerAcquired onPowerAcquired;

        #region Implementation layer
        override protected void onMatchFound(int level)
        {
            onPowerAcquired.trigger(level);
        }
        #endregion
    }
}