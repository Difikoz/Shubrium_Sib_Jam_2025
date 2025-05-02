using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Restore Health", menuName = "Winter Universe/Gameplay/Effect/New Restore Health")]
    public class GameplayEffectRestoreHealth : GameplayEffect
    {
        [field: SerializeField] public GameplayStatConfig StatValue { get; private set; }
        [field: SerializeField, Range(0f, 10f)] public float StatValueMultiplier { get; private set; }
        [field: SerializeField] public int FixedValue { get; private set; }

        public override void OnApply(Pawn owner, Pawn source)
        {
            if (StatValue != null)
            {
                owner.Health.Restore(Mathf.RoundToInt(GetMultipliedStatValue(source)), source);
            }
            if (FixedValue != 0)
            {
                owner.Health.Restore(FixedValue, source);
            }
        }

        public float GetMultipliedStatValue(Pawn source)
        {
            return source.GameplayComponent.GetGameplayStat(StatValue.Key).CurrentValue * StatValueMultiplier;
        }
    }
}