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
    }
}