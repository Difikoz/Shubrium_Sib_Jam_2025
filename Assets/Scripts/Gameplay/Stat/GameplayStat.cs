using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class GameplayStat
    {
        public GameplayStatConfig Config { get; private set; }
        public float BaseValue { get; private set; }
        public float CurrentValue { get; private set; }
        public float FlatValue { get; private set; }
        public float MultiplierValue { get; private set; }
        public List<float> FlatModifiers { get; private set; }
        public List<float> MultiplierModifiers { get; private set; }

        public GameplayStat(GameplayStatConfig config, float baseValue)
        {
            Config = config;
            BaseValue = baseValue;
            CurrentValue = baseValue;
            FlatModifiers = new();
            MultiplierModifiers = new();
        }

        public void AddModifier(GameplayStatModifier modifier)
        {
            if (modifier.Type == GameplayStatModifierType.Flat)
            {
                FlatModifiers.Add(modifier.Value);
            }
            else if (modifier.Type == GameplayStatModifierType.Multiplier)
            {
                MultiplierModifiers.Add(modifier.Value);
            }
            CalculateCurrentValue();
        }

        public void RemoveModifier(GameplayStatModifier modifier)
        {
            if (modifier.Type == GameplayStatModifierType.Flat && FlatModifiers.Contains(modifier.Value))
            {
                FlatModifiers.Remove(modifier.Value);
            }
            else if (modifier.Type == GameplayStatModifierType.Multiplier && MultiplierModifiers.Contains(modifier.Value))
            {
                MultiplierModifiers.Remove(modifier.Value);
            }
            CalculateCurrentValue();
        }

        public void CalculateCurrentValue()
        {
            Debug.Log($"{Config.DisplayName} was {CurrentValue}");
            FlatValue = 0f;
            MultiplierValue = 0f;
            foreach (float f in FlatModifiers)
            {
                FlatValue += f;
            }
            foreach (float f in MultiplierModifiers)
            {
                MultiplierValue += f;
            }
            if (MultiplierValue != 0f)
            {
                MultiplierValue *= BaseValue + FlatValue;
                MultiplierValue /= 100f;
            }
            CurrentValue = Mathf.Clamp(BaseValue + FlatValue + MultiplierValue, Config.MinValue, Config.MaxValue);
            Debug.Log($"{Config.DisplayName} now is {CurrentValue}");
        }
    }
}