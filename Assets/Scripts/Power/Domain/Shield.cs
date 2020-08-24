using UnityEngine;

using System.Collections;

using Power.Base;

using Player.Power;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Powers/Light/Shield", order = 1)]
    public class Shield : LightPower
    {
        [SerializeField]
        private float duration;

        [SerializeField]
        private float[] durationPerLevel = { 3, 5, 10 };

        private Shield(int level)
        {
            duration = durationPerLevel[level];
        }

        override public BasePower clone(int level)
        {
            return new Shield(level);
        }

        override public IEnumerator execute(PowerExecutor executor)
        {
            executor.switchShield(true);
            yield return new WaitForSeconds(duration);
            executor.switchShield(false);
        }
    }
}