using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class StatCreator
    {
        [field: SerializeField] public GameplayStatConfig Stat { get; private set; }
        [field: SerializeField] public float BaseValue { get; private set; }
    }
}