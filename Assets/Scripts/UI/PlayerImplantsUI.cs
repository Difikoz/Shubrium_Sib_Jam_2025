using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerImplantsUI : BasicComponent
    {
        [SerializeField] private GameObject _implantIconPrefab;
        [SerializeField] private Transform _iconsContainer;
        
        private Dictionary<string, ImplantIconUI> _spawnedIcons;

        public override void InitializeComponent()
        {
            _spawnedIcons = new();
        }

        public override void EnableComponent()
        {
            GameManager.StaticInstance.Player.Equipment.OnImplantsChanged += UpdateImplantsUI;
        }

        public override void DisableComponent()
        {
            GameManager.StaticInstance.Player.Equipment.OnImplantsChanged -= UpdateImplantsUI;
        }
        
        private void UpdateImplantsUI(List<ImplantConfig> implants)
        {
            // Создаем словарь для подсчета количества каждого импланта
            // Используем составной ключ, чтобы различать импланты по их полному идентификатору
            Dictionary<string, int> implantCounts = new Dictionary<string, int>();
            Dictionary<string, ImplantConfig> implantConfigs = new Dictionary<string, ImplantConfig>();
            
            // Подсчитываем количество каждого импланта
            foreach (var implant in implants)
            {                
                string uniqueKey = $"{implant.Key}";
                
                if (implantCounts.ContainsKey(uniqueKey))
                {
                    // Если это действительно тот же самый имплант (стак), увеличиваем счетчик
                    implantCounts[uniqueKey]++;
                }
                else
                {
                    // Новый имплант
                    implantCounts[uniqueKey] = 1;
                    implantConfigs[uniqueKey] = implant;
                }
            }
            
            // Список ключей для удаления
            List<string> keysToRemove = new List<string>();
            
            // Обновляем существующие иконки и отмечаем, какие нужно удалить
            foreach (var iconPair in _spawnedIcons)
            {
                string key = iconPair.Key;
                
                if (implantCounts.ContainsKey(key))
                {
                    // Обновляем счетчик
                    iconPair.Value.UpdateCount(implantCounts[key]);
                    // Удаляем из словаря обработанные импланты
                    implantCounts.Remove(key);
                    implantConfigs.Remove(key);
                }
                else
                {
                    // Если такого импланта больше нет, отмечаем для удаления
                    keysToRemove.Add(key);
                }
            }
            
            // Удаляем ненужные иконки
            foreach (var key in keysToRemove)
            {
                Destroy(_spawnedIcons[key].gameObject);
                _spawnedIcons.Remove(key);
            }
            
            // Создаем новые иконки для оставшихся имплантов
            foreach (var pair in implantConfigs)
            {
                string key = pair.Key;
                ImplantConfig config = pair.Value;
                int count = implantCounts[key];
                
                GameObject newIconObj = Instantiate(_implantIconPrefab, _iconsContainer);
                ImplantIconUI iconUI = newIconObj.GetComponent<ImplantIconUI>();
                
                iconUI.Setup(config, count);
                _spawnedIcons.Add(key, iconUI);
            }
        }
    }    
} 