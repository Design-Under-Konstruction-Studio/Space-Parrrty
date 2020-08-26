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

        [SerializeField]
        private float delayBetweenBreakingLines = 1.0f;

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
            int topmostLineIndex = executor.getTopmostLineIndex();
            for (int breakableLinesCount = 0; breakableLinesCount < breakableLines; breakableLinesCount++)
            {
                executor.destroyLine(topmostLineIndex + breakableLinesCount);
                yield return new WaitForSeconds(delayBetweenBreakingLines);
            }
        }
    }
}