using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class StageManager : BasicComponentHolder
    {
        [field: SerializeField] public List<Stage> Stages { get; private set; }
        public int CurrentStageIndex { get; private set; }

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            CurrentStageIndex = -1;
            foreach(Stage stage in Stages)
            {
                stage.InitializeComponent();
                stage.DeactivateComponent();
            }
        }

        public void StartNextStage()
        {
            CurrentStageIndex++;
            _components.Add(Stages[CurrentStageIndex]);
            Stages[CurrentStageIndex].ActivateComponent();
            Stages[CurrentStageIndex].EnableComponent();
            GameManager.StaticInstance.SpawnManager.SpawnEnemies(Stages[CurrentStageIndex]);
            StartCoroutine(HandleStagesCoroutine());
        }

        private IEnumerator HandleStagesCoroutine()
        {
            WaitForSeconds delay = new(5f);
            yield return delay;
            while (!Stages[CurrentStageIndex].CanCompleteStage())
            {
                yield return delay;
            }
            Stages[CurrentStageIndex].CompleteStage();
            Stages[CurrentStageIndex].DisableComponent();
            Stages[CurrentStageIndex].DeactivateComponent();
            _components.Remove(Stages[CurrentStageIndex]);
            if (CurrentStageIndex == Stages.Count - 1)
            {
                Debug.Log("ТЫ ПРОШЁЛ ИГРУ, ЕЕЕ");
                // вызвать какой-нибудь метод
            }
            else
            {
                StartNextStage();
            }
        }
    }
}