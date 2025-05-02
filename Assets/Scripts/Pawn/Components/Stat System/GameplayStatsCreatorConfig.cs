using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Stat Creator", menuName = "Winter Universe/Gameplay/Stat/New Stat Creator")]
    public class GameplayStatsCreatorConfig : ScriptableObject
    {
        [field: SerializeField] public List<StatCreator> BaseStats { get; private set; }
    }
}