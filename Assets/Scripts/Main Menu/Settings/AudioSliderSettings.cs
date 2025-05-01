using FMODUnity;
using UnityEngine;

namespace WinterUniverse
{
    public class AudioSliderSettings : SliderSettings
    {
        [SerializeField] private string _audioType = "Master";

        protected override void OnValueChanged(float value)
        {
            //RuntimeManager.SetFloat($"Volume{_audioType}", value);
        }
    }
}