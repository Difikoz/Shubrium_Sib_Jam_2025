using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Cone", menuName = "Winter Universe/Ability/Cast Type/New Cone")]
    public class AbilityConeCastTypeConfig : AbilityCastTypeConfig
    {
        [field: SerializeField, Range(5f, 180f)] public float Angle { get; private set; }

        public override void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 direction, List<AbilityHitTypeData> hitTypes, AbilityTargetType targetType)
        {

        }
    }
}