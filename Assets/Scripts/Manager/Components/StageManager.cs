using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class StageManager : BasicComponentHolder
    {
        [field: SerializeField] public List<Stage> Stages { get; private set; }
        public int CurrentStageIndex { get; private set; }
        public Stage CurrentStage => Stages[CurrentStageIndex];

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            CurrentStageIndex = -1;
            foreach (Stage stage in Stages)
            {
                stage.InitializeComponent();
                //stage.DeactivateComponent();
            }
        }

        public void StartNextStage()
        {
            CurrentStageIndex++;
            _components.Add(CurrentStage);
            CurrentStage.ActivateComponent();
            CurrentStage.EnableComponent();
            GameManager.StaticInstance.SpawnManager.SpawnEnemies(CurrentStage);
            StartCoroutine(HandleStagesCoroutine());
        }

        public void DisableCurrentStage()
        {
            CurrentStage.DisableComponent();
            CurrentStage.DeactivateComponent();
            _components.Remove(CurrentStage);
        }

        private IEnumerator HandleStagesCoroutine()
        {
            WaitForSeconds delay = new(2f);
            yield return delay;
            while (!CurrentStage.CanCompleteStage())
            {
                yield return delay;
            }
            CurrentStage.CompleteStage();
            if (CurrentStageIndex == Stages.Count - 1)
            {
                GameManager.StaticInstance.GameComplete();
            }
            else
            {
                GameManager.StaticInstance.ElevatorManager.OpenDoors();
            }
        }
    }
}