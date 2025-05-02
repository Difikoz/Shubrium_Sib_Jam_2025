using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<Level> _levels = new List<Level>();
        [SerializeField] private int _currentLevelIndex = 0;
        
        private SpawnManager _spawnManager;
        private Level _currentLevel;
        
        private void Awake()
        {
            // Получаем ссылку на SpawnManager
            _spawnManager = FindObjectOfType<SpawnManager>();
            if (_spawnManager == null)
            {
                Debug.LogError($"[{GetType().Name}] SpawnManager не найден!");
            }
            
            // Деактивируем все уровни при старте
            DeactivateAllLevels();
        }
        
        private void Start()
        {
            // Запускаем первый уровень
            StartCurrentLevel();
        }
        
        // Запуск текущего уровня по индексу
        public void StartCurrentLevel()
        {
            if (_levels.Count == 0)
            {
                Debug.LogError($"[{GetType().Name}] Нет уровней для запуска!");
                return;
            }
            
            // Проверяем валидность индекса
            if (_currentLevelIndex < 0 || _currentLevelIndex >= _levels.Count)
            {
                Debug.LogError($"[{GetType().Name}] Неверный индекс уровня: {_currentLevelIndex}");
                return;
            }
            
            // Запоминаем текущий уровень
            _currentLevel = _levels[_currentLevelIndex];
            
            // Подписываемся на событие завершения уровня
            _currentLevel.OnLevelCompleted.AddListener(OnCurrentLevelCompleted);
            
            // Активируем уровень
            _currentLevel.ActivateLevel();
            
            // Инициализируем уровень
            _currentLevel.Initialize();
            
            // Спавним врагов
            if (_spawnManager != null)
            {
                _spawnManager.SpawnEnemiesForLevel(_currentLevel);
            }
            
            Debug.Log($"[{GetType().Name}] Запущен уровень {_currentLevelIndex + 1}: {_currentLevel.LevelName}");
        }
        
        // Обработчик события завершения уровня
        private void OnCurrentLevelCompleted()
        {
            Debug.Log($"[{GetType().Name}] Уровень {_currentLevelIndex + 1} завершен!");
            CompleteCurrentLevel();
        }
        
        // Переход к следующему уровню
        public void CompleteCurrentLevel()
        {
            // Очищаем текущий уровень
            if (_currentLevel != null)
            {
                // Отписываемся от события
                _currentLevel.OnLevelCompleted.RemoveListener(OnCurrentLevelCompleted);
                
                // Очищаем и деактивируем
                _currentLevel.ClearEnemies();
                _currentLevel.DeactivateLevel();
            }
            
            // Переходим к следующему
            _currentLevelIndex++;
            
            // Проверяем, не закончились ли уровни
            if (_currentLevelIndex >= _levels.Count)
            {
                Debug.Log($"[{GetType().Name}] Все уровни пройдены!");
                // Здесь можно вызвать логику завершения игры или перезапуск с первого уровня
                _currentLevelIndex = 0; // Для цикличного прохождения
            }
            
            // Запускаем следующий уровень
            StartCurrentLevel();
        }
        
        // Перезапуск текущего уровня
        public void RestartCurrentLevel()
        {
            if (_currentLevel != null)
            {
                // Отписываемся от события
                _currentLevel.OnLevelCompleted.RemoveListener(OnCurrentLevelCompleted);
                
                // Очищаем и деактивируем
                _currentLevel.ClearEnemies();
                _currentLevel.DeactivateLevel();
            }
            
            StartCurrentLevel();
        }
        
        // Деактивация всех уровней
        private void DeactivateAllLevels()
        {
            foreach (var level in _levels)
            {
                if (level != null)
                {
                    level.DeactivateLevel();
                }
            }
        }
        
        // Метод для удобства тестирования
        public void JumpToLevel(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < _levels.Count)
            {
                if (_currentLevel != null)
                {
                    // Отписываемся от события
                    _currentLevel.OnLevelCompleted.RemoveListener(OnCurrentLevelCompleted);
                    
                    // Очищаем и деактивируем
                    _currentLevel.ClearEnemies();
                    _currentLevel.DeactivateLevel();
                }
                
                _currentLevelIndex = levelIndex;
                StartCurrentLevel();
            }
            else
            {
                Debug.LogError($"[{GetType().Name}] Неверный индекс уровня: {levelIndex}");
            }
        }
    }
} 