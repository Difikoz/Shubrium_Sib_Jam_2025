using UnityEngine;

namespace WinterUniverse
{
    public abstract class AbilityHitTypeConfig : ScriptableObject
    {
        [field: SerializeField] public GameplayStatConfig StatValue { get; private set; }
        [field: SerializeField, Range(0f, 10f)] public float StatValueMultiplier { get; private set; }

        public virtual void OnHit(Pawn caster, Collider collider, Vector3 position, Vector3 direction, AbilityTargetType targetType)
        {
            Pawn target = collider.GetComponentInParent<Pawn>();
            if (target != null || collider.TryGetComponent(out target))
            {
                OnHit(caster, target, position, direction, targetType);
            }
        }

        public abstract void OnHit(Pawn caster, Pawn target, Vector3 position, Vector3 direction, AbilityTargetType targetType);

        public float GetMultipliedStatValue(Pawn caster)
        {
            return caster.GameplayComponent.GetGameplayStat(StatValue.Key).CurrentValue * StatValueMultiplier;
        }
    }
}