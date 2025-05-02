using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class StageManager : BasicComponent
    {
        [field: SerializeField] public List<Stage> Stages { get; private set; }
        public int CurrentStageIndex { get; private set; }

        public override void InitializeComponent()
        {
            CurrentStageIndex = -1;
            foreach (Stage stage in Stages)
            {
                stage.InitializeComponent();
            }
            StartNextStage();
        }

        public override void ActivateComponent()
        {
            StartCoroutine(HandleStagesCoroutine());
        }

        public void StartNextStage()
        {
            CurrentStageIndex++;
            Stages[CurrentStageIndex].ActivateComponent();
            GameManager.StaticInstance.SpawnManager.SpawnEnemies(Stages[CurrentStageIndex]);
        }

        private IEnumerator HandleStagesCoroutine()
        {
            WaitForSeconds delay = new(1f);
            while (true)
            {
                if (Stages[CurrentStageIndex].CanCompleteStage())
                {
                    Stages[CurrentStageIndex].CompleteStage();
                    Stages[CurrentStageIndex].DeactivateComponent();
                    StartNextStage();
                }
                yield return delay;
            }
        }
    }
}