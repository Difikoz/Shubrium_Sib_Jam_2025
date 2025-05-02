using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Implant", menuName = "Winter Universe/Implant/New Implant")]
    public class ImplantConfig : BasicInfoConfig
    {
        [field: SerializeField] public bool CanStack { get; private set; }
        [field: SerializeField] public bool RemoveAfterAnyTriggerPerfomed { get; private set; }
        [field: SerializeField] public List<GameplayStatModifierCreator> Modifiers { get; private set; }
        [field: SerializeField] public List<GameplayEffectCreator> Effects { get; private set; }

        public void OnTriggerPerfomed(string trigger, Pawn owner, Pawn source)
        {
            for (int i = Effects.Count - 1; i >= 0; i--)
            {
                if (Effects[i].Trigger.Key == trigger && Effects[i].Triggered)
                {
                    Effects[i].Effect.OnApply(owner, source);
                    if (Effects[i].SingleUse)
                    {
                        Effects.RemoveAt(i);
                    }
                }
            }
            if (RemoveAfterAnyTriggerPerfomed)
            {
                owner.Equipment.RemoveImplant(this);
            }
        }
    }
}