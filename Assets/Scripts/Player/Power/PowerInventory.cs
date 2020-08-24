using UnityEngine;

using Power.Base;

using TileController.Power;

namespace Player.Power
{
    [System.Serializable]
    public class PowerInventory
    {
        #region Properties
        public LightPower LightPower
        {
            get => lightPower;
            private set => lightPower = value;
        }

        public DarkPower DarkPower
        {
            get => darkPower;
            private set => darkPower = value;
        }
        #endregion

        #region Power Gallery
        [Header("Power gallery - assign these")]
        [SerializeField]
        private LightPower[] lightPowerRepository;

        [SerializeField]
        private DarkPower[] darkPowerRepository;
        #endregion

        #region Events & delegates
        [Header("Events & delegates - assign these")]
        [SerializeField]
        private OnPowerAcquired onPowerAcquired;
        #endregion 

        #region Obtained powers
        [Header("Obtained powers - do not assign")]
        [SerializeField]
        private LightPower lightPower;

        [SerializeField]
        private DarkPower darkPower;
        #endregion

        public void setup()
        {
            onPowerAcquired.subscribe(generatePowers);
        }

        public void dismantle()
        {
            onPowerAcquired.unsubscribe(generatePowers);
        }

        public void generatePowers(int level)
        {
            level = Mathf.Max(level, 0);
            level = Mathf.Min(level, 2);

            lightPower = (LightPower)lightPowerRepository[Random.Range(0, lightPowerRepository.Length - 1)].clone(level);
            darkPower = (DarkPower)darkPowerRepository[Random.Range(0, darkPowerRepository.Length - 1)].clone(level);
        }

        public void clean()
        {
            lightPower = null;
            darkPower = null;
        }
    }
}