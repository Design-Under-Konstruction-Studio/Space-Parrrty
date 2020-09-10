using UnityEngine;

using System.Collections;

using Power.Base;

using Player.Power;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Thief", menuName = "Powers/Dark/Thief", order = 1)]
    public class Thief : DarkPower
    {
        [Header("Internal state - do not assign")]
        [SerializeField]
        private bool removeDarkPowersFromOthers = false;

        [SerializeField]
        private bool removeLightPowersFromOthers = false;

        [SerializeField]
        private bool regeneratePowersAfterExecution = false;

        [Header("Scaling values - tweak for balancing")]
        [SerializeField]
        private bool[] removeDarkPowersFromOthersPerLevel;

        [SerializeField]
        private bool[] removeLightPowersFromOthersPerLevel;

        [SerializeField]
        private bool[] regeneratePowersAfterExecutionPerLevel;

        private Thief(int level)
        {
            removeDarkPowersFromOthers = removeDarkPowersFromOthersPerLevel[level];
            removeLightPowersFromOthers = removeLightPowersFromOthersPerLevel[level];

            if (!regeneratePowersAfterExecutionPerLevel[level])
            {
                RegenPowersLevel = 0;
            }
        }

        override public BasePower clone(int level)
        {
            return new Thief(level);
        }

        override public IEnumerator execute(PowerExecutor executor)
        {
            executor.losePowers(removeLightPowersFromOthers, removeDarkPowersFromOthers);
            yield return new WaitForEndOfFrame(); // Do nothing
        }
    }
}