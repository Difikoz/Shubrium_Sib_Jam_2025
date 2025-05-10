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

        public override void EnableComponent()
        {
            base.EnableComponent();
            //StartCoroutine(FindClosestEnemy());
        }

        public override void FixedUpdateComponent()
        {
            base.FixedUpdateComponent();
            if (GameplayComponent.HasGameplayTag("Is Perfoming Action"))// костыль, по хорошему добавлять тег при атаке и дэше и чекать их наличие, как вариант
            {
                RB.rotation = Quaternion.RotateTowards(RB.rotation, Quaternion.LookRotation(Locomotion.LookDirection.normalized), Locomotion.RotateSpeed * Time.fixedDeltaTime);
            }
            else if (Locomotion.GroundVelocity != Vector3.zero)
            {
                RB.rotation = Quaternion.RotateTowards(RB.rotation, Quaternion.LookRotation(Locomotion.GroundVelocity.normalized), Locomotion.RotateSpeed * Time.fixedDeltaTime);
            }
            RB.linearVelocity = Locomotion.TotalVelocity;
        }

        private IEnumerator FindClosestEnemy()
        {
            WaitForSeconds delay = new(0.25f);
            EnemyController[] enemies;
            EnemyController closestEnemy;
            float distance;
            float maxDistance;
            while (true)
            {
                enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
                maxDistance = float.MaxValue;
                closestEnemy = null;
                foreach (EnemyController enemy in enemies)
                {
                    distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < maxDistance && enemy.isActiveAndEnabled)
                    {
                        maxDistance = distance;
                        closestEnemy = enemy;
                    }
                }
                Combat.SetTarget(closestEnemy);
                yield return delay;
            }
        }

        public override IEnumerator PerformDeath()
        {
            yield return new WaitForSeconds(1f);
            GameManager.StaticInstance.GameOver();
        }
    }
}