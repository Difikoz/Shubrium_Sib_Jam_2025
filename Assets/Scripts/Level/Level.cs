using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WinterUniverse
{
    // Структура для хранения типа врага и его количества
    [Serializable]
    public class EnemyToSpawn
    {
        public EnemyConfig EnemyConfig;
        public int Count = 1;
    }
    
    public class Level : MonoBehaviour
    {
        [SerializeField] private string _levelName = "Уровень";
        
        // Точки спавна - просто трансформы, где будут появляться враги
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        
        // Враги для спавна - список типов и их количества
        [SerializeField] private List<EnemyToSpawn> _enemiesToSpawn = new List<EnemyToSpawn>();
        
        // ГЛАВНЫЙ ОБЪЕКТ УРОВНЯ - содержит все здания, декорации и прочие элементы
        [SerializeField] private GameObject _levelObjects;
        
        // Родительский трансформ для всех спавненных врагов
        [SerializeField] private Transform _enemiesParent;
        
        // Событие при завершении уровня
        public UnityEvent OnLevelCompleted = new UnityEvent();
        
        // Список активных врагов на уровне
        private List<GameObject> _activeEnemies = new List<GameObject>();
        
        public string LevelName => _levelName;
        public List<Transform> SpawnPoints => _spawnPoints;
        public List<EnemyToSpawn> EnemiesToSpawn => _enemiesToSpawn;
        public Transform EnemiesParent => _enemiesParent;
        
        // Активация уровня
        public void ActivateLevel()
        {
            // Активируем объект уровня с декорациями и зданиями
            if (_levelObjects != null)
            {
                _levelObjects.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"[{GetType().Name}] Объект уровня не назначен для \"{_levelName}\"!");
            }
            
            Debug.Log($"[{GetType().Name}] Уровень \"{_levelName}\" активирован");
        }
        
        // Деактивация уровня
        public void DeactivateLevel()
        {
            // Деактивируем объект уровня с декорациями и зданиями
            if (_levelObjects != null)
            {
                _levelObjects.SetActive(false);
            }
            
            Debug.Log($"[{GetType().Name}] Уровень \"{_levelName}\" деактивирован");
        }
        
        // Инициализация уровня
        public void Initialize()
        {
            _activeEnemies.Clear();
            
            // Создаем родительский объект для врагов, если он не назначен
            if (_enemiesParent == null)
            {
                GameObject enemiesParentObj = new GameObject("Enemies");
                enemiesParentObj.transform.SetParent(transform);
                enemiesParentObj.transform.localPosition = Vector3.zero;
                _enemiesParent = enemiesParentObj.transform;
            }
            
            Debug.Log($"[{GetType().Name}] Уровень \"{_levelName}\" инициализирован");
        }
        
        // Регистрация врага, созданного на уровне
        public void RegisterEnemy(GameObject enemy)
        {
            if (enemy != null && !_activeEnemies.Contains(enemy))
            {
                _activeEnemies.Add(enemy);
                
                // Устанавливаем родителя для врага
                if (_enemiesParent != null)
                {
                    enemy.transform.SetParent(_enemiesParent);
                }
            }
        }
        
        // Очистка врагов на уровне
        public void ClearEnemies()
        {
            foreach (var enemy in _activeEnemies)
            {
                if (enemy != null)
                {
                    Destroy(enemy);
                }
            }
            
            _activeEnemies.Clear();
            Debug.Log($"[{GetType().Name}] Все враги на уровне удалены");
        }
        
        // Проверка завершения уровня
        public bool CheckCompletion()
        {
            // Здесь можно реализовать логику проверки условий завершения
            // Например, если все враги уничтожены
            
            bool isCompleted = _activeEnemies.Count == 0 || _activeEnemies.TrueForAll(e => e == null);
            
            if (isCompleted)
            {
                OnLevelCompleted?.Invoke();
            }
            
            return isCompleted;
        }
        
        // Принудительное завершение уровня
        public void CompleteLevel()
        {
            OnLevelCompleted?.Invoke();
            Debug.Log($"[{GetType().Name}] Уровень \"{_levelName}\" завершен");
        }
    }
} 