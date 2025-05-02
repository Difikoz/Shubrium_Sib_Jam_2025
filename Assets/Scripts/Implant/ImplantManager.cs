using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ImplantManager : Singleton<ImplantManager>
    {
        [SerializeField] private List<ImplantConfig> _allImplants = new List<ImplantConfig>();
        [SerializeField] private int _implantOptions = 3;
        [SerializeField] private ImplantSelectionUI _implantSelectionUI;
        
        private PlayerImplants _playerImplants;
        
        public bool IsSelectingImplant { get; private set; } = false;
        
        protected override void OnAwake()
        {
            base.OnAwake();
            // Дополнительная инициализация
        }
        
        public override void InitializeComponent()
        {
            base.InitializeComponent();
            // Находим компонент имплантов игрока напрямую
            _playerImplants = FindObjectOfType<PlayerImplants>();
            if (_playerImplants == null)
            {
                Debug.LogError($"[{GetType().Name}] PlayerImplants not found!");
            }
        }
        
        public void ShowImplantSelection()
        {
            IsSelectingImplant = true;
            
            // Получаем случайные импланты для выбора
            var implants = GetRandomImplants(_implantOptions);
            
            // Активируем UI выбора имплантов
            _implantSelectionUI.ShowImplantSelection(implants, OnImplantSelected);
        }
        
        private void OnImplantSelected(ImplantConfig selectedImplant)
        {
            // Добавляем выбранный имплант игроку
            AddImplantToPlayer(selectedImplant);
            
            // Закрываем UI выбора
            _implantSelectionUI.HideImplantSelection();
            
            // Отмечаем, что выбор завершен
            IsSelectingImplant = false;
            
            // Здесь можно добавить логику для показа диалогов
            // ShowDialogUI();
        }
        
        public void AddImplantToPlayer(ImplantConfig implant)
        {
            // Проверяем, что у нас есть ссылка на компонент имплантов игрока
            if (_playerImplants == null)
            {
                _playerImplants = FindObjectOfType<PlayerImplants>();
                if (_playerImplants == null)
                {
                    Debug.LogError($"[{GetType().Name}] PlayerImplants not found!");
                    return;
                }
            }
            
            // Применяем имплант через компонент игрока
            _playerImplants.ApplyImplant(implant);
        }
        
        public List<ImplantConfig> GetRandomImplants(int count)
        {
            List<ImplantConfig> validImplants = new List<ImplantConfig>();
            
            // Проверяем ссылку на компонент имплантов игрока
            if (_playerImplants == null)
            {
                _playerImplants = FindObjectOfType<PlayerImplants>();
                if (_playerImplants == null)
                {
                    Debug.LogError($"[{GetType().Name}] PlayerImplants not found!");
                    return new List<ImplantConfig>();
                }
            }
            
            // Фильтруем только те импланты, которые можно добавить
            foreach (var implant in _allImplants)
            {
                if (_playerImplants.CanAddImplant(implant))
                    validImplants.Add(implant);
            }
            
            // Если недостаточно имплантов, вернем все что есть
            if (validImplants.Count <= count)
                return new List<ImplantConfig>(validImplants);
                
            // Выбираем случайные импланты
            List<ImplantConfig> result = new List<ImplantConfig>();
            for (int i = 0; i < count; i++)
            {
                if (validImplants.Count == 0)
                    break;
                    
                int randomIndex = Random.Range(0, validImplants.Count);
                result.Add(validImplants[randomIndex]);
                validImplants.RemoveAt(randomIndex);
            }
            
            return result;
        }
    }
} 