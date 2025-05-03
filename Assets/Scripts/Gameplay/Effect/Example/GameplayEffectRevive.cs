using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Revive", menuName = "Winter Universe/Gameplay/Effect/New Revive")]
    public class GameplayEffectRevive : GameplayEffect
    {
        public override void OnApply(Pawn owner, Pawn source)
        {
            owner.Health.Revive(source);
        }
    }
}