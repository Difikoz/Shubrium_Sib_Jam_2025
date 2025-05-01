using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public abstract class SliderSettings : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private string _playerPrefsKey;
        [SerializeField] private float _defaultValue = 0f;
        [SerializeField] private float _minValue = -1f;
        [SerializeField] private float _maxValue = 1f;

        private void Start()
        {
            ToggleValue(PlayerPrefs.GetFloat(_playerPrefsKey, _defaultValue));
            _slider.minValue = _minValue;
            _slider.maxValue = _maxValue;
            _slider.value = PlayerPrefs.GetFloat(_playerPrefsKey, _defaultValue);
            _slider.onValueChanged.AddListener(ToggleValue);
        }

        private void ToggleValue(float value)
        {
            OnValueChanged(value);
            PlayerPrefs.SetFloat(_playerPrefsKey, value);
        }

        protected abstract void OnValueChanged(float value);
    }
}