using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class OutroManager : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;
        [SerializeField] private DialogueUI _dialogueUI;
        [SerializeField] private DialogueConfig _dialogueConfig;

        private void Start()
        {
            _dialogueUI.InitializeComponent();
            StartCoroutine(ProccessCoroutine());
        }

        private IEnumerator ProccessCoroutine()
        {
            yield return FadeScreen(0f);
            yield return new WaitForSeconds(3f);
            StartCoroutine(_dialogueUI.ShowDialogue(_dialogueConfig, OnDialogComplete));
        }

        private void OnDialogComplete()
        {
            StartCoroutine(LoadMainMenuScene());
        }

        public IEnumerator FadeScreen(float value)
        {
            Color c = _fadeImage.color;
            while (c.a != value)
            {
                c.a = Mathf.MoveTowards(c.a, value, 2f * Time.deltaTime);
                _fadeImage.color = c;
                yield return null;
            }
        }

        private IEnumerator LoadMainMenuScene()
        {
            yield return FadeScreen(1f);
            AudioManager.StaticInstance.ChangeBackgroundMusic(0);
            SceneManager.LoadScene(0);
        }
    }
}