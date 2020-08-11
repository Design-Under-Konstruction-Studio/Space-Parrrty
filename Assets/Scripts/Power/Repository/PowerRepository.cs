using System.Linq;
using UnityEngine;

using Power.Enum;

namespace Power.Repository
{
    [CreateAssetMenu(fileName = "Power Repository", menuName = "Powers/Power repository", order = 1)]
    public class PowerRepository : ScriptableObject
    {
        [SerializeField]
        private Power[] powerPrefabs;

        public Power getRandomPowerByAlignment(PowerAlignment alignment)
        {
            Power[] alignedPowers = powerPrefabs.Where(power => power.Alignment == alignment).ToArray();
            return alignedPowers[Random.Range(0, alignedPowers.Length)].clone();
        }
    }
}