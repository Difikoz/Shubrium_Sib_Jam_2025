using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitGameButton;

        private void Awake()
        {
            _startGameButton.onClick.AddListener(OnStartGameButtonPressed);
            _settingsButton.onClick.AddListener(OnSettingsButtonPressed);
            _quitGameButton.onClick.AddListener(OnQuitGameButtonPressed);
        }

        private void OnStartGameButtonPressed()
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }

        private void OnSettingsButtonPressed()
        {
            
        }

        private void OnQuitGameButtonPressed()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}