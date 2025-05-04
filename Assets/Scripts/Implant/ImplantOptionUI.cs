using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace WinterUniverse
{
    public class ImplantOptionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _keyHintText;
        [SerializeField] private Button _button;
        
        [Header("Выделение при наведении")]
        [SerializeField] private GameObject _hoverHighlight;
        
        private ImplantConfig _implant;
        private Action<ImplantConfig> _onClicked;
        private int _optionIndex;
        
        private void Awake()
        {
            if (_hoverHighlight != null)
            {
                _hoverHighlight.SetActive(false);
            }
        }
        
        public void Setup(ImplantConfig implant, Action<ImplantConfig> onClicked, int index)
        {
            _implant = implant;
            _onClicked = onClicked;
            _optionIndex = index;
            
            _nameText.text = implant.DisplayName;
            _descriptionText.text = implant.Description;
            _icon.sprite = implant.Icon;
            
            if (_keyHintText != null)
            {
                switch (index)
                {
                    case 0:
                        _keyHintText.text = "Нажмите 1";
                        break;
                    case 1:
                        _keyHintText.text = "Нажмите 2";
                        break;
                    case 2:
                        _keyHintText.text = "Нажмите 3";
                        break;
                    default:
                        _keyHintText.text = "";
                        break;
                }
            }
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnButtonClicked);
            
            if (_hoverHighlight != null)
            {
                _hoverHighlight.SetActive(false);
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_hoverHighlight != null)
            {
                _hoverHighlight.SetActive(true);
            }
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_hoverHighlight != null)
            {
                _hoverHighlight.SetActive(false);
            }
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