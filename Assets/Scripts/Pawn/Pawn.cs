using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnAnimatorComponent))]
    [RequireComponent(typeof(PawnLocomotionComponent))]
    [RequireComponent(typeof(Rigidbody))]
    public class Pawn : BasicComponentHolder
    {
        [field: SerializeField] public GameplayStatsCreatorConfig StatsCreator { get; private set; }

        public Rigidbody RB { get; private set; }
        public PawnAnimatorComponent Animator { get; private set; }
        public PawnLocomotionComponent Locomotion { get; private set; }
        public GameplayComponent GameplayComponent { get; private set; }

        public override void FillComponents()
        {
            GameplayComponent = new();
            GameplayComponent.AddGameplayStats(StatsCreator.BaseStats);
            RB = GetComponent<Rigidbody>();
            Animator = GetComponent<PawnAnimatorComponent>();
            Locomotion = GetComponent<PawnLocomotionComponent>();
            _components.Add(Animator);
            _components.Add(Locomotion);
        }

        public void PerformTrigger(string trigger)
        {

        }
    }
}