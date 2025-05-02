using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class ElevatorManager : MonoBehaviour
    {
        private ImplantManager _implantManager;
        
        private bool _isInElevator = false;
        
        private void Awake()
        {
            // Находим ImplantManager на сцене
            _implantManager = FindObjectOfType<ImplantManager>();
            if (_implantManager == null)
            {
                Debug.LogError($"[{GetType().Name}] ImplantManager не найден на сцене!");
            }
        }
        
        public void ActivateElevator()
        {
            if (_isInElevator)
                return;
                
            _isInElevator = true;
            
            // Сразу показываем импланты без задержки
            Debug.Log($"[{GetType().Name}] Elevator activated");
            _implantManager.ShowImplantSelection();
            
            // Запускаем ожидание завершения выбора
            StartCoroutine(WaitForImplantSelection());
        }
        
        private IEnumerator WaitForImplantSelection()
        {
            // Ждем только один фрейм для обработки выбора
            yield return null;
            
            // Заканчиваем, когда ImplantManager завершит процесс выбора
            while (_implantManager.IsSelectingImplant)
            {
                yield return null;
            }
            
            // Когда выбор завершен, завершаем работу лифта
            _isInElevator = false;
            Debug.Log($"[{GetType().Name}] Elevator sequence completed");
            // Тут можно добавить переход к следующему уровню
        }
    }
} 