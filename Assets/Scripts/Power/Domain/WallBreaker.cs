using UnityEngine;

using System.Collections;

using Power.Base;

using Player.Power;

using TileController.Base;

namespace Power.Domain
{
    // [CreateAssetMenu(fileName = "WallBreaker", menuName = "Powers/Light/WallBreaker", order = 1)]
    // public class WallBreaker : LightPower
    // {
    //     [Header("Internal state - do not assign")]
    //     [SerializeField]
    //     private int breakableWalls;

    //     [Header("Scaling values - tweak for balancing")]
    //     [SerializeField]
    //     private int[] breakableWallsPerLevel = { 1, 2, 3 };

    //     [SerializeField]
    //     private float delayBetweenBreakingWalls = 1.0f;

    //     private WallBreaker(int level)
    //     {
    //         breakableWalls = breakableWallsPerLevel[level];
    //     }

    //     override public BasePower clone(int level)
    //     {
    //         return new WallBreaker(level);
    //     }

    //     override public IEnumerator execute(PowerExecutor executor)
    //     {
    //         // int bottommostWallIndex = executor.getTopmostWallIndex();
    //         // for (int breakableWallsCount = 0; breakableWallsCount < breakableWalls; breakableWallsCount++)
    //         // {
    //         //     executor.destroyWall(topmostWallIndex + breakableWallsCount);
    //         //     yield return new WaitForSeconds(delayBetweenBreakingWalls);
    //         // }
    //     }
    // }
}