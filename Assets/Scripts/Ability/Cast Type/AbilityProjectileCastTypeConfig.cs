using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Winter Universe/Ability/Cast Type/New Projectile")]
    public class AbilityProjectileCastTypeConfig : AbilityCastTypeConfig
    {
        [field: SerializeField] public bool Homing { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Count { get; private set; }

        public override void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 direction, List<AbilityHitTypeData> hitTypes, AbilityTargetType targetType)
        {

        }
    }
}