using System;
using UnityEngine;

namespace WinterUniverse
{
    public static class EventBus
    {
        public static Action OnInputBingingsChanged;

        public static void InputBingingsChanged()
        {
            OnInputBingingsChanged?.Invoke();
        }
    }
}