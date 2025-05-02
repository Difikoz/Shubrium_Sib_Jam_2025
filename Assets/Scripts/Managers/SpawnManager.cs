using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class SpawnManager : MonoBehaviour
    {
        // Список всех спавненных врагов
        private List<GameObject> _spawnedEnemies = new List<GameObject>();
        
        // Спавн одного врага
        public GameObject SpawnEnemy(EnemyConfig config, Vector3 position, Quaternion rotation)
        {
            if (config == null || config.EnemyPrefab == null)
            {
                Debug.LogError($"[{GetType().Name}] Невозможно создать врага: неверный конфиг или префаб");
                return null;
            }
            
            // Создаем врага
            GameObject enemy = Instantiate(config.EnemyPrefab, position, rotation);
            
            // TODO: Настраиваем компоненты врага на основе конфига
            
            // Добавляем врага в список
            _spawnedEnemies.Add(enemy);
            
            Debug.Log($"[{GetType().Name}] Создан враг: {config.Key} в позиции {position}");
            
            return enemy;
        }
        
        // Спавн группы врагов из данных
        public List<GameObject> SpawnEnemies(List<EnemySpawnData> spawnDataList)
        {
            List<GameObject> spawnedEnemies = new List<GameObject>();
            
            foreach (var spawnData in spawnDataList)
            {
                GameObject enemy = SpawnEnemy(spawnData.EnemyConfig, spawnData.SpawnPosition, spawnData.SpawnRotation);
                if (enemy != null)
                {
                    spawnedEnemies.Add(enemy);
                }
            }
            
            return spawnedEnemies;
        }
        
        // Удаление всех спавненных врагов
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