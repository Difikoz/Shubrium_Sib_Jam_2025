using UnityEngine;

namespace WinterUniverse
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelConfig _currentLevelConfig;
        
        private SpawnManager _spawnManager;
        
        private void Awake()
        {
            // Получаем ссылку на SpawnManager
            _spawnManager = FindObjectOfType<SpawnManager>();
            if (_spawnManager == null)
            {
                Debug.LogError($"[{GetType().Name}] SpawnManager не найден!");
            }
        }
        
        private void Start()
        {
            // Автоматически загружаем уровень, если он задан
            if (_currentLevelConfig != null)
            {
                LoadLevel(_currentLevelConfig);
            }
        }
        
        // Загрузка уровня по конфигу
        public void LoadLevel(LevelConfig levelConfig)
        {
            if (levelConfig == null)
            {
                Debug.LogError($"[{GetType().Name}] Невозможно загрузить уровень: неверный конфиг");
                return;
            }
            
            // Очищаем предыдущий уровень, если был
            ClearLevel();
            
            // Устанавливаем текущий конфиг
            _currentLevelConfig = levelConfig;
            
            // Спавним врагов из конфига
            SpawnEnemiesForLevel();
            
            Debug.Log($"[{GetType().Name}] Загружен уровень: {levelConfig.DisplayName}");
        }
        
        // Спавн врагов для текущего уровня
        private void SpawnEnemiesForLevel()
        {
            if (_currentLevelConfig == null || _spawnManager == null)
                return;
                
            _spawnManager.SpawnEnemies(_currentLevelConfig.EnemiesToSpawn);
        }
        
        // Очистка текущего уровня
        public void ClearLevel()
        {
            if (_spawnManager != null)
            {
                _spawnManager.DespawnAllEnemies();
            }
        }
        
        // Перезагрузка текущего уровня
        public void RestartLevel()
        {
            if (_currentLevelConfig != null)
            {
                LoadLevel(_currentLevelConfig);
            }
        }
    }
} 