using System.Collections;
using UnityEngine;
using Power.Base;
using Player.Power;

namespace Power.Domain
{
    [CreateAssetMenu(fileName = "Saboteur", menuName = "Powers/Dark/Saboteur", order = 1)]
    public class Saboteur : DarkPower
    {
        [Header("Internal state - do not assign")]
        [SerializeField]
        private int quantityObstacleLimit = 2;
        private int quantityOfObstacle;
        
        [Header("Scaling values - tweak for balancing")]
        [SerializeField]
        private int[] quantityObstaclePerLevel = { 1, 2 };
        private Saboteur(int level)
        {
            quantityOfObstacle = quantityObstaclePerLevel[level];
        }

        override public BasePower clone(int level)
        {
            return new Saboteur(level);
        }

        override public IEnumerator execute(PowerExecutor executor)
        {
            for(int i = 0; i < quantityOfObstacle; i++) {
                yield return new WaitForSeconds(1);
                executor.createObstacle();
            }
        }
    }
}
