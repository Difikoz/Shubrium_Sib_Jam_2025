using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ImplantSelectionUI : BasicComponent
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _optionPrefab;
        [SerializeField] private Transform _optionsContainer;

        private List<ImplantOptionUI> _spawnedOptions;
        private Action<ImplantConfig> _onImplantSelected;

        public override void InitializeComponent()
        {
            _spawnedOptions = new();
            DeactivateComponent();
        }

        public IEnumerator ShowImplantSelection(List<ImplantConfig> implants, Action<ImplantConfig> onSelected)
        {
            _onImplantSelected = onSelected;

            // Очищаем предыдущие опции
            ClearOptions();

            // Создаем UI для каждого импланта
            for (int i = 0; i < implants.Count; i++)
            {
                ImplantOptionUI option = LeanPool.Spawn(_optionPrefab, _optionsContainer).GetComponent<ImplantOptionUI>();
                option.Setup(implants[i], OnOptionClicked, i);
                _spawnedOptions.Add(option);
            }

            _canvasGroup.gameObject.SetActive(true);

            while (_canvasGroup.alpha != 1f)
            {
                _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, 1f, 2f * Time.deltaTime);
                yield return null;
            }
        }

        public IEnumerator HideImplantSelection()
        {
            while (_canvasGroup.alpha != 0f)
            {
                _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, 0f, Time.deltaTime);
                yield return null;
            }
            ClearOptions();
            _canvasGroup.gameObject.SetActive(false);
        }

        private void OnOptionClicked(ImplantConfig implant)
        {
            if (_canvasGroup.alpha != 1f)
            {
                return;
            }
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
            if (_canvasGroup.gameObject.activeSelf && index >= 0 && index < _spawnedOptions.Count)
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