using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : Pawn
    {
        public Rigidbody RB { get; private set; }

        public override void FillComponents()
        {
            RB = GetComponent<Rigidbody>();
            base.FillComponents();
        }

        public override void FixedUpdateComponent()
        {
            base.FixedUpdateComponent();
            if (Locomotion.GroundVelocity != Vector3.zero)
            {
                RB.rotation = Quaternion.RotateTowards(RB.rotation, Quaternion.LookRotation(Locomotion.GroundVelocity.normalized), Locomotion.RotateSpeed * Time.fixedDeltaTime);
            }
            RB.linearVelocity = Locomotion.GroundVelocity * GameplayComponent.GetGameplayStat("Move Speed").CurrentValue + Locomotion.KnockbackVelocity + Locomotion.DashVelocity;
        }

        public override IEnumerator PerformDeath()
        {
            yield return new WaitForSeconds(1f);
            GameManager.StaticInstance.GameOver();
        }
    }
}