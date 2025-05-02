using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Heal", menuName = "Winter Universe/Ability/Hit Type/New Heal")]
    public class AbilityHealHitTypeConfig : AbilityHitTypeConfig
    {
        [field: SerializeField] public int Value { get; private set; }

        public override void OnHit(Pawn caster, Pawn target, Vector3 position, Vector3 direction, AbilityTargetType targetType)
        {
            switch (targetType)
            {
                case AbilityTargetType.Caster:
                    if (caster != target)
                    {
                        return;
                    }
                    break;
                case AbilityTargetType.Target:
                    if (caster == target)
                    {
                        return;
                    }
                    break;
                case AbilityTargetType.All:
                    // NICE =)
                    break;
            }
            target.Health.Restore(Value, caster);
        }
    }
}