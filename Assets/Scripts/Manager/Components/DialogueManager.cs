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
        }
    }
} 