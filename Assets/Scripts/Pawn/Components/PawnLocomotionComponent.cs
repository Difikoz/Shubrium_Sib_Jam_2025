using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnLocomotionComponent : PawnComponent
    {
        public Vector3 MoveDirection;
        public Vector3 GroundVelocity { get; private set; }
        public Vector3 KnockbackVelocity { get; private set; }
        public Vector3 DashVelocity { get; private set; }
        [field: SerializeField, Range(10f, 720f)] public float RotateSpeed { get; private set; }
        [field: SerializeField, Range(1f, 100f)] public float Mass { get; private set; }
        [field: SerializeField, Range(0.1f, 0.5f)] public float TimeToDash { get; private set; }

        private Coroutine _dashCoroutine;

        public override void ActivateComponent()
        {
            base.ActivateComponent();
            _dashCoroutine = null;
        }

        public override void UpdateComponent()
        {
            if (_pawn.GameplayComponent.HasGameplayTag("Is Moving"))
            {
                if (MoveDirection == Vector3.zero || DashVelocity != Vector3.zero || KnockbackVelocity != Vector3.zero || _pawn.GameplayComponent.HasGameplayTag("Is Perfoming Action"))
                {
                    _pawn.GameplayComponent.RemoveGameplayTag("Is Moving");
                    _pawn.Animator.SetBool("Is Moving", false);
                }
                else
                {
                    GroundVelocity = Vector3.MoveTowards(GroundVelocity, MoveDirection, 2f * Time.deltaTime);
                }
            }
            else
            {
                if (MoveDirection != Vector3.zero && DashVelocity == Vector3.zero && KnockbackVelocity == Vector3.zero && !_pawn.GameplayComponent.HasGameplayTag("Is Perfoming Action"))
                {
                    _pawn.GameplayComponent.AddGameplayTag("Is Moving");
                    _pawn.Animator.SetBool("Is Moving", true);
                }
                else
                {
                    GroundVelocity = Vector3.MoveTowards(GroundVelocity, Vector3.zero, 4f * Time.deltaTime);
                    if (KnockbackVelocity != Vector3.zero)
                    {
                        KnockbackVelocity = Vector3.MoveTowards(KnockbackVelocity, Vector3.zero, Mass * Time.deltaTime);
                    }
                }
            }
        }

        public void AddKnockback(Vector3 direction, float force)
        {
            if (DashVelocity != Vector3.zero)
            {
                return;
            }
            KnockbackVelocity += direction.normalized * force;
        }

        public void PerformDash()
        {
            if (_dashCoroutine != null || _pawn.GameplayComponent.HasGameplayTag("Is Perfoming Action"))
            {
                return;
            }
            DashVelocity = transform.forward * _pawn.GameplayComponent.GetGameplayStat("Dash Force").CurrentValue / TimeToDash;
            KnockbackVelocity = Vector3.zero;
            GroundVelocity = Vector3.zero;
            _dashCoroutine = StartCoroutine(DashCoroutine());
        }

        private IEnumerator DashCoroutine()
        {
            yield return new WaitForSeconds(TimeToDash);
            DashVelocity = Vector3.zero;
            yield return new WaitForSeconds(_pawn.GameplayComponent.GetGameplayStat("Dash Cooldown").CurrentValue);
            _dashCoroutine = null;
        }
    }
}