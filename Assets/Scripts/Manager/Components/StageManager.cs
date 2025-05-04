using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class StageManager : BasicComponentHolder
    {
        [SerializeField] private int startStageIndex;

        [field: SerializeField] public List<Stage> Stages { get; private set; }
        public int CurrentStageIndex { get; private set; }
        public Stage CurrentStage => Stages[CurrentStageIndex];
        public bool LastStage => CurrentStageIndex == Stages.Count - 1;

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            CurrentStageIndex = startStageIndex - 1;//-1;
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
            GameManager.StaticInstance.Player.Equipment.UpdateImplantCooldown();
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
            GameManager.StaticInstance.ElevatorManager.OpenDoors();
            GameManager.StaticInstance.DialogueManager.ShowDialogue(GameManager.StaticInstance.StageManager.CurrentStage.DialogueAfterBattle);
            GameManager.StaticInstance.SetInputMode(InputMode.UI);
            while (GameManager.StaticInstance.DialogueManager.IsShowingDialogue)
            {
                yield return delay;
            }
            GameManager.StaticInstance.SetInputMode(InputMode.Game);
        }
    }
}