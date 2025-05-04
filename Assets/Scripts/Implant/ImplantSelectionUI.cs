using Lean.Pool;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ImplantSelectionUI : BasicComponent
    {
        [SerializeField] private GameObject _uiRoot;
        [SerializeField] private GameObject _optionPrefab;
        [SerializeField] private Transform _optionsContainer;

        private List<ImplantOptionUI> _spawnedOptions;
        private Action<ImplantConfig> _onImplantSelected;

        public override void InitializeComponent()
        {
            _spawnedOptions = new();
            DeactivateComponent();
        }

        public void ShowImplantSelection(List<ImplantConfig> implants, Action<ImplantConfig> onSelected)
        {
            _onImplantSelected = onSelected;

            // Очищаем предыдущие опции
            ClearOptions();

            // Создаем UI для каждого импланта
            foreach (var implant in implants)
            {
                ImplantOptionUI option = LeanPool.Spawn(_optionPrefab, _optionsContainer).GetComponent<ImplantOptionUI>();
                option.Setup(implant, OnOptionClicked);
                _spawnedOptions.Add(option);
            }

            _uiRoot.SetActive(true);
        }

        public void HideImplantSelection()
        {
            _uiRoot.SetActive(false);
            ClearOptions();
        }

        private void OnOptionClicked(ImplantConfig implant)
        {
            _onImplantSelected?.Invoke(implant);
        }

        private void ClearOptions()
        {
            foreach (var option in _spawnedOptions)
            {
                LeanPool.Despawn(option.gameObject);
            }
            _spawnedOptions.Clear();
        }
        
        // Новые методы для выбора имплантов через кнопки 1, 2, 3
        
        /// <summary>
        /// Выбирает имплант по индексу (0, 1, 2)
        /// </summary>
        public void SelectImplantByIndex(int index)
        {
            if (_uiRoot.activeSelf && index >= 0 && index < _spawnedOptions.Count)
            {
                ImplantOptionUI option = _spawnedOptions[index];
                option.SelectImplant();
            }
        }
        
        /// <summary>
        /// Выбирает первый имплант (кнопка 1)
        /// </summary>
        public void SelectImplant1()
        {
            SelectImplantByIndex(0);
        }
        
        /// <summary>
        /// Выбирает второй имплант (кнопка 2)
        /// </summary>
        public void SelectImplant2()
        {
            SelectImplantByIndex(1);
        }
        
        /// <summary>
        /// Выбирает третий имплант (кнопка 3)
        /// </summary>
        public void SelectImplant3()
        {
            SelectImplantByIndex(2);
        }
    }
}