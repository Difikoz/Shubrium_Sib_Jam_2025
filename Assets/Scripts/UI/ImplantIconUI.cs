using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WinterUniverse
{
    // Отдельный класс для иконки импланта
    public class ImplantIconUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _countText;
        
        private ImplantConfig _config;
        
        public void Setup(ImplantConfig config, int count)
        {
            _config = config;
            
            // Настраиваем внешний вид
            _icon.sprite = config.Icon;
            
            // Обновляем счетчик
            UpdateCount(count);
        }
        
        public void UpdateCount(int count)
        {
            // Если только один имплант, скрываем счетчик
            if (count <= 1)
            {
                _countText.gameObject.SetActive(false);
            }
            else
            {
                _countText.gameObject.SetActive(true);
                _countText.text = count.ToString();
            }
        }
    }
}