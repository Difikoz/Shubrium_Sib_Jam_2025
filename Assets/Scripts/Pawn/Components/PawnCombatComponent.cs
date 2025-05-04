using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombatComponent : PawnComponent
    {
        [field: SerializeField] public AbilityPresetConfig BasicAttack { get; private set; }
        [field: SerializeField] public Transform ShootPoint { get; private set; }

        public AbilityPresetConfig CurrentAbility { get; private set; }
        public Pawn Target { get; private set; }
        public float DistanceToTarget { get; private set; }
        public float AngleToTarget { get; private set; }

        public override void UpdateComponent()
        {
            if (Target != null)
            {
                DistanceToTarget = Vector3.Distance(transform.position, Target.transform.position);
                AngleToTarget = Vector3.SignedAngle(transform.forward, (Target.transform.position - transform.position).normalized, Vector3.up);
            }
        }

        public bool PerformAttack(bool ignoreTargeting, out float waitTime)
        {
            return PerformAbility(BasicAttack, ignoreTargeting, out waitTime);
        }

        public bool PerformAbility(AbilityPresetConfig ability, bool ignoreTargeting, out float waitTime)
        {
            waitTime = 0f;
            if (CanPerformAbility(ability, ignoreTargeting))
            {
                CurrentAbility = ability;
                if (CurrentAbility.PlayAnimationOnStart)
                {
                    _pawn.Animator.SetFloat("Attack Speed", _pawn.GameplayComponent.GetGameplayStat("Attack Speed").CurrentValue);
                    _pawn.Animator.PlayAction(CurrentAbility.AnimationName);
                }
                else
                {
                    waitTime = 1f / _pawn.GameplayComponent.GetGameplayStat("Attack Speed").CurrentValue;
                    PerformAbilityCast();
                }
                return true;
            }
            return false;
        }

        private bool CanPerformAbility(AbilityPresetConfig ability, bool ignoreTargeting)
        {
            if (_pawn.GameplayComponent.HasGameplayTag("Is Perfoming Action"))
            {
                return false;
            }
            if (!ignoreTargeting)
            {
                if (DistanceToTarget >= ability.CastType.Distance)
                {
                    return false;
                }
                if (Mathf.Abs(AngleToTarget) >= ability.CastType.AngleToCast / 2f)
                {
                    return false;
                }
                if (ability.TargetType == AbilityTargetType.Target && Target == null)
                {
                    return false;
                }
            }
            return ability.CastType.CanCast(_pawn, Target, ShootPoint.position, ShootPoint.eulerAngles, ShootPoint.forward, ability.HitTypes, ability.TargetType);
        }

        public void PerformAbilityCast()
        {
            CurrentAbility.CastType.OnCast(_pawn, Target, ShootPoint.position, ShootPoint.eulerAngles, ShootPoint.forward, CurrentAbility.HitTypes, CurrentAbility.TargetType);
        }

        public void SetTarget(Pawn target)
        {
            if (target != null)
            {
                Target = target;
            }
            else
            {
                ResetTarget();
            }
        }

        public void ResetTarget()
        {
            Target = null;
        }
    }
}