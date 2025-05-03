using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Cone", menuName = "Winter Universe/Ability/Cast Type/New Cone")]
    public class AbilityConeCastTypeConfig : AbilityCastTypeConfig
    {
        [field: SerializeField, Range(5f, 180f)] public float Angle { get; private set; }

        public override void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 eulerAngles, Vector3 direction, List<AbilityHitTypeData> hitTypes, AbilityTargetType targetType)
        {
            Collider[] colliders = Physics.OverlapSphere(position, Distance, GameManager.StaticInstance.LayersManager.PawnMask);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Pawn pawn))
                {
                    if (Mathf.Abs(Vector3.SignedAngle(direction, (pawn.transform.position - position).normalized, Vector3.up)) > Angle / 2f)
                    {
                        continue;
                    }
                    foreach (AbilityHitTypeData hitTypeData in hitTypes)
                    {
                        if (hitTypeData.Triggered)
                        {
                            hitTypeData.HitType.OnHit(caster, pawn, position, eulerAngles, direction, targetType);
                        }
                    }
                }
            }
        }
    }
}