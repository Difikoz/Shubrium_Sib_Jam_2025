using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class GameplayStatModifier
    {
        [SerializeField] private GameplayStatModifierType _type;
        [SerializeField] private float _value;

        public GameplayStatModifierType Type => _type;
        public float Value => _value;

        public GameplayStatModifier(GameplayStatModifierType type, float value)
        {
            _type = type;
            _value = value;
        }
    }
}