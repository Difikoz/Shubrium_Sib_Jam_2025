using Lean.Pool;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : Pawn
    {
        [field: SerializeField] public GameObject ExplosionEffect { get; private set; }
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
            if (!GameplayComponent.HasGameplayTag("Is Perfoming Action")) //(Locomotion.GroundVelocity != Vector3.zero)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Locomotion.GroundVelocity.normalized), Locomotion.RotateSpeed * Time.deltaTime);
            }
            Agent.velocity = Locomotion.GroundVelocity * GameplayComponent.GetGameplayStat("Move Speed").CurrentValue + Locomotion.KnockbackVelocity + Locomotion.DashVelocity;
        }

        private IEnumerator LifetimeCoroutine()
        {
            WaitForSeconds delay = new(0.5f);
            Combat.SetTarget(GameManager.StaticInstance.Player);
            while (!GameplayComponent.HasGameplayTag("Is Dead"))
            {
                while (GameManager.StaticInstance.InputMode != InputMode.Game)
                {
                    yield return null;
                }
                while (GameplayComponent.HasGameplayTag("Is Perfoming Action"))
                {
                    yield return null;
                }
                if (Combat.DistanceToTarget > Combat.BasicAttack.CastType.Distance)
                {
                    Agent.SetDestination(Combat.Target.transform.position);
                    yield return delay;
                }
                if (Agent.hasPath && Combat.DistanceToTarget < Combat.BasicAttack.CastType.Distance / 2f)
                {
                    Agent.ResetPath();
                }
                if (Combat.PerformAttack(false, out float waitTime))
                {
                    if (waitTime > 0f)
                    {
                        yield return new WaitForSeconds(waitTime);
                    }
                    else
                    {
                        yield return null;
                    }
                }
                yield return null;
            }
        }

        public override IEnumerator PerformDeath()
        {
            LeanPool.Despawn(LeanPool.Spawn(ExplosionEffect, transform.position, Quaternion.identity), 10f);
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
}