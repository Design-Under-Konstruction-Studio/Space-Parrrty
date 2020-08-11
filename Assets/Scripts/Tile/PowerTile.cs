using UnityEngine;

using Power.Repository;

namespace TileController
{
    public class PowerTile : Tile
    {
        [SerializeField]
        private PowerInventory powerInventory;

        new void Start()
        {
            base.Start();
            powerInventory = board.GetComponentInParent<PowerInventory>();
        }

        override protected void onMatchFound()
        {
            powerInventory.addPowersToInventory();
        }
    }
}