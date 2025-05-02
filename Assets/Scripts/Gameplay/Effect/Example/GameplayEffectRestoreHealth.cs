using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Restore Health", menuName = "Winter Universe/Gameplay/Effect/New Restore Health")]
    public class GameplayEffectRestoreHealth : GameplayEffect
    {
        [field: SerializeField] public int Value { get; private set; }

        public override void OnApply(Pawn owner, Pawn source)
        {
            owner.Health.Restore(Value, source);
        }
    }
}