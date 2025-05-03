using UnityEngine;

namespace WinterUniverse
{
    public abstract class AbilityValueBasedHitTypeConfig : AbilityHitTypeConfig
    {
        [field: SerializeField] public GameplayStatConfig StatValue { get; private set; }
        [field: SerializeField, Range(0f, 10f)] public float StatValueMultiplier { get; private set; }

        public float GetMultipliedStatValue(Pawn caster)
        {
            return caster.GameplayComponent.GetGameplayStat(StatValue.Key).CurrentValue * StatValueMultiplier;
        }
    }
}