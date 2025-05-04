using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
        [SerializeField] private float _sectionOverlap = 2f;   // Нахлест секций в пикселях
        
        // Приватные поля
        private List<RectTransform> _sections = new List<RectTransform>();
        private float _screenHeight;
        private List<int> _shuffledSpriteIndices = new List<int>(); // Перемешанные индексы спрайтов
        private int _currentSpriteIndex = 0; // Текущий индекс в перемешанном списке
        
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
            
            // Перемешиваем индексы спрайтов
            ShuffleSpriteIndices();
            
            // Создаем секции с нахлестом
            CreateSection(0, _screenHeight - _sectionOverlap);  // Секция выше экрана (с нахлестом)
            CreateSection(1, 0);                               // Секция на экране
            CreateSection(2, -_screenHeight + _sectionOverlap); // Секция ниже экрана (с нахлестом)
        }
        
        // Перемешивание индексов спрайтов
        private void ShuffleSpriteIndices()
        {
            // Создаем новый список индексов
            _shuffledSpriteIndices = Enumerable.Range(0, _sectionSprites.Count).ToList();
            
            // Перемешиваем список (алгоритм Фишера-Йейтса)
            for (int i = _shuffledSpriteIndices.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                int temp = _shuffledSpriteIndices[i];
                _shuffledSpriteIndices[i] = _shuffledSpriteIndices[j];
                _shuffledSpriteIndices[j] = temp;
            }
            
            // Сбрасываем текущий индекс
            _currentSpriteIndex = 0;
            
            Debug.Log($"[{GetType().Name}] Спрайты перемешаны для нового цикла.");
        }
        
        // Получение следующего спрайта из перемешанного списка
        private Sprite GetNextSprite()
        {
            // Если мы использовали все спрайты, перемешиваем заново
            if (_currentSpriteIndex >= _shuffledSpriteIndices.Count)
            {
                ShuffleSpriteIndices();
            }
            
            // Берем спрайт из перемешанного списка
            Sprite sprite = _sectionSprites[_shuffledSpriteIndices[_currentSpriteIndex]];
            _currentSpriteIndex++;
            
            return sprite;
        }
        
        private void CreateSection(int index, float yPosition)
        {
            GameObject sectionObj = Instantiate(_sectionPrefab, _container);
            
            // Настраиваем изображение
            Image image = sectionObj.GetComponent<Image>();
            if (image != null)
            {
                // Используем спрайт из перемешанного списка
                image.sprite = GetNextSprite();
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
                    // Добавляем нахлест при перемещении секции наверх
                    pos.y = _screenHeight * 1.5f - _sectionOverlap;
                    _sections[i].anchoredPosition = pos;
                    
                    // Обновляем спрайт секции на следующий из перемешанного списка
                    Image image = _sections[i].GetComponent<Image>();
                    if (image != null)
                    {
                        image.sprite = GetNextSprite();
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
        
        // Метод для изменения нахлеста секций
        public void SetSectionOverlap(float overlap)
        {
            _sectionOverlap = overlap;
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