using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WinterUniverse
{
    public class ImplantOptionUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Button _button;
        
        private ImplantConfig _implant;
        private Action<ImplantConfig> _onClicked;
        
        public void Setup(ImplantConfig implant, Action<ImplantConfig> onClicked)
        {
            _implant = implant;
            _onClicked = onClicked;
            
            _nameText.text = implant.DisplayName;
            _descriptionText.text = implant.Description;
            _icon.sprite = implant.Icon;            
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnButtonClicked);
        }
        
        private void OnButtonClicked()
        {
            _onClicked?.Invoke(_implant);
        }
        
        /// <summary>
        /// Метод для программного выбора импланта (вызов по кнопке)
        /// </summary>
        public void SelectImplant()
        {
            OnButtonClicked();
        }
    }
} 