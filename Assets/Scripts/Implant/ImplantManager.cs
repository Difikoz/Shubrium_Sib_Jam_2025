using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ImplantManager : BasicComponent
    {
        [SerializeField] private List<ImplantConfig> _allImplants = new();
        [SerializeField] private int _implantOptions = 3;
        [SerializeField] private ImplantSelectionUI _implantSelectionUI;

        public bool IsSelectingImplant { get; private set; } = false;

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
            GameManager.StaticInstance.Player.Equipment.AddImplant(selectedImplant);

            // Закрываем UI выбора
            _implantSelectionUI.HideImplantSelection();

            // Отмечаем, что выбор завершен
            IsSelectingImplant = false;

            // Здесь можно добавить логику для показа диалогов
            // ShowDialogUI();
        }

        public List<ImplantConfig> GetRandomImplants(int count)
        {
            List<ImplantConfig> validImplants = new();

            // Фильтруем только те импланты, которые можно добавить
            foreach (var implant in _allImplants)
            {
                if (GameManager.StaticInstance.Player.Equipment.CanAddImplant(implant))
                {
                    validImplants.Add(implant);
                }
            }

            // Если недостаточно имплантов, вернем все что есть
            if (validImplants.Count <= count)
                return new List<ImplantConfig>(validImplants);

            // Выбираем случайные импланты
            List<ImplantConfig> result = new();
            int randomIndex;
            for (int i = 0; i < count; i++)
            {
                if (validImplants.Count == 0)
                {
                    break;
                }
                randomIndex = Random.Range(0, validImplants.Count);
                result.Add(validImplants[randomIndex]);
                validImplants.RemoveAt(randomIndex);
            }

            return result;
        }
    }
}