using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

namespace WinterUniverse
{
    public class DialogueUI : BasicComponent, ISubmitHandler
    {
        [SerializeField] private CanvasGroup _uiRoot;
        [SerializeField] private TextMeshProUGUI _speakerNameText;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private Button _nextButton;

        private List<DialogueLine> _currentLines;
        private int _currentLineIndex;
        private Action _onDialogueCompleted;

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            _nextButton.onClick.AddListener(AdvanceDialogue);
        }

        public IEnumerator ShowDialogue(DialogueConfig dialogue, Action onCompleted = null)
        {
            _nextButton.Select();
            _currentLines = new(dialogue.Lines);
            _currentLineIndex = 0;
            _onDialogueCompleted = onCompleted;

            // Показываем первую строку
            DisplayCurrentLine();

            _uiRoot.gameObject.SetActive(true);

            while (_uiRoot.alpha != 1f)
            {
                _uiRoot.alpha = Mathf.MoveTowards(_uiRoot.alpha, 1f, 2f * Time.deltaTime);
                yield return null;
            }

        }

        public IEnumerator HideDialogue()
        {
            while (_uiRoot.alpha != 0f)
            {
                _uiRoot.alpha = Mathf.MoveTowards(_uiRoot.alpha, 0f, 2f * Time.deltaTime);
                yield return null;
            }
            _uiRoot.gameObject.SetActive(false);
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
            StartCoroutine(AdvanceDialogueCoroutine());
        }

        private IEnumerator AdvanceDialogueCoroutine()
        {
            if (_uiRoot.alpha == 1f)
            {
                _currentLineIndex++;
                AudioManager.StaticInstance.PlaySound("event:/ui/dialog_click");
                if (_currentLineIndex < _currentLines.Count)
                {
                    // Еще есть строки, показываем следующую
                    DisplayCurrentLine();
                }
                else
                {
                    // Диалог закончился
                    yield return HideDialogue();
                    _onDialogueCompleted?.Invoke();
                }
            }
        }

        public void OnSubmit(BaseEventData eventData)
        {
            AdvanceDialogue();
        }
    }
}