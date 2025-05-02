using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class GameplayStatModifierCreator
    {
        [SerializeField] private GameplayStatConfig _config;
        [SerializeField] private GameplayStatModifier _modifier;

        public GameplayStatConfig Config => _config;
        public GameplayStatModifier Modifier => _modifier;

        public GameplayStatModifierCreator(GameplayStatConfig config, GameplayStatModifier modifier)
        {
            _config = config;
            _modifier = modifier;
        }
    }
}