using UnityEngine;

namespace WinterUniverse
{
    public class PawnLocomotionComponent : PawnComponent
    {
        public Vector3 MoveDirection;
        public Vector3 GroundVelocity { get; private set; }
        public Vector3 FallVelocity { get; private set; }
        public Vector3 KnockbackVelocity { get; private set; }

        public override void UpdateComponent()
        {
            if (MoveDirection != Vector3.zero)
            {
                GroundVelocity = Vector3.MoveTowards(GroundVelocity, MoveDirection, 2f * Time.deltaTime);
            }
            else
            {
                GroundVelocity = Vector3.MoveTowards(GroundVelocity, Vector3.zero, 4f * Time.deltaTime);
            }
            if (KnockbackVelocity != Vector3.zero)
            {
                KnockbackVelocity = Vector3.MoveTowards(KnockbackVelocity, Vector3.zero, _pawn.RB.mass * Time.deltaTime);
            }
        }

        public override void FixedUpdateComponent()
        {
            _pawn.RB.linearVelocity = GroundVelocity + FallVelocity + KnockbackVelocity;
        }
    }
}