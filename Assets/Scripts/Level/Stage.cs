using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class Stage : BasicComponent
    {
        [field: SerializeField] public string StageName { get; private set; }
        [field: SerializeField] public List<Transform> SpawnPoints { get; private set; }
        [field: SerializeField] public List<EnemySpawnData> EnemiesToSpawn { get; private set; }

        private List<EnemyController> _spawnedEnemies;

        public override void InitializeComponent()
        {
            _spawnedEnemies = new();
        }

        public void AddSpawnedEnemy(EnemyController enemy)
        {
            _spawnedEnemies.Add(enemy);
        }

        public bool CanCompleteStage()
        {
            foreach (EnemyController enemy in _spawnedEnemies)
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
            foreach (EnemyController enemy in _spawnedEnemies)
            {
                enemy.DisableComponent();
                LeanPool.Despawn(enemy.gameObject);
            }
        }
    }
}