using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : Pawn
    {
        public NavMeshAgent Agent { get; private set; }

        public override void FillComponents()
        {
            Agent = GetComponent<NavMeshAgent>();
            base.FillComponents();
        }

        public override void EnableComponent()
        {
            base.EnableComponent();
            StartCoroutine(MoveToPlayerCoroutine());
        }

        public override void UpdateComponent()
        {
            Locomotion.MoveDirection = (Agent.steeringTarget - transform.position).normalized;
            base.UpdateComponent();
            if (Locomotion.GroundVelocity != Vector3.zero)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Locomotion.GroundVelocity.normalized), Locomotion.RotateSpeed * Time.deltaTime);
            }
            Agent.velocity = Locomotion.GroundVelocity * GameplayComponent.GetGameplayStat("Move Speed").CurrentValue + Locomotion.KnockbackVelocity + Locomotion.DashVelocity;
        }

        private IEnumerator MoveToPlayerCoroutine()
        {
            WaitForSeconds delay = new(0.25f);
            while (true)
            {
                Agent.SetDestination(GameManager.StaticInstance.Player.transform.position);
                yield return delay;
            }
        }
    }
}