using UnityEngine;

namespace WinterUniverse
{
    public abstract class BasicInfoConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField, TextArea(3, 5)] public string Description { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}