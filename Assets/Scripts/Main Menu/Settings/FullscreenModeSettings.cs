using UnityEngine;

namespace WinterUniverse
{
    public class FullscreenModeSettings : DropdownSettings
    {
        protected override void OnValueChanged(int value)
        {
            switch (value)
            {
                case 0:
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                    break;
                case 1:
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    break;
                case 2:
                    Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                    break;
                case 3:
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                    break;
            }
        }
    }
}