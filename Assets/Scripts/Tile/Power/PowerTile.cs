using UnityEngine;

using TileController.Base;

namespace TileController.Power
{
    public class PowerTile : Tile
    {
        [SerializeField]
        private OnPowerAcquired onPowerAcquired;

        override protected void onMatchFound(int level)
        {
            onPowerAcquired.trigger(level);
        }
    }
}