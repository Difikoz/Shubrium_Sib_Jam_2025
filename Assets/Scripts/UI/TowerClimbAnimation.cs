using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    /// <summary>
    /// Сверхпростая анимация башни для полноэкранных секций
    /// </summary>
    public class TowerClimbAnimation : MonoBehaviour
    {
        [Header("Настройки")]
        [SerializeField] private GameObject _sectionPrefab;    // Префаб секции (Image на весь экран)
        [SerializeField] private RectTransform _container;     // Контейнер для секций
        [SerializeField] private List<Sprite> _sectionSprites; // Спрайты секций
        [SerializeField] private float _scrollSpeed = 300f;    // Скорость в пикселях в секунду
        
        // Приватные поля
        private List<RectTransform> _sections = new List<RectTransform>();
        private float _screenHeight;
        
        private void Start()
        {
            // Проверка на ошибки конфигурации
            if (_sectionPrefab == null || _container == null || _sectionSprites == null || _sectionSprites.Count == 0)
            {
                Debug.LogError($"[{GetType().Name}] Не все параметры заданы!");
                enabled = false;
                return;
            }
            
            // Получаем высоту экрана
            _screenHeight = _container.rect.height;
            Debug.Log($"[{GetType().Name}] Высота экрана: {_screenHeight}");
            
            // Создаем ровно 3 секции: одна видима, одна сверху, одна снизу
            CreateSection(0, _screenHeight);      // Секция выше экрана
            CreateSection(1, 0);                  // Секция на экране
            CreateSection(2, -_screenHeight);     // Секция ниже экрана
        }
        
        private void CreateSection(int index, float yPosition)
        {
            GameObject sectionObj = Instantiate(_sectionPrefab, _container);
            
            // Настраиваем изображение
            Image image = sectionObj.GetComponent<Image>();
            if (image != null)
            {
                // Выбираем спрайт из списка случайно или по порядку
                image.sprite = _sectionSprites[Random.Range(0, _sectionSprites.Count)];
            }
            
            // Настраиваем позицию
            RectTransform rectTrans = sectionObj.GetComponent<RectTransform>();
            rectTrans.anchoredPosition = new Vector2(0, yPosition);
            
            // Даем осмысленное имя для отладки
            sectionObj.name = "Section_" + index;
            
            // Добавляем в список
            _sections.Add(rectTrans);
        }
        
        private void Update()
        {
            // Двигаем все секции вниз с одинаковой скоростью
            for (int i = 0; i < _sections.Count; i++)
            {
                // Двигаем секцию вниз
                Vector2 pos = _sections[i].anchoredPosition;
                pos.y -= _scrollSpeed * Time.deltaTime;
                _sections[i].anchoredPosition = pos;
                
                // Если секция полностью ушла вниз за экран
                if (pos.y < -_screenHeight * 1.5f)
                {
                    // Перемещаем ее наверх и даем новый спрайт
                    pos.y = _screenHeight * 1.5f;
                    _sections[i].anchoredPosition = pos;
                    
                    // Обновляем спрайт секции
                    Image image = _sections[i].GetComponent<Image>();
                    if (image != null)
                    {
                        image.sprite = _sectionSprites[Random.Range(0, _sectionSprites.Count)];
                    }
                    
                    Debug.Log($"[{GetType().Name}] Секция {i} перемещена наверх.");
                }
            }
        }
        
        // Метод для изменения скорости прокрутки
        public void SetScrollSpeed(float speed)
        {
            _scrollSpeed = speed;
        }
        
        // Очистка при уничтожении
        private void OnDestroy()
        {
            foreach (var section in _sections)
            {
                if (section != null)
                    Destroy(section.gameObject);
            }
            _sections.Clear();
        }
    }
} 