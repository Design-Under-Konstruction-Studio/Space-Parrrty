using UnityEngine;

using System.Collections;

using Power.Base;

using Player.Power;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Haste", menuName = "Powers/Dark/Haste", order = 1)]
    public class Haste : DarkPower
    {
        [Header("Idle time spent before checking if all the new line are already generated")]
        [SerializeField]
        private float stopAccelerationCheckDelay = 0.3f;

        [Header("Internal state - do not assign")]
        [SerializeField]
        private float speedMultiplier;

        [SerializeField]
        private int newLineLimit;

        [Header("Scaling values - tweak for balancing")]
        [SerializeField]
        private float[] speedMultiplierPerLevel = { 2, 4, 6 };

        [SerializeField]
        private int[] newLineLimitPerLevel = { 1, 2, 3 };

        private Haste(int level)
        {
            speedMultiplier = speedMultiplierPerLevel[level];
            newLineLimit = newLineLimitPerLevel[level];
        }

        override public BasePower clone(int level)
        {
            return new Haste(level);
        }

        override public IEnumerator execute(PowerExecutor executor)
        {
            int originalNumberOfLines = executor.accelerateBoard(speedMultiplier);
            do
            {
                yield return new WaitForSeconds(stopAccelerationCheckDelay);
            } while (!executor.boardReachedHasteLimit(originalNumberOfLines, newLineLimit));
            executor.retardBoard(speedMultiplier);
        }
    }
}