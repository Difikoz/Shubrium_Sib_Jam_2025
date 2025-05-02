using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Tag", menuName = "Winter Universe/Gameplay/New Tag")]
    public class GameplayTag : ScriptableObject
    {
        [field: SerializeField] public string Key { get; private set; }
    }
}