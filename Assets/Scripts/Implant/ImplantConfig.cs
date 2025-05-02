using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Implant", menuName = "Winter Universe/Implant/New Preset")]
    public class ImplantConfig : BasicInfoConfig
    {
        //[field: SerializeField] public List<EffectCreator> Effects { get; private set; }

        [field: SerializeField] public List<GameplayStatModifierCreator> Modifiers { get; private set; }
        [field: SerializeField] public bool CanStack { get; private set; } = true;
        
        // Поле для механики
        [field: SerializeField] public MechanicType Mechanic { get; private set; } = MechanicType.None;
        
        // Перечисление доступных механик
        public enum MechanicType
        {
            None,           // Нет механики (только статы)
            Shield,         // Щит
            Resurrection,   // Воскрешение
            AoeReflect,     // Отражение урона по области
            // Здесь можно добавить другие типы механик
        }
        
        // Вспомогательные методы для проверки типа импланта
        public bool HasMechanic() => Mechanic != MechanicType.None;
        
        public bool HasStatsModifiers() => Modifiers != null && Modifiers.Count > 0;
    }
}