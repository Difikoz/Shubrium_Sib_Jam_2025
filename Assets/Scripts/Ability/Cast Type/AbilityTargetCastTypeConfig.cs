using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Target", menuName = "Winter Universe/Ability/Cast Type/New Target")]
    public class AbilityTargetCastTypeConfig : AbilityCastTypeConfig
    {
        public override void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 direction, List<AbilityHitTypeData> hitTypes, AbilityTargetType targetType)
        {

        }
    }
}