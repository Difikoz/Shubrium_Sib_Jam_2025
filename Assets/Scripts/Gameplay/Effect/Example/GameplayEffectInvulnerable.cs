using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Invulnerable", menuName = "Winter Universe/Gameplay/Effect/New Invulnerable")]
    public class GameplayEffectInvulnerable : GameplayEffect
    {
        [field: SerializeField] public float Duration { get; private set; }

        public override void OnApply(Pawn owner, Pawn source)
        {
            owner.Health.AddInvulnerable(Duration);
        }
    }
}