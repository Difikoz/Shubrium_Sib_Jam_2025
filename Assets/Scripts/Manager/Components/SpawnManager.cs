using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class SpawnManager : MonoBehaviour
    {
        // Список всех спавненных врагов
        private List<GameObject> _spawnedEnemies = new List<GameObject>();
        
        // Спавн врага по трансформу
        public GameObject SpawnEnemy(EnemyConfig config, Transform spawnPoint, Level level)
        {
            if (config == null || config.EnemyPrefab == null)
            {
                Debug.LogError($"[{GetType().Name}] Невозможно создать врага: неверный конфиг или префаб");
                return null;
            }
            
            if (spawnPoint == null)
            {
                Debug.LogError($"[{GetType().Name}] Невозможно создать врага: не указана точка спавна");
                return null;
            }
            
            // Создаем врага в указанной точке с её поворотом
            GameObject enemy = Instantiate(config.EnemyPrefab, spawnPoint.position, spawnPoint.rotation);
            
            // Если у уровня есть родительский трансформ для врагов, используем его
            if (level != null && level.EnemiesParent != null)
            {
                enemy.transform.SetParent(level.EnemiesParent);
            }
            
            // Регистрируем врага в Level
            level?.RegisterEnemy(enemy);
            
            // Добавляем врага в список
            _spawnedEnemies.Add(enemy);
            
            Debug.Log($"[{GetType().Name}] Создан враг: {config.Key} в точке {spawnPoint.name}");
            
            return enemy;
        }
        
        // Спавн всех врагов для уровня
        public void SpawnEnemiesForLevel(Level level)
        {
            if (level == null)
            {
                Debug.LogError($"[{GetType().Name}] Невозможно создать врагов: уровень не указан");
                return;
            }
            
            List<Transform> spawnPoints = level.SpawnPoints;
            if (spawnPoints == null || spawnPoints.Count == 0)
            {
                Debug.LogError($"[{GetType().Name}] Невозможно создать врагов: на уровне нет точек спавна");
                return;
            }
            
            // Индекс текущей точки спавна
            int spawnPointIndex = 0;
            
            // Проходим по всем типам врагов
            foreach (var enemyToSpawn in level.EnemiesToSpawn)
            {
                if (enemyToSpawn.EnemyConfig == null)
                    continue;
                
                // Спавним указанное количество врагов данного типа
                for (int i = 0; i < enemyToSpawn.Count; i++)
                {
                    // Берем следующую точку спавна циклически
                    Transform spawnPoint = spawnPoints[spawnPointIndex];
                    spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Count;
                    
                    // Спавним врага
                    SpawnEnemy(enemyToSpawn.EnemyConfig, spawnPoint, level);
                }
            }
            
            Debug.Log($"[{GetType().Name}] Созданы враги для уровня: {level.LevelName}");
        }
        
        // Удаление всех врагов
        public void DespawnAllEnemies()
        {
            foreach (var enemy in _spawnedEnemies)
            {
                if (enemy != null)
                {
                    Destroy(enemy);
                }
            }
            
            _spawnedEnemies.Clear();
            Debug.Log($"[{GetType().Name}] Все враги удалены");
        }
    }
} 