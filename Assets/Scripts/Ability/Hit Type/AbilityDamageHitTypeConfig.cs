using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Winter Universe/Ability/Hit Type/New Damage")]
    public class AbilityDamageHitTypeConfig : AbilityValueBasedHitTypeConfig
    {
        [field: SerializeField] public DamageTypeConfig StatDamageType { get; private set; }
        [Header("Fixed Value Below")]
        [SerializeField] private string _test;
        [field: SerializeField] public List<DamageType> FixedDamageTypes { get; private set; }

        public override void OnHit(Pawn caster, Pawn target, Vector3 position, Vector3 eulerAngles, Vector3 direction, AbilityTargetType targetType)
        {
            if (!CanHitTarget(caster, target, targetType, true))
            {
                return;
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