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
            StartCoroutine(LifetimeCoroutine());
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

        private IEnumerator LifetimeCoroutine()
        {
            WaitForSeconds delay = new(0.5f);
            Combat.SetTarget(GameManager.StaticInstance.Player);
            while (true)
            {
                while (GameplayComponent.HasGameplayTag("Is Perfoming Action"))
                {
                    yield return null;
                }
                while (Combat.DistanceToTarget > Combat.BasicAttack.CastType.Distance)
                {
                    Agent.SetDestination(Combat.Target.transform.position);
                    yield return delay;
                }
                if (Agent.hasPath && Combat.DistanceToTarget < Combat.BasicAttack.CastType.Distance / 2f)
                {
                    Agent.ResetPath();
                }
                if (Combat.PerformAttack())
                {
                    yield return null;
                }
                yield return null;
            }
        }
    }
}