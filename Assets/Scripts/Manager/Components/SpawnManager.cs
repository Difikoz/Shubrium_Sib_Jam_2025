using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class SpawnManager : BasicComponent
    {
        public void SpawnEnemies(Stage stage)
        {
            List<Transform> vacantSpawnPoints = new(stage.SpawnPoints);
            int spawnPointIndex;
            foreach (EnemySpawnData spawnData in stage.EnemiesToSpawn)
            {
                for (int i = 0; i < spawnData.Count; i++)
                {
                    if (vacantSpawnPoints.Count == 0)// на случай, если врагов больше, чем точек спавна
                    {
                        vacantSpawnPoints = new(stage.SpawnPoints);
                    }
                    spawnPointIndex = Random.Range(0, vacantSpawnPoints.Count);
                    SpawnEnemy(spawnData.Config, vacantSpawnPoints[spawnPointIndex], stage);
                    vacantSpawnPoints.RemoveAt(spawnPointIndex);
                }
            }
        }

        public void SpawnEnemy(EnemyConfig config, Transform spawnPoint, Stage stage)
        {
            EnemyController enemy = LeanPool.Spawn(config.Prefab, spawnPoint.position, spawnPoint.rotation).GetComponent<EnemyController>();
            stage.AddSpawnedEnemy(enemy);
        }
    }
}