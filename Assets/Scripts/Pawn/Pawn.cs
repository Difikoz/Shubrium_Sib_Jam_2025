using System;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnAnimatorComponent))]
    [RequireComponent(typeof(Rigidbody))]
    public class Pawn : BasicComponentHolder
    {
        public Action<string> OnTriggerPerfomed;
        [field: SerializeField] public GameplayStatsCreatorConfig StatsCreator { get; private set; }

        public Rigidbody RB { get; private set; }
        public PawnAnimatorComponent Animator { get; private set; }
        public GameplayComponent GameplayComponent { get; private set; }

        public override void FillComponents()
        {
            GameplayComponent = new();
            GameplayComponent.CreateStats(StatsCreator.BaseStats);
            RB = GetComponent<Rigidbody>();
            Animator = GetComponent<PawnAnimatorComponent>();
            _components.Add(Animator);
        }
    }
}