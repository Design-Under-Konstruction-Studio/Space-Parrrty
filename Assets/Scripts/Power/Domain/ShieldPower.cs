using UnityEngine;
using System.Collections;

using Player;

namespace PowerModule.Domain
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Powers/Shield", order = 1)]
    public class ShieldPower : Power
    {

        [SerializeField]
        private float durationInSeconds;

        override public void onPowerReleased(PlayerObject player)
        {
            player.StartCoroutine(enableShieldCR(player));
        }
        override public Power clone()
        {
            return new ShieldPower(durationInSeconds);
        }

        private ShieldPower(float duration)
        {
            durationInSeconds = duration;
        }

        private IEnumerator enableShieldCR(PlayerObject player)
        {
            player.turnOnShield(true);
            yield return new WaitForSeconds(durationInSeconds);
            player.turnOnShield(false);
        }
    }
}