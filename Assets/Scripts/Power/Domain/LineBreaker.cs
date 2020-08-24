using UnityEngine;

using System.Collections;

using Power.Base;

using Player.Power;

using TileController.Base;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "LineBreaker", menuName = "Powers/Light/LineBreaker", order = 1)]
    public class LineBreaker : LightPower
    {
        [SerializeField]
        private int breakableLines;

        [SerializeField]
        private int[] breakableLinesPerLevel = { 1, 2, 3 };

        private LineBreaker(int level)
        {
            breakableLines = breakableLinesPerLevel[level];
        }

        override public BasePower clone(int level)
        {
            return new LineBreaker(level);
        }

        override public IEnumerator execute(PowerExecutor executor)
        {
            executor.destroyTopmostLines(breakableLines);
            yield return new WaitForSeconds(0);
        }
    }
}