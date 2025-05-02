using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class AbilityHitTypeData
    {
        [field: SerializeField] public AbilityHitTypeConfig HitType { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float ChanceToTrigger { get; private set; }

        public bool Triggered => Random.value < ChanceToTrigger;
    }
}