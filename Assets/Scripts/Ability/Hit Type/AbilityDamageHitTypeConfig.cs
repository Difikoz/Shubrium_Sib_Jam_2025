using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Winter Universe/Ability/Hit Type/New Damage")]
    public class AbilityDamageHitTypeConfig : AbilityHitTypeConfig
    {
        [field: SerializeField] public DamageTypeConfig StatDamageType { get; private set; }
        [Header("Fixed Value Below")]
        [SerializeField] private string _test;
        [field: SerializeField] public List<DamageType> FixedDamageTypes { get; private set; }

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
            if (StatValue != null && StatDamageType != null)
            {
                target.Health.Reduce(Mathf.RoundToInt(GetMultipliedStatValue(caster)), StatDamageType, caster);
            }
            if (FixedDamageTypes != null && FixedDamageTypes.Count > 0)
            {
                target.Health.ApplyDamages(FixedDamageTypes, caster);
            }
        }
    }
}