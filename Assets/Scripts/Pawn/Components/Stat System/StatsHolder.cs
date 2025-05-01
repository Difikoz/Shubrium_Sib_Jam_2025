using System;
using System.Collections.Generic;

namespace WinterUniverse
{
    public class StatsHolder
    {
        public Action OnStatsChanged;

        public Dictionary<string, Stat> Stats { get; private set; }

        public StatsHolder(List<StatConfig> stats)
        {
            Stats = new();
            foreach (StatConfig stat in stats)
            {
                Stats.Add(stat.ID, new(stat));
            }
        }

        public void RecalculateStats()
        {
            foreach (KeyValuePair<string, Stat> s in Stats)
            {
                s.Value.CalculateCurrentValue();
            }
            OnStatsChanged?.Invoke();
        }

        public Stat GetStat(string id)
        {
            if (Stats.ContainsKey(id))
            {
                return Stats[id];
            }
            return null;
        }

        public void AddStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                AddStatModifier(smc);
            }
            RecalculateStats();
        }

        public void AddStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Config.ID).AddModifier(smc.Modifier);
        }

        public void RemoveStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                RemoveStatModifier(smc);
            }
            RecalculateStats();
        }

        public void RemoveStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Config.ID).RemoveModifier(smc.Modifier);
        }
    }
}