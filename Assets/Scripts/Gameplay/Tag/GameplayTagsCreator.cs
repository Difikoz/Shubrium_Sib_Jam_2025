using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Tags Creator", menuName = "Winter Universe/Gameplay/New Tags Creator")]
    public class GameplayTagsCreator : ScriptableObject
    {
        [field: SerializeField] public string Key { get; private set; }
        [field: SerializeField] public List<GameplayTag> Tags { get; private set; }
    }
}