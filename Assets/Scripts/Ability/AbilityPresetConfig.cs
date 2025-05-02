using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Winter Universe/Ability/New Ability")]
    public class AbilityPresetConfig : BasicInfoConfig
    {
        [field: SerializeField] public AbilityTargetType TargetType { get; private set; }
        [field: SerializeField] public AbilityCastTypeConfig CastType { get; private set; }
        [field: SerializeField] public List<AbilityHitTypeData> HitTypes { get; private set; }
    }
}