using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Winter Universe/Ability/Hit Type/New Damage")]
    public class AbilityDamageHitTypeConfig : AbilityHitTypeConfig
    {
        [field: SerializeField] public List<DamageType> DamageTypes { get; private set; }

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
            // target.ApplyDamages(DamageTypes, caster);
        }
    }
}