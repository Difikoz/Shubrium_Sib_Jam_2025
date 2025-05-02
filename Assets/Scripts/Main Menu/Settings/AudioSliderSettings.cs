using FMODUnity;
using UnityEngine;

namespace WinterUniverse
{
    public class AudioSliderSettings : SliderSettings
    {
        [SerializeField] private string _busPath = "bus:/Master";

        protected override void OnValueChanged(float value)
        {
            RuntimeManager.GetBus(_busPath).setVolume(value);
        }
    }
}