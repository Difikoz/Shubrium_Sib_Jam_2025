using System;
using System.Collections.Generic;

namespace WinterUniverse
{
    public class GameplayComponent
    {
        public Action<Dictionary<string, GameplayStat>> OnStatsChanged;
        public Action<List<string>> OnTagsChanged;

        public Dictionary<string, GameplayStat> GameplayStats;
        public List<string> GameplayTags;

        public GameplayComponent()
        {
            GameplayStats = new();
            GameplayTags = new();
        }

        public void AddGameplayStats(List<GameplayStatCreator> statCreators)
        {
            foreach (GameplayStatCreator statCreator in statCreators)
            {
                AddGameplayStat(statCreator);
            }
        }

        public void AddGameplayStat(GameplayStatCreator statCreator)
        {
            if (HasGameplayStat(statCreator.Stat.Key))
            {
                return;
            }
            GameplayStats.Add(statCreator.Stat.Key, new(statCreator.Stat, statCreator.BaseValue));
            OnStatsChanged?.Invoke(GameplayStats);
        }

        public bool HasGameplayStat(string key)
        {
            return GameplayStats.ContainsKey(key);
        }

        public GameplayStat GetGameplayStat(string key)
        {
            if (HasGameplayStat(key))
            {
                return GameplayStats[key];
            }
            else
            {
                return null;
            }
        }

        public void AddGameplayStatModifiers(List<GameplayStatModifierCreator> statModifierCreators)
        {
            if (statModifierCreators == null || statModifierCreators.Count == 0)
            {
                return;
            }
            foreach (GameplayStatModifierCreator smc in statModifierCreators)
            {
                AddGameplayStatModifier(smc);
            }
        }

        public void AddGameplayStatModifier(GameplayStatModifierCreator statModifierCreator)
        {
            GetGameplayStat(statModifierCreator.Config.Key).AddModifier(statModifierCreator.Modifier);
            OnStatsChanged?.Invoke(GameplayStats);
        }

        public void RemoveGameplayStatModifiers(List<GameplayStatModifierCreator> statModifierCreators)
        {
            if (statModifierCreators == null || statModifierCreators.Count == 0)
            {
                return;
            }
            foreach (GameplayStatModifierCreator smc in statModifierCreators)
            {
                RemoveGameplayStatModifier(smc);
            }
        }

        public void RemoveGameplayStatModifier(GameplayStatModifierCreator statModifierCreator)
        {
            GetGameplayStat(statModifierCreator.Config.Key).RemoveModifier(statModifierCreator.Modifier);
            OnStatsChanged?.Invoke(GameplayStats);
        }

        public void RecalculateGameplayStats()
        {
            foreach (KeyValuePair<string, GameplayStat> stat in GameplayStats)
            {
                stat.Value.CalculateCurrentValue();
            }
            OnStatsChanged?.Invoke(GameplayStats);
        }

        public void AddGameplayTags(List<GameplayTag> tags)
        {
            foreach (GameplayTag tag in tags)
            {
                AddGameplayTag(tag.Key);
            }
        }

        public void AddGameplayTags(List<string> tags)
        {
            foreach (string tag in tags)
            {
                AddGameplayTag(tag);
            }
        }

        public void AddGameplayTag(string tag)
        {
            if (HasGameplayTag(tag))
            {
                return;
            }
            GameplayTags.Add(tag);
            OnTagsChanged?.Invoke(GameplayTags);
        }

        public void RemoveGameplayTags(List<GameplayTag> tags)
        {
            foreach (GameplayTag tag in tags)
            {
                RemoveGameplayTag(tag.Key);
            }
        }

        public void RemoveGameplayTags(List<string> tags)
        {
            foreach (string tag in tags)
            {
                RemoveGameplayTag(tag);
            }
        }

        public void RemoveGameplayTag(string key)
        {
            if (HasGameplayTag(key))
            {
                GameplayTags.Remove(key);
                OnTagsChanged?.Invoke(GameplayTags);
            }
        }

        public bool HasGameplayTag(string key)
        {
            return GameplayTags.Contains(key);
        }
    }
}