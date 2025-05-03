using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnAnimatorComponent))]
    [RequireComponent(typeof(PawnCombatComponent))]
    [RequireComponent(typeof(PawnEquipmentComponent))]
    [RequireComponent(typeof(PawnHealthComponent))]
    [RequireComponent(typeof(PawnLocomotionComponent))]
    public abstract class Pawn : BasicComponentHolder
    {
        [field: SerializeField] public GameplayStatsCreatorConfig StatsCreator { get; private set; }

        public PawnAnimatorComponent Animator { get; private set; }
        public PawnCombatComponent Combat { get; private set; }
        public PawnEquipmentComponent Equipment { get; private set; }
        public PawnHealthComponent Health { get; private set; }
        public PawnLocomotionComponent Locomotion { get; private set; }
        public GameplayComponent GameplayComponent { get; private set; }

        public override void FillComponents()
        {
            GameplayComponent = new();
            GameplayComponent.AddGameplayStats(StatsCreator.BaseStats);
            Animator = GetComponent<PawnAnimatorComponent>();
            Combat = GetComponent<PawnCombatComponent>();
            Equipment = GetComponent<PawnEquipmentComponent>();
            Health = GetComponent<PawnHealthComponent>();
            Locomotion = GetComponent<PawnLocomotionComponent>();
            _components.Add(Animator);
            _components.Add(Combat);
            _components.Add(Equipment);
            _components.Add(Health);
            _components.Add(Locomotion);
        }

        public void PerformTrigger(string trigger, Pawn targetOrSource)
        {
            foreach (ImplantConfig implant in Equipment.Implants)
            {
                implant.OnTriggerPerfomed(trigger, this, targetOrSource);
            }
        }

        public abstract IEnumerator PerformDeath();
    }
}