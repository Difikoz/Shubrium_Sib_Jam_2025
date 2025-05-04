using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class DashUI : BasicComponent
    {
        [SerializeField] private Image _dashFillImage;
        [SerializeField] private Color _readyColor = Color.green;
        [SerializeField] private Color _cooldownColor = Color.red;
        
        public override void InitializeComponent()
        {
            // Начальное состояние - рывок готов
            _dashFillImage.fillAmount = 1f;
            _dashFillImage.color = _readyColor;
        }
        
        public override void EnableComponent()
        {
            // Подписываемся на событие обновления кулдауна
            if (GameManager.StaticInstance != null && GameManager.StaticInstance.Player != null)
            {
                GameManager.StaticInstance.Player.Locomotion.OnDashCooldownUpdate += UpdateDashUI;
            }
        }
        
        public override void DisableComponent()
        {
            // Отписываемся от события
            if (GameManager.StaticInstance != null && GameManager.StaticInstance.Player != null)
            {
                GameManager.StaticInstance.Player.Locomotion.OnDashCooldownUpdate -= UpdateDashUI;
            }
        }
        
        // Обновление UI рывка
        private void UpdateDashUI(float fillAmount)
        {
            if (_dashFillImage == null) return;
            
            // Обновляем заполнение
            _dashFillImage.fillAmount = fillAmount;
            
            // Обновляем цвет
            _dashFillImage.color = Color.Lerp(_cooldownColor, _readyColor, fillAmount);
        }
    }
} 