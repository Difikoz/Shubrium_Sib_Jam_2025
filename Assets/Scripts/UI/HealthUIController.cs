using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class HealthUIController : BasicComponent
    {
        [SerializeField] private GameObject _heartPrefab;
        [SerializeField] private Transform _heartsContainer;
        [SerializeField] private int _maxHeartsToShow = 10; // Максимальное количество отображаемых сердец

        private List<HeartUI> _hearts;

        public override void InitializeComponent()
        {
            _hearts = new();
        }

        public override void EnableComponent()
        {
            GameManager.StaticInstance.Player.Health.OnValueChanged += UpdateHealthUI;
        }

        public override void DisableComponent()
        {
            GameManager.StaticInstance.Player.Health.OnValueChanged -= UpdateHealthUI;
        }

        // Обновление UI на основе полученных значений здоровья (текущее и максимальное)
        private void UpdateHealthUI(int currentHealth, int maxHealth)
        {
            // Рассчитываем, сколько сердец нам нужно (каждое сердце = 2 единицы здоровья)
            int heartsNeeded = Mathf.CeilToInt(maxHealth / 2f);
            heartsNeeded = Mathf.Min(heartsNeeded, _maxHeartsToShow);

            // Создаем или удаляем сердца, если нужно
            while (_hearts.Count < heartsNeeded)
            {
                GameObject heartObj = Instantiate(_heartPrefab, _heartsContainer);
                HeartUI heart = heartObj.GetComponent<HeartUI>();
                _hearts.Add(heart);
            }

            while (_hearts.Count > heartsNeeded)
            {
                HeartUI heart = _hearts[_hearts.Count - 1];
                _hearts.RemoveAt(_hearts.Count - 1);
                Destroy(heart.gameObject);
            }

            // Обновляем состояние каждого сердца
            for (int i = 0; i < _hearts.Count; i++)
            {
                HeartUI heart = _hearts[i];

                // Рассчитываем количество единиц здоровья для текущего сердца
                int heartIndex = i * 2; // Индекс начала текущего сердца (0, 2, 4, 6...)

                if (heartIndex + 1 < currentHealth)
                {
                    // Если осталось 2 единицы здоровья - сердце полное
                    heart.SetState(HeartUI.HeartState.Full);
                }
                else if (heartIndex < currentHealth)
                {
                    // Если осталась 1 единица здоровья - половина сердца
                    heart.SetState(HeartUI.HeartState.Half);
                }
                else
                {
                    // Если не осталось единиц здоровья - сердце пустое
                    heart.SetState(HeartUI.HeartState.Empty);
                }
            }
        }
    }
}