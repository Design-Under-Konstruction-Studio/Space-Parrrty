using UnityEngine;
using UnityEngine.InputSystem;

using Power.Repository;
using Power.Events;
using Power.Enum;
using Power.Domain;

namespace Player
{
    public class PlayerObject : MonoBehaviour
    {
        [Header("Assign these to Scriptables and Prefabs")]
        [SerializeField]
        private PowerRepository powerRepository;

        [SerializeField]
        private OnPowerObtained onPowerObtained;

        [Header("Do not assign these")]
        [SerializeField]
        private PlayerStats stats;

        [SerializeField]
        private PowerInventory powerInventory;

        private void Awake()
        {
            onPowerObtained.subscribe(addPowersToInventory);
        }

        private void OnDestroy()
        {
            onPowerObtained.unsubscribe(addPowersToInventory);
        }

        private void addPowersToInventory()
        {
            powerInventory.addPowersToInventory(
                powerRepository.getRandomPowerByAlignment(PowerAlignment.LightPower),
                powerRepository.getRandomPowerByAlignment(PowerAlignment.DarkPower));
        }

        public void useLightPower()
        {
            powerInventory.usePower(PowerAlignment.LightPower, this);
        }

        public void useDarkPower()
        {
            powerInventory.usePower(PowerAlignment.LightPower, this);
        }

        private void turnOnShield(bool turnOnShield)
        {
            stats.HasShield = turnOnShield;
        }
    }
}