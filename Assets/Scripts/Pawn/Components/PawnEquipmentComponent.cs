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
            Implants.Add(Instantiate(config));
            _pawn.GameplayComponent.AddGameplayStatModifiers(config.Modifiers);
            OnImplantsChanged?.Invoke(Implants);
        }

        public void RemoveImplant(ImplantConfig config)
        {
            foreach (ImplantConfig implant in Implants)
            {
                if (implant.Key == config.Key)
                {
                    _pawn.GameplayComponent.RemoveGameplayStatModifiers(config.Modifiers);
                    OnImplantsChanged?.Invoke(Implants);
                    break;
                }
            }
        }

        public void UpdateImplantCooldown()
        {
            for (int i = 0; i < Implants.Count; i++)
            {
                foreach (ImplantConfig implant in GameManager.StaticInstance.ImplantManager.AllImplants)
                {
                    if (Implants[i].Key == implant.Key)
                    {
                        Implants[i] = Instantiate(implant);
                        break;
                    }
                }
            }
        }
    }
}