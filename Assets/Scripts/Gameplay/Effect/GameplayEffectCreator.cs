using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class GameplayEffectCreator
    {
        [field: SerializeField, Range(0f, 1f)] public float Chance { get; private set; }
        [field: SerializeField] public bool SingleUse { get; private set; }
        [field: SerializeField] public TriggerKeyConfig Trigger { get; private set; }
        [field: SerializeField] public GameplayEffect Effect { get; private set; }

        public bool Triggered => Random.value <= Chance;
    }
}