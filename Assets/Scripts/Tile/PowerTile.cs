using UnityEngine;

using PowerModule.Events;

namespace TileController
{
    public class PowerTile : Tile
    {
        [SerializeField]
        private OnPowerObtained onPowerObtained;

        override protected void onMatchFound()
        {
            onPowerObtained.trigger();
        }
    }
}