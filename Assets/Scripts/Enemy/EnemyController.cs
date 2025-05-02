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
            base.FillComponents();
            Agent = GetComponent<NavMeshAgent>();
        }

        public override void EnableComponent()
        {
            base.EnableComponent();
            StartCoroutine(MoveToPlayerCoroutine());
        }

        public override void UpdateComponent()
        {
            base.UpdateComponent();
            Locomotion.MoveDirection = Agent.desiredVelocity.normalized;
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