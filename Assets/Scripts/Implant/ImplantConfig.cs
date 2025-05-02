using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Implant", menuName = "Winter Universe/Implant/New Preset")]
    public class ImplantConfig : BasicInfoConfig
    {
        [field: SerializeField] public List<GameplayStatModifierCreator> Modifiers { get; private set; }
        //[field: SerializeField] public List<EffectCreator> Effects { get; private set; }
    }
}