using UnityEngine;

using Power.Repository;

namespace Player
{
    [System.Serializable]
    public class PlayerStats
    {
        #region Properties
        public bool HasShield
        {
            get => hasShield;
            set => hasShield = value;
        }
        #endregion

        [SerializeField]
        private bool hasShield;
    }
}