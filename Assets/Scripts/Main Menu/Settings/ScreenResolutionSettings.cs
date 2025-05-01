using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ScreenResolutionSettings : DropdownSettings
    {
        private Resolution[] _resolutions;

        protected override void UpdateDropdownSettings()
        {
            _resolutions = Screen.resolutions;
            List<string> resolutions = new();
            foreach (Resolution resolution in _resolutions)
            {
                resolutions.Add($"{resolution.width}x{resolution.height}");
            }
            _dropdown.ClearOptions();
            _dropdown.AddOptions(resolutions);
        }

        protected override void OnValueChanged(int value)
        {
            Screen.SetResolution(_resolutions[value].width, _resolutions[value].height, true);
        }
    }
}