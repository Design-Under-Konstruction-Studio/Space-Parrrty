using UnityEngine;

using System.Collections;

using Power.Base;

using Player.Power;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Freeze", menuName = "Powers/Dark/Freeze", order = 1)]
    public class Freeze : DarkPower
    {
        [Header("Internal state - do not assign")]
        [SerializeField]
        private float duration;

        [Header("Scaling values - tweak for balancing")]
        [SerializeField]
        private float[] durationPerLevel = { 3, 5, 10 };

        private Freeze(int level)
        {
            duration = durationPerLevel[level];
        }

        override public BasePower clone(int level)
        {
            return new Freeze(level);
        }

        override public IEnumerator execute(PowerExecutor executor)
        {
            executor.switchFreeze(true);
            yield return new WaitForSeconds(duration);
            executor.switchFreeze(false);
        }
    }
}