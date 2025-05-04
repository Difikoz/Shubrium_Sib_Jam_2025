using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class AbilityCastTypeConfig : ScriptableObject
    {
        [field: SerializeField, Range(0f, 25f)] public float Distance { get; private set; }
        [field: SerializeField, Range(0f, 360f)] public float AngleToCast { get; private set; }

        public virtual bool CanCast(Pawn caster, Pawn target, Vector3 position, Vector3 eulerAngles, Vector3 direction, List<AbilityHitTypeData> hitTypes, AbilityTargetType targetType)
        {
            return true;
        }

        public abstract void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 eulerAngles, Vector3 direction, List<AbilityHitTypeData> hitTypes, AbilityTargetType targetType);
    }
}