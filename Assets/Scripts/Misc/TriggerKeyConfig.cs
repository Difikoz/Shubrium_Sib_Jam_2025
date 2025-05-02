using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Trigger Key", menuName = "Winter Universe/Misc/New Trigger Key")]
    public class TriggerKeyConfig : ScriptableObject
    {
        [field: SerializeField] public string Key { get; private set; }
    }
}