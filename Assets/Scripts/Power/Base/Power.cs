using UnityEngine;

using System.Collections;

using Player.Power;

namespace Power.Base
{
    public abstract class BasePower : ScriptableObject
    {
        public int RegenPowersLevel
        {
            get => regenPowersLevel;
            protected set => regenPowersLevel = value;
        }

        [Header("Level of powers that will be generated after power execution")]
        [Tooltip("Set to 0 so no powers are generated")]
        [SerializeField]
        private int regenPowersLevel = 0;

        public abstract BasePower clone(int level);

        public abstract IEnumerator execute(PowerExecutor executor);
    }
}