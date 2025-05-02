using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Pawn))]
    public class PlayerImplants : PawnComponent
    {
        [SerializeField] private int _maxImplants = 4;
        [SerializeField] private List<ImplantConfig> _appliedImplants = new List<ImplantConfig>();
        
        public override void InitializeComponent()
        {
            base.InitializeComponent();
            // Дополнительная инициализация при необходимости
        }
        
        public bool CanAddImplant(ImplantConfig implant)
        {
            if (_appliedImplants.Count >= _maxImplants)
                return false;
                
            // Если имплант имеет механику и такая механика уже есть - нельзя добавить
            if (implant.HasMechanic() && 
                _appliedImplants.Exists(x => x.Mechanic == implant.Mechanic))
                return false;
                
            // Если имплант не стакается и уже есть - нельзя добавить
            if (!implant.CanStack && _appliedImplants.Exists(x => x.ID == implant.ID))
                return false;
                
            return true;
        }
        
        public bool ApplyImplant(ImplantConfig implant)
        {
            if (!CanAddImplant(implant))
                return false;
                
            _appliedImplants.Add(implant);
            
            // Применяем модификаторы статов
            if (implant.HasStatsModifiers())
            {
                Debug.Log($"[{GetType().Name}] Applying stat modifiers from implant: {implant.DisplayName}");
                foreach (var modifier in implant.Modifiers)
                {
                    // Код применения модификатора
                    
                }
            }
            
            // Применяем механику, если она есть
            if (implant.HasMechanic())
            {
                ApplyMechanicImplant(implant);
            }
            
            Debug.Log($"[{GetType().Name}] Applied implant: {implant.DisplayName}");
            
            return true;
        }
        
        public List<ImplantConfig> GetAppliedImplants()
        {
            // Возвращаем копию списка, чтобы внешние классы не могли его изменять напрямую
            return new List<ImplantConfig>(_appliedImplants);
        }
        
        private void ApplyMechanicImplant(ImplantConfig implant)
        {
            // Используем новый тип механики вместо ID
            switch (implant.Mechanic)
            {
                case ImplantConfig.MechanicType.Shield:
                    Debug.Log($"[{GetType().Name}] Shield mechanic applied");
                    // Код для применения щита
                    break;
                case ImplantConfig.MechanicType.Resurrection:
                    Debug.Log($"[{GetType().Name}] Resurrection mechanic applied");
                    // Код для воскрешения
                    break;
                case ImplantConfig.MechanicType.AoeReflect:
                    Debug.Log($"[{GetType().Name}] AOE reflection mechanic applied");
                    // Код для отражения урона
                    break;
                case ImplantConfig.MechanicType.None:
                    Debug.Log($"[{GetType().Name}] No mechanic applied for implant: {implant.DisplayName}");
                    break;
                default:
                    Debug.Log($"[{GetType().Name}] Unknown implant mechanic: {implant.Mechanic}");
                    break;
            }
        }
    }
} 