using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ImplantManager : BasicComponent
    {
        [field: SerializeField] public List<ImplantConfig> AllImplants { get; private set; }
        [SerializeField] private int _implantOptions = 3;

        public bool IsSelectingImplant { get; private set; } = false;

        public IEnumerator ShowImplantSelection()
        {
            IsSelectingImplant = true;

            // Получаем случайные импланты для выбора
            var implants = GetRandomImplants(_implantOptions);

            // Активируем UI выбора имплантов
            yield return GameManager.StaticInstance.UIManager.ImplantSelectionUI.ShowImplantSelection(implants, OnImplantSelected);
        }

        private void OnImplantSelected(ImplantConfig selectedImplant)
        {
            // Добавляем выбранный имплант игроку
            GameManager.StaticInstance.Player.Equipment.AddImplant(selectedImplant);

            // Закрываем UI выбора
            StartCoroutine(GameManager.StaticInstance.UIManager.ImplantSelectionUI.HideImplantSelection());

            // Отмечаем, что выбор завершен
            IsSelectingImplant = false;
        }

        public List<ImplantConfig> GetRandomImplants(int count)
        {
            List<ImplantConfig> validImplants = new();

            // Фильтруем только те импланты, которые можно добавить
            foreach (var implant in AllImplants)
            {
                if (GameManager.StaticInstance.Player.Equipment.CanAddImplant(implant))
                {
                    validImplants.Add(implant);
                }
            }

            // Если недостаточно имплантов, вернем все что есть
            if (validImplants.Count <= count)
                return new List<ImplantConfig>(validImplants);

            // рандомные учитывая шанс
            List<ImplantConfig> randomImplants = new();
            for (int i = 0; i < count; i++)
            {
                int selectedIndex;
                int sum = 0;
                for (int s = 0; s < validImplants.Count; s++)
                {
                    sum += validImplants[s].Chance;
                }
                int randomIndex = Random.Range(0, sum);
                for (selectedIndex = 0; selectedIndex < validImplants.Count; selectedIndex++)
                {
                    randomIndex -= validImplants[selectedIndex].Chance;
                    if (randomIndex < 0)
                    {
                        break;
                    }
                }
                randomImplants.Add(validImplants[selectedIndex]);
                validImplants.RemoveAt(selectedIndex);
            }

            //// Выбираем случайные импланты
            //List<ImplantConfig> result = new();
            //int randomIndex;
            //for (int i = 0; i < count; i++)
            //{
            //    if (validImplants.Count == 0)
            //    {
            //        break;
            //    }
            //    randomIndex = Random.Range(0, validImplants.Count);
            //    result.Add(validImplants[randomIndex]);
            //    validImplants.RemoveAt(randomIndex);
            //}

            return randomImplants;
        }
    }
}