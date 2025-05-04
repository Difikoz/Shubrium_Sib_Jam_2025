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
        private float _totalScrollDistance = 0f; // Общее пройденное расстояние для отслеживания
        
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
            // Расчет приращения прокрутки
            float scrollIncrement = _scrollSpeed * Time.deltaTime;
            _totalScrollDistance += scrollIncrement;
            
            // Проверяем, не пора ли выполнить перестановку секций
            bool needsRearrangement = false;
            int bottomSectionIndex = -1;
            float lowestY = float.MaxValue;
            
            // Двигаем все секции вниз с одинаковой скоростью
            for (int i = 0; i < _sections.Count; i++)
            {
                // Двигаем секцию вниз
                Vector2 pos = _sections[i].anchoredPosition;
                pos.y -= scrollIncrement;
                _sections[i].anchoredPosition = pos;
                
                // Отслеживаем самую нижнюю секцию
                if (pos.y < lowestY)
                {
                    lowestY = pos.y;
                    bottomSectionIndex = i;
                }
                
                // Проверяем, нужна ли перестановка
                if (pos.y < -_screenHeight * 1.5f)
                {
                    needsRearrangement = true;
                }
            }
            
            // Если нужна перестановка - переставляем секции и обновляем их позиции
            if (needsRearrangement && bottomSectionIndex >= 0)
            {
                RearrangeSections(bottomSectionIndex);
            }
        }
        
        // Метод для перестановки секций с точными позициями
        private void RearrangeSections(int bottomSectionIndex)
        {
            // Получаем самую верхнюю секцию
            int topSectionIndex = -1;
            float highestY = float.MinValue;
            
            for (int i = 0; i < _sections.Count; i++)
            {
                if (i != bottomSectionIndex && _sections[i].anchoredPosition.y > highestY)
                {
                    highestY = _sections[i].anchoredPosition.y;
                    topSectionIndex = i;
                }
            }
            
            if (topSectionIndex < 0) return;
            
            // Перемещаем нижнюю секцию наверх на строго определенную позицию
            RectTransform bottomSection = _sections[bottomSectionIndex];
            RectTransform topSection = _sections[topSectionIndex];
            
            // Точно рассчитываем позицию новой верхней секции
            Vector2 newPosition = topSection.anchoredPosition;
            newPosition.y += _screenHeight - _sectionOverlap;
            bottomSection.anchoredPosition = newPosition;
            
            // Обновляем спрайт
            Image image = bottomSection.GetComponent<Image>();
            if (image != null)
            {
                image.sprite = GetNextSprite();
            }
            
            Debug.Log($"[{GetType().Name}] Секция {bottomSectionIndex} перемещена наверх.");
        }
        
        // Каждые N секунд принудительно выравниваем все секции для предотвращения дрейфа
        private void FixedUpdate()
        {
            // Каждые 5 секунд выполняем выравнивание
            if (Time.frameCount % 150 == 0) 
            {
                AlignAllSections();
            }
        }
        
        // Выравнивание всех секций относительно друг друга
        private void AlignAllSections()
        {
            if (_sections.Count < 3) return;
            
            // Сортируем секции по их Y-позиции (сверху вниз)
            List<RectTransform> sortedSections = _sections
                .OrderByDescending(s => s.anchoredPosition.y)
                .ToList();
            
            // Берем верхнюю секцию как опорную и выравниваем остальные относительно неё
            Vector2 topPos = sortedSections[0].anchoredPosition;
            
            // Выравниваем среднюю секцию
            Vector2 middlePos = topPos;
            middlePos.y -= _screenHeight - _sectionOverlap;
            sortedSections[1].anchoredPosition = middlePos;
            
            // Выравниваем нижнюю секцию
            Vector2 bottomPos = middlePos;
            bottomPos.y -= _screenHeight - _sectionOverlap;
            sortedSections[2].anchoredPosition = bottomPos;
            
            Debug.Log($"[{GetType().Name}] Выполнено выравнивание всех секций.");
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