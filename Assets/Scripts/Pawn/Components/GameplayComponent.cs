using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class GameplayComponent
    {
        public Action<string, GameplayStat> OnStatsChanged;

        public Dictionary<string, GameplayStat> GameplayStats { get; private set; }

        public void CreateStats(List<StatCreator> statCreators)
        {
            GameplayStats = new();
            foreach (StatCreator statCreator in statCreators)
            {
                GameplayStats.Add(statCreator.Stat.ID, new(statCreator.Stat, statCreator.BaseValue));
            }
        }

        public void RecalculateGameplayStats()
        {
            foreach (KeyValuePair<string, GameplayStat> s in GameplayStats)
            {
                s.Value.CalculateCurrentValue();
            }
            //OnStatsChanged?.Invoke(GameplayStats);
        }

        public GameplayStat GetGameplayStat(string id)
        {
            if (GameplayStats.ContainsKey(id))
            {
                return GameplayStats[id];
            }
            return null;
        }

        public void AddGameplayStatModifiers(List<GameplayStatModifierCreator> modifiers)
        {
            foreach (GameplayStatModifierCreator smc in modifiers)
            {
                AddGameplayStatModifier(smc);
            }
            RecalculateGameplayStats();
        }

        public void AddGameplayStatModifier(GameplayStatModifierCreator smc)
        {
            GetGameplayStat(smc.Config.ID).AddModifier(smc.Modifier);
        }

        public void RemoveGameplayStatModifiers(List<GameplayStatModifierCreator> modifiers)
        {
            foreach (GameplayStatModifierCreator smc in modifiers)
            {
                RemoveGameplayStatModifier(smc);
            }
            RecalculateGameplayStats();
        }

        public void RemoveGameplayStatModifier(GameplayStatModifierCreator smc)
        {
            GetGameplayStat(smc.Config.ID).RemoveModifier(smc.Modifier);
        }
    }
}