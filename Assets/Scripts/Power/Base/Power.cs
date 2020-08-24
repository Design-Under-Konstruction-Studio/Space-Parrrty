using UnityEngine;

using System.Collections;

using Player.Power;

namespace Power.Base
{
    public abstract class BasePower : ScriptableObject
    {
        public abstract BasePower clone(int level);

        public abstract IEnumerator execute(PowerExecutor executor);
    }
}