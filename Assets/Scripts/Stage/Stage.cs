using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class Stage : BasicComponentHolder
    {
        [field: SerializeField] public string StageName { get; private set; }
        [field: SerializeField] public Transform PlayerStartPoint { get; private set; }
        [field: SerializeField] public List<Transform> SpawnPoints { get; private set; }
        [field: SerializeField] public List<EnemySpawnData> EnemiesToSpawn { get; private set; }
        [field: SerializeField] public DialogueConfig DialogueBeforeBattle { get; private set; }
        [field: SerializeField] public DialogueConfig DialogueAfterBattle { get; private set; }

        public int EnemyCount => _components.Count;

        public override void ActivateComponent()
        {
            gameObject.SetActive(true);
            base.ActivateComponent();
        }

        public override void DeactivateComponent()
        {
            base.DeactivateComponent();
            gameObject.SetActive(false);
        }

        public void TeleportPlayerToStartPoint()
        {
            GameManager.StaticInstance.Player.transform.SetPositionAndRotation(PlayerStartPoint.transform.position, PlayerStartPoint.transform.rotation);
        }

        public void AddSpawnedEnemy(EnemyController enemy)
        {
            enemy.InitializeComponent();
            enemy.EnableComponent();
            enemy.Health.Revive(enemy);
            _components.Add(enemy);
        }

        public bool CanCompleteStage()
        {
            foreach (BasicComponent enemy in _components)
            {
                if (enemy.isActiveAndEnabled)
                {
                    return false;
                }
            }
            return true;
        }

        public void CompleteStage()
        {
            foreach (BasicComponent enemy in _components)
            {
                enemy.DisableComponent();
                LeanPool.Despawn(enemy.gameObject);
            }
            _components.Clear();
        }

        public Transform GetRandomSpawnPoint()
        {
            return SpawnPoints[Random.Range(0, SpawnPoints.Count)];
        }
    }
}