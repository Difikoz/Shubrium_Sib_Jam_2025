using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Stat", menuName = "Winter Universe/Pawn/New Stat")]
    public class StatConfig : BasicInfoConfig
    {
        [field: SerializeField, Range(0f, 999999f)] public float MinValue { get; private set; }
        [field: SerializeField, Range(0f, 999999f)] public float MaxValue { get; private set; }
        [field: SerializeField] public bool IsPercent { get; private set; }
    }
}