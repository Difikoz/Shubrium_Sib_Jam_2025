using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Additional Ability", menuName = "Winter Universe/Ability/Hit Type/New Additional Ability")]
    public class AbilityAdditionalAbilityHitTypeConfig : AbilityHitTypeConfig
    {
        [field: SerializeField] public AbilityPresetConfig AdditionalAbility { get; private set; }

        public override void OnHit(Pawn caster, Collider collider, Vector3 position, Vector3 eulerAngles, Vector3 direction, AbilityTargetType targetType)
        {
            AdditionalAbility.CastType.OnCast(caster, null, position, eulerAngles, direction, AdditionalAbility.HitTypes, targetType);
        }

        public override void OnHit(Pawn caster, Pawn target, Vector3 position, Vector3 eulerAngles, Vector3 direction, AbilityTargetType targetType)
        {
            AdditionalAbility.CastType.OnCast(caster, target, position, eulerAngles, direction, AdditionalAbility.HitTypes, targetType);
        }
    }
}