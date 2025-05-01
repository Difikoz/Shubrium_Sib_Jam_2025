using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _blackScreen;
        [SerializeField] private GameObject _mainGameMenuWindow;
        [SerializeField] private Button _mainGameMenuButtonNewGame;
        [SerializeField] private Button _mainGameMenuButtonSettings;
        [SerializeField] private Button _mainGameMenuButtonQuitGame;
        [SerializeField] private GameObject _mainSettingsMenuWindow;
        [SerializeField] private Button _mainSettingsMenuButtonGraphicsSettings;
        [SerializeField] private Button _mainSettingsMenuButtonAudioSettings;
        [SerializeField] private Button _mainSettingsMenuButtonGameplaySettings;
        [SerializeField] private Button _mainSettingsMenuButtonBackToMainGameMenu;
        [SerializeField] private GameObject _graphicsSettingsMenuWindow;
        [SerializeField] private Button _graphicsSettingsMenuButtonBackToMainSettingsMenu;
        [SerializeField] private GameObject _audioSettingsMenuWindow;
        [SerializeField] private Button _audioSettingsMenuButtonBackToMainSettingsMenu;
        [SerializeField] private GameObject _gameplaySettingsMenuWindow;
        [SerializeField] private Button _gameplaySettingsMenuButtonBackToMainSettingsMenu;

        private IEnumerator Start()
        {
            _blackScreen.SetActive(true);
            yield return new WaitForSeconds(1f);
            ShowAllMenuWindows();
            yield return new WaitForSeconds(0.1f);
            HideAllMenuWindows();
            yield return new WaitForSeconds(0.1f);
            _mainGameMenuButtonNewGame.onClick.AddListener(OnMainGameMenuButtonNewGamePressed);
            _mainGameMenuButtonSettings.onClick.AddListener(OnMainGameMenuButtonSettingsPressed);
            _mainGameMenuButtonQuitGame.onClick.AddListener(OnMainGameMenuButtonQuitGamePressed);
            _mainSettingsMenuButtonGraphicsSettings.onClick.AddListener(OnMainSettingsMenuButtonGraphicsSettingsPressed);
            _mainSettingsMenuButtonAudioSettings.onClick.AddListener(OnMainSettingsMenuButtonAudioSettingsPressed);
            _mainSettingsMenuButtonGameplaySettings.onClick.AddListener(OnMainSettingsMenuButtonGameplaySettingsPressed);
            _mainSettingsMenuButtonBackToMainGameMenu.onClick.AddListener(OnMainSettingsMenuButtonBackToMainGameMenuPressed);
            _graphicsSettingsMenuButtonBackToMainSettingsMenu.onClick.AddListener(OnGraphicsSettingsMenuButtonBackToMainSettingsMenuPressed);
            _audioSettingsMenuButtonBackToMainSettingsMenu.onClick.AddListener(OnAudioSettingsMenuButtonBackToMainSettingsMenuPressed);
            _gameplaySettingsMenuButtonBackToMainSettingsMenu.onClick.AddListener(OnGameplaySettingsMenuButtonBackToMainSettingsMenuPressed);
            yield return new WaitForSeconds(0.1f);
            _blackScreen.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            _mainGameMenuWindow.SetActive(true);
        }

        private void ShowAllMenuWindows()
        {
            _mainGameMenuWindow.SetActive(true);
            _mainSettingsMenuWindow.SetActive(true);
            _graphicsSettingsMenuWindow.SetActive(true);
            _audioSettingsMenuWindow.SetActive(true);
            _gameplaySettingsMenuWindow.SetActive(true);
        }

        private void HideAllMenuWindows()
        {
            _mainGameMenuWindow.SetActive(false);
            _mainSettingsMenuWindow.SetActive(false);
            _graphicsSettingsMenuWindow.SetActive(false);
            _audioSettingsMenuWindow.SetActive(false);
            _gameplaySettingsMenuWindow.SetActive(false);
        }

        public IEnumerator NewGame()
        {
            _blackScreen.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            SceneManager.LoadScene(1);
        }

        private void OnMainGameMenuButtonNewGamePressed()
        {
            StartCoroutine(NewGame());
        }

        private void OnMainGameMenuButtonSettingsPressed()
        {
            _mainGameMenuWindow.SetActive(false);
            _mainSettingsMenuWindow.SetActive(true);
        }

        private void OnMainGameMenuButtonQuitGamePressed()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void OnMainSettingsMenuButtonBackToMainGameMenuPressed()
        {
            _mainGameMenuWindow.SetActive(true);
            _mainSettingsMenuWindow.SetActive(false);
        }

        private void OnMainSettingsMenuButtonGraphicsSettingsPressed()
        {
            _mainSettingsMenuWindow.SetActive(false);
            _graphicsSettingsMenuWindow.SetActive(true);
        }

        private void OnMainSettingsMenuButtonAudioSettingsPressed()
        {
            _mainSettingsMenuWindow.SetActive(false);
            _audioSettingsMenuWindow.SetActive(true);
        }

        private void OnMainSettingsMenuButtonGameplaySettingsPressed()
        {
            _mainSettingsMenuWindow.SetActive(false);
            _gameplaySettingsMenuWindow.SetActive(true);
        }

        private void OnGraphicsSettingsMenuButtonBackToMainSettingsMenuPressed()
        {
            _mainSettingsMenuWindow.SetActive(true);
            _graphicsSettingsMenuWindow.SetActive(false);
        }

        private void OnAudioSettingsMenuButtonBackToMainSettingsMenuPressed()
        {
            _mainSettingsMenuWindow.SetActive(true);
            _audioSettingsMenuWindow.SetActive(false);
        }

        private void OnGameplaySettingsMenuButtonBackToMainSettingsMenuPressed()
        {
            _mainSettingsMenuWindow.SetActive(true);
            _gameplaySettingsMenuWindow.SetActive(false);
        }
    }
}