using UnityEngine;
using System.Collections;

using PowerModule.Repository;
using PowerModule.Base;

using Player;

namespace PowerModule.Test
{
    public class PowerTester : MonoBehaviour
    {
        [SerializeField]
        private PowerRepository powerRepository;

        [SerializeField]
        private PlayerHub playerHub;

        private Power testPower;
        private void Update()
        {
            testPower = powerRepository.checkForTestablePower();
            if (testPower != null)
            {
                StartCoroutine(testPowerCR());
            }
        }

        private IEnumerator testPowerCR()
        {
            playerHub.testPowerOnPlayer(testPower);
            yield return new WaitForSeconds(5);
            testPower = null;
        }
    }
}