using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class DialogueManager : BasicComponent
    {
        
        public bool IsShowingDialogue { get; private set; } = false;
                
        public void ShowDialogue(DialogueConfig dialogue)
        {
            if (IsShowingDialogue || dialogue == null)
                return;
                
            IsShowingDialogue = true;
            
            // Показываем UI диалога
            GameManager.StaticInstance.UIManager.DialogueUI.ShowDialogue(dialogue, OnDialogueCompleted);
        }
        
        private void OnDialogueCompleted()
        {
            IsShowingDialogue = false;
            Debug.Log($"[{GetType().Name}] Диалог завершен");
        }
        
        // Показ диалога текущего уровня
        public void ShowCurrentStageDialogue()
        {
            Stage currentStage = GameManager.StaticInstance.StageManager.CurrentStage;
            if (currentStage != null && currentStage.StageDialogue != null)
            {
                ShowDialogue(currentStage.StageDialogue);
                Debug.Log($"[{GetType().Name}] Показан диалог для стадии: {currentStage.StageName}");
            }
            else
            {
                Debug.Log($"[{GetType().Name}] У текущей стадии нет диалога");
            }
        }
    }
} 