using UnityEngine;

namespace WinterUniverse
{
    public abstract class AbilityHitTypeConfig : ScriptableObject
    {
        public virtual void OnHit(Pawn caster, Collider collider, Vector3 position, Vector3 eulerAngles, Vector3 direction, AbilityTargetType targetType)
        {
            Pawn target = collider.GetComponentInParent<Pawn>();
            if (target != null || collider.TryGetComponent(out target))
            {
                OnHit(caster, target, position, eulerAngles, direction, targetType);
            }
        }

        public abstract void OnHit(Pawn caster, Pawn target, Vector3 position, Vector3 eulerAngles, Vector3 direction, AbilityTargetType targetType);

        public bool CanHitTarget(Pawn caster, Pawn target, AbilityTargetType targetType, bool checkFaction)
        {
            switch (targetType)
            {
                case AbilityTargetType.Caster:
                    if (caster != target)
                    {
                        return false;
                    }
                    break;
                case AbilityTargetType.Target:
                    if (caster == target)
                    {
                        return false;
                    }
                    if (checkFaction && caster.Faction == target.Faction)
                    {
                        return false;
                    }
                    break;
                case AbilityTargetType.All:
                    return true;
                case AbilityTargetType.OnlyPlayer:
                    if (target.Faction == Faction.Enemy)
                    {
                        return false;
                    }
                    break;
                case AbilityTargetType.OnlyEnemy:
                    if (target.Faction == Faction.Ally)
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }
    }
}