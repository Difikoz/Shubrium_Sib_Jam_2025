using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Restore Health", menuName = "Winter Universe/Gameplay/Effect/New Restore Health")]
    public class GameplayEffectRestoreHealth : GameplayEffectValueBased
    {
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
    }
}