using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Winter Universe/Ability/Cast Type/New Projectile")]
    public class AbilityProjectileCastTypeConfig : AbilityCastTypeConfig
    {
        public override void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 direction, List<AbilityHitTypeConfig> hitTypes, AbilityTargetType targetType)
        {

        }
    }
}