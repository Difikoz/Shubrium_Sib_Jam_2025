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
        [field: SerializeField, Range(1f, 100f)] public float Mass { get; private set; }
        [field: SerializeField, Range(0.1f, 0.5f)] public float TimeToDash { get; private set; }

        private Coroutine _dashCoroutine;

        public override void FixedUpdateComponent()
        {
            if (MoveDirection != Vector3.zero)
            {
                GroundVelocity = Vector3.MoveTowards(GroundVelocity, MoveDirection, 2f * Time.fixedDeltaTime);
            }
            else
            {
                GroundVelocity = Vector3.MoveTowards(GroundVelocity, Vector3.zero, 4f * Time.fixedDeltaTime);
            }
            if (KnockbackVelocity != Vector3.zero)
            {
                KnockbackVelocity = Vector3.MoveTowards(KnockbackVelocity, Vector3.zero, Mass * Time.fixedDeltaTime);
            }
            _pawn.RB.linearVelocity = GroundVelocity * _pawn.GameplayComponent.GetGameplayStat("Move Speed").CurrentValue + KnockbackVelocity + DashVelocity;
        }

        public void AddKnockback(Vector3 direction, float force)
        {
            KnockbackVelocity += direction.normalized * force;
        }

        public void PerformDash()
        {
            if (_dashCoroutine != null)
            {
                return;
            }
            DashVelocity = transform.forward * _pawn.GameplayComponent.GetGameplayStat("Dash Force").CurrentValue;
            _dashCoroutine = StartCoroutine(DashCoroutine());
        }

        private IEnumerator DashCoroutine()
        {
            yield return new WaitForSeconds(TimeToDash);
            DashVelocity = Vector3.zero;
            _dashCoroutine = null;
        }
    }
}