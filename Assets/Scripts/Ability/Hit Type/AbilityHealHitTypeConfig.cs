using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Heal", menuName = "Winter Universe/Ability/Hit Type/New Heal")]
    public class AbilityHealHitTypeConfig : AbilityValueBasedHitTypeConfig
    {
        [Header("Fixed Value Below")]
        [SerializeField] private string _test;
        [field: SerializeField] public int FixedValue { get; private set; }

        public override void OnHit(Pawn caster, Pawn target, Vector3 position, Vector3 eulerAngles, Vector3 direction, AbilityTargetType targetType)
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
            if (StatValue != null)
            {
                target.Health.Restore(Mathf.RoundToInt(GetMultipliedStatValue(caster)), caster);
            }
            if (FixedValue > 0)
            {
                target.Health.Restore(FixedValue, caster);
            }
        }
    }
}