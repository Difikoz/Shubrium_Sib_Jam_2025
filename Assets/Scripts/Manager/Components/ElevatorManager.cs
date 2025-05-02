using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class ElevatorManager : BasicComponent
    {
        private bool _isInElevator = false;
        
        public void ActivateElevator()
        {
            if (_isInElevator)
                return;
                
            _isInElevator = true;

            // Закрытие дверей лифта?

            // Сразу показываем импланты без задержки
            GameManager.StaticInstance.ImplantManager.ShowImplantSelection();

            // Выключение текущего этажа

            // Запускаем ожидание завершения выбора
            StartCoroutine(WaitForImplantSelection());
        }
        
        private IEnumerator WaitForImplantSelection()
        {
            // Ждем только один фрейм для обработки выбора
            yield return null;
            
            // Заканчиваем, когда ImplantManager завершит процесс выбора
            while (GameManager.StaticInstance.ImplantManager.IsSelectingImplant)
            {
                yield return null;
            }

            // Включение нового этажа

            // Когда выбор завершен, завершаем работу лифта
            _isInElevator = false;

            // Открытие дверей лифта?
        }
    }
} 