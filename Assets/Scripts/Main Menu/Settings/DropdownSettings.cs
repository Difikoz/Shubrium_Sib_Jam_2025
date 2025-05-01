using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class DropdownSettings : MonoBehaviour
    {
        [SerializeField] protected TMP_Dropdown _dropdown;
        [SerializeField] protected string _playerPrefsKey;
        [SerializeField] protected int _defaultValue = 0;

        protected virtual void Start()
        {
            UpdateDropdownSettings();
            ToggleValue(PlayerPrefs.GetInt(_playerPrefsKey, _defaultValue));
            _dropdown.SetValueWithoutNotify(PlayerPrefs.GetInt(_playerPrefsKey, _defaultValue));
            _dropdown.onValueChanged.AddListener(ToggleValue);
        }

        private void OnEnable()
        {
            _dropdown.SetValueWithoutNotify(PlayerPrefs.GetInt(_playerPrefsKey, _defaultValue));
        }

        protected virtual void UpdateDropdownSettings()
        {

        }

        private void ToggleValue(int value)
        {
            OnValueChanged(value);
            PlayerPrefs.SetInt(_playerPrefsKey, value);
        }

        protected abstract void OnValueChanged(int value);
    }
}