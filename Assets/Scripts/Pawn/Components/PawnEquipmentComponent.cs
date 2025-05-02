using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipmentComponent : PawnComponent
    {
        public List<ImplantConfig> Implants { get; private set; }

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            Implants = new();
        }

        public void AddImplant(ImplantConfig config)
        {
            Implants.Add(config);
            _pawn.GameplayComponent.AddGameplayStatModifiers(config.Modifiers);
        }
    }
}