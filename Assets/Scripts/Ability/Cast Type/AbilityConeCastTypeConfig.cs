using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Cone", menuName = "Winter Universe/Ability/Cast Type/New Cone")]
    public class AbilityConeCastTypeConfig : AbilityCastTypeConfig
    {
        public override void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 direction, List<AbilityHitTypeData> hitTypes, AbilityTargetType targetType)
        {

        }
    }
}