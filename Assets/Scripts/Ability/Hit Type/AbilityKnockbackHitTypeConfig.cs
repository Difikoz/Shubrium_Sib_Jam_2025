using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Knockback", menuName = "Winter Universe/Ability/Hit Type/New Knockback")]
    public class AbilityKnockbackHitTypeConfig : AbilityValueBasedHitTypeConfig
    {
        [Header("Fixed Value Below")]
        [SerializeField] private string _test;
        [field: SerializeField] public float FixedValue { get; private set; }

        public override void OnHit(Pawn caster, Pawn target, Vector3 position, Vector3 eulerAngles, Vector3 direction, AbilityTargetType targetType)
        {
            if (!CanHitTarget(caster, target, targetType, true))
            {
                return;
            }
            if (StatValue != null)
            {
                target.Locomotion.AddKnockback(direction, GetMultipliedStatValue(caster));
            }
            if (FixedValue != 0f)
            {
                target.Locomotion.AddKnockback(direction, FixedValue);
            }
        }
    }
}