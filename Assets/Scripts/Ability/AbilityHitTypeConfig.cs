using UnityEngine;

namespace WinterUniverse
{
    public abstract class AbilityHitTypeConfig : ScriptableObject
    {
        public virtual void OnHit(Pawn caster, Collider collider, Vector3 position, Vector3 direction, AbilityTargetType targetType)
        {
            Pawn target = collider.GetComponentInParent<Pawn>();
            if (target != null || collider.TryGetComponent(out target))
            {
                OnHit(caster, target, position, direction, targetType);
            }
        }

        public abstract void OnHit(Pawn caster, Pawn target, Vector3 position, Vector3 direction, AbilityTargetType targetType);
    }
}