using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Implant", menuName = "Winter Universe/Implant/New Implant")]
    public class ImplantConfig : BasicInfoConfig
    {
        [field: SerializeField] public bool CanStack { get; private set; }
        [field: SerializeField] public List<GameplayStatModifierCreator> Modifiers { get; private set; }
        [field: SerializeField] public List<GameplayEffectCreator> Effects { get; private set; }

        public void OnTriggerPerfomed(string trigger, Pawn owner, Pawn source)
        {
            foreach (GameplayEffectCreator effectCreator in Effects)
            {
                if (effectCreator.Trigger == trigger && effectCreator.Triggered)
                {
                    effectCreator.Effect.OnApply(owner, source);
                }
            }
        }
    }
}