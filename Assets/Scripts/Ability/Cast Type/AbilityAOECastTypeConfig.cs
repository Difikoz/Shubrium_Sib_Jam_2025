using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "AOE", menuName = "Winter Universe/Ability/Cast Type/New AOE")]
    public class AbilityAOECastTypeConfig : AbilityCastTypeConfig
    {
        public override void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 eulerAngles, Vector3 direction, List<AbilityHitTypeData> hitTypes, AbilityTargetType targetType)
        {
            Collider[] colliders = Physics.OverlapSphere(position, Distance, GameManager.StaticInstance.LayersManager.PawnMask);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Pawn pawn))
                {
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