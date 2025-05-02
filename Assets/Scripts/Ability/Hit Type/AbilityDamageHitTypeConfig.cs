using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Winter Universe/Ability/Hit Type/New Damage")]
    public class AbilityDamageHitTypeConfig : AbilityHitTypeConfig
    {
        [field: SerializeField] public List<DamageType> DamageTypes { get; private set; }
        [field: SerializeField] public bool UseStatToCalculate { get; private set; }
        [field: SerializeField] public GameplayStatConfig StateDamageValue { get; private set; }
        [field: SerializeField] public DamageTypeConfig StatDamageType { get; private set; }

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
            if (UseStatToCalculate && StateDamageValue != null && StatDamageType != null)
            {
                target.Health.Reduce(Mathf.RoundToInt(caster.GameplayComponent.GetGameplayStat(StateDamageValue.Key).CurrentValue), StatDamageType, caster);
            }
            else
            {
                target.Health.ApplyDamages(DamageTypes, caster);
            }
        }
    }
}