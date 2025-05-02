using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipmentComponent : PawnComponent
    {
        public Action<List<ImplantConfig>> OnImplantsChanged;

        public List<ImplantConfig> Implants { get; private set; }

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            Implants = new();
        }

        public bool CanAddImplant(ImplantConfig implant)
        {
            return implant.CanStack || !Implants.Contains(implant);
        }

        public void AddImplant(ImplantConfig config)
        {
            Implants.Add(config);
            _pawn.GameplayComponent.AddGameplayStatModifiers(config.Modifiers);
            OnImplantsChanged?.Invoke(Implants);
        }
    }
}