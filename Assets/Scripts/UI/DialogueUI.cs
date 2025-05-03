using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class DialogueUI : BasicComponent
    {
        [SerializeField] private GameObject _uiRoot;
        [SerializeField] private TextMeshProUGUI _speakerNameText;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private Button _nextButton;
        
        private List<DialogueLine> _currentLines;
        private int _currentLineIndex;
        private Action _onDialogueCompleted;
        
        public override void InitializeComponent()
        {
            _nextButton.onClick.AddListener(AdvanceDialogue);
            
            // Явно выключаем UI-корень
            if (_uiRoot != null)
            {
                _uiRoot.SetActive(false);
            }
        }
        
        public override void EnableComponent()
        {
            
        }
        
        public override void DisableComponent()
        {
            
        }
        
        public override void UpdateComponent()
        {
            
        }
        
        public void ShowDialogue(DialogueConfig dialogue, Action onCompleted = null)
        {
            if (dialogue == null || dialogue.Lines.Count == 0)
            {
                Debug.LogError($"[{GetType().Name}] Попытка показать пустой диалог");
                return;
            }
            
            _currentLines = dialogue.Lines;
            _currentLineIndex = 0;
            _onDialogueCompleted = onCompleted;
            
            // Показываем первую строку
            DisplayCurrentLine();
            
            _uiRoot.SetActive(true);
        }
        
        public void HideDialogue()
        {
            _uiRoot.SetActive(false);
            _currentLines = null;
        }
        
        private void DisplayCurrentLine()
        {
            if (_currentLineIndex < _currentLines.Count)
            {
                DialogueLine line = _currentLines[_currentLineIndex];
                _speakerNameText.text = line.SpeakerName;
                _dialogueText.text = line.Text;
            }
        }
        
        private void AdvanceDialogue()
        {
            _currentLineIndex++;
            
            if (_currentLineIndex < _currentLines.Count)
            {
                // Еще есть строки, показываем следующую
                DisplayCurrentLine();
            }
            else
            {
                // Диалог закончился
                _onDialogueCompleted?.Invoke();
                HideDialogue();
            }
        }
    }
} 