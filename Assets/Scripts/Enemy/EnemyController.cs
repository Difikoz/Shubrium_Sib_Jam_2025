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
        [field: SerializeField, Range(0f, 1f)] public float ChanceToRelocate { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public bool IsRotatingToTarget { get; private set; }

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
                if (IsRotatingToTarget && Combat.DirectionToTarget != Vector3.zero)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Combat.DirectionToTarget), Locomotion.RotateSpeed * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Locomotion.GroundVelocity.normalized), Locomotion.RotateSpeed * Time.deltaTime);
                }
            }
            Agent.velocity = Locomotion.GroundVelocity * GameplayComponent.GetGameplayStat("Move Speed").CurrentValue + Locomotion.KnockbackVelocity + Locomotion.DashVelocity;
        }

        private IEnumerator LifetimeCoroutine()
        {
            WaitForSeconds delay = new(0.5f);
            Combat.SetTarget(GameManager.StaticInstance.Player);
            while (!GameplayComponent.HasGameplayTag("Is Dead"))
            {
                while (GameplayComponent.HasGameplayTag("Is Perfoming Action") || GameManager.StaticInstance.InputMode != InputMode.Game)
                {
                    yield return null;
                }
                IsRotatingToTarget = false;
                while (Combat.DistanceToTarget > Combat.BasicAttack.CastType.Distance * 0.75f)
                {
                    Agent.SetDestination(Combat.Target.transform.position);
                    yield return delay;
                }
                Agent.ResetPath();
                IsRotatingToTarget = true;
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
                if (Random.value < ChanceToRelocate)
                {
                    IsRotatingToTarget = false;
                    Agent.SetDestination(GameManager.StaticInstance.StageManager.CurrentStage.GetRandomSpawnPoint().position);
                    yield return delay;
                    while (Agent.remainingDistance > 0.1f)
                    {
                        yield return delay;
                    }
                    Agent.ResetPath();
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