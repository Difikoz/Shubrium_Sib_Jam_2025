using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "AOE", menuName = "Winter Universe/Ability/Cast Type/New AOE")]
    public class AbilityAOECastTypeConfig : AbilityCastTypeConfig
    {
        public override void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 direction, List<AbilityHitTypeData> hitTypes, AbilityTargetType targetType)
        {

        }
    }
}