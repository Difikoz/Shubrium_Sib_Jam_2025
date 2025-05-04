using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class OutroManager : MonoBehaviour
    {
        [SerializeField] private DialogueUI _dialogueUI;
        [SerializeField] private DialogueConfig _dialogueConfig;

        private void Start()
        {
            _dialogueUI.InitializeComponent();
            StartCoroutine(ProccessCoroutine());
        }

        private IEnumerator ProccessCoroutine()
        {
            yield return new WaitForSeconds(3f);
            _dialogueUI.ShowDialogue(_dialogueConfig, OnDialogComplete);
        }

        private void OnDialogComplete()
        {

        }
    }
}