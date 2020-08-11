using System.Linq;
using UnityEngine;

using Power.Enum;

namespace Power.Repository
{
    public class PowerInventory : MonoBehaviour
    {
        [SerializeField]
        private PowerRepository powerRepository;

        [SerializeField]
        private Power[] powers = new Power[2];

        public void addPowersToInventory()
        {
            addPowersToInventory(
                powerRepository.getRandomPowerByAlignment(PowerAlignment.LightPower),
                powerRepository.getRandomPowerByAlignment(PowerAlignment.DarkPower));
        }

        private void addPowersToInventory(Power lightPower, Power darkPower)
        {
            if (lightPower.Alignment == PowerAlignment.LightPower)
            {
                powers[0] = lightPower;
                powers[0].onPowerGained();
            }

            if (darkPower.Alignment == PowerAlignment.DarkPower)
            {
                powers[1] = darkPower;
                powers[1].onPowerGained();
            }
        }
    }
}