using UnityEngine;

namespace WinterUniverse
{
    public abstract class GameplayEffectValueBased : GameplayEffect
    {
        [field: SerializeField] public GameplayStatConfig StatValue { get; private set; }
        [field: SerializeField, Range(0f, 10f)] public float StatValueMultiplier { get; private set; }
        [field: SerializeField] public int FixedValue { get; private set; }

        public float GetMultipliedStatValue(Pawn source)
        {
            return source.GameplayComponent.GetGameplayStat(StatValue.Key).CurrentValue * StatValueMultiplier;
        }
    }
}