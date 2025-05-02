using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class AbilityCastTypeConfig : ScriptableObject
    {
        public virtual bool CanCast(Pawn caster, Pawn target, Vector3 position, Vector3 direction, List<AbilityHitTypeConfig> hitTypes, AbilityTargetType targetType)
        {
            if (targetType == AbilityTargetType.Target && target == null)
            {
                return false;
            }
            return true;
        }

        public abstract void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 direction, List<AbilityHitTypeConfig> hitTypes, AbilityTargetType targetType);
    }
}