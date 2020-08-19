using System.Linq;
using UnityEngine;

using PowerModule.Base;

namespace PowerModule.Repository
{
    [CreateAssetMenu(fileName = "Power Repository", menuName = "Powers/Power repository", order = 1)]
    public class PowerRepository : ScriptableObject
    {
        [SerializeField]
        private LightPower[] lightPowerPrefabs;

        [SerializeField]
        private DarkPower[] darkPowerPrefabs;

        public LightPower getRandomLightPower()
        {
            return (LightPower)lightPowerPrefabs[Random.Range(0, lightPowerPrefabs.Length)].clone();
        }

        public DarkPower getRandomDarkPower()
        {
            return (DarkPower)darkPowerPrefabs[Random.Range(0, darkPowerPrefabs.Length)].clone();
        }

        public Power checkForTestablePower()
        {
            Power returnPower = null;

            foreach (LightPower power in lightPowerPrefabs)
            {
                if (power.readyForTesting)
                {
                    if (returnPower == null)
                    {
                        returnPower = power;
                    }

                    power.readyForTesting = false;
                }
            }

            foreach (DarkPower power in darkPowerPrefabs)
            {
                if (power.readyForTesting)
                {
                    if (returnPower == null)
                    {
                        returnPower = power;
                    }

                    power.readyForTesting = false;
                }
            }

            return returnPower;
        }
    }
}