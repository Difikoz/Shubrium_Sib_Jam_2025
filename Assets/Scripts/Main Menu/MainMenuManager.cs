using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Основные элементы")]
        [SerializeField] private GameObject _blackScreen;
        [SerializeField] private GameObject _mainGameMenuWindow;
        [SerializeField] private float _fadeSpeed = 1.0f; // Скорость затенения экрана
        
        private CanvasGroup _blackScreenCanvasGroup;
        
        [Header("Кнопки главного меню")]
        [SerializeField] private Button _mainGameMenuButtonNewGame;
        [SerializeField] private Button _mainGameMenuButtonSettings;
        [SerializeField] private Button _mainGameMenuButtonQuitGame;
        
        [Header("Меню настроек")]
        [SerializeField] private GameObject _mainSettingsMenuWindow;
        [SerializeField] private Button _mainSettingsMenuButtonGraphicsSettings;
        [SerializeField] private Button _mainSettingsMenuButtonAudioSettings;
        [SerializeField] private Button _mainSettingsMenuButtonGameplaySettings;
        [SerializeField] private Button _mainSettingsMenuButtonBackToMainGameMenu;
        
        [Header("Графические настройки")]
        [SerializeField] private GameObject _graphicsSettingsMenuWindow;
        [SerializeField] private Button _graphicsSettingsMenuButtonBackToMainSettingsMenu;
        
        [Header("Настройки звука")]
        [SerializeField] private GameObject _audioSettingsMenuWindow;
        [SerializeField] private Button _audioSettingsMenuButtonBackToMainSettingsMenu;
        
        [Header("Настройки геймплея")]
        [SerializeField] private GameObject _gameplaySettingsMenuWindow;
        [SerializeField] private Button _gameplaySettingsMenuButtonBackToMainSettingsMenu;

        private IEnumerator Start()
        {
            // Инициализация черного экрана и проверка всех окон
            InitBlackScreen();
            SafeShowAllMenuWindows();
            SafeHideAllMenuWindows();
            SafeAddButtonListeners();
            
            // Плавно скрываем черный экран
            yield return StartCoroutine(FadeOut());
            
            // Показываем главное меню
            if (_mainGameMenuWindow != null)
            {
                _mainGameMenuWindow.SetActive(true);
            }
            else
            {
                Debug.LogError($"[{GetType().Name}] Отсутствует ссылка на _mainGameMenuWindow");
            }
        }
        
        // Инициализация CanvasGroup для черного экрана
        private void InitBlackScreen()
        {
            if (_blackScreen != null)
            {
                _blackScreen.SetActive(true);
                _blackScreenCanvasGroup = _blackScreen.GetComponent<CanvasGroup>();
                
                // Если на объекте нет CanvasGroup, добавляем его
                if (_blackScreenCanvasGroup == null)
                {
                    _blackScreenCanvasGroup = _blackScreen.AddComponent<CanvasGroup>();
                    Debug.Log($"[{GetType().Name}] Добавлен CanvasGroup на _blackScreen");
                }
                
                // Устанавливаем начальную непрозрачность
                _blackScreenCanvasGroup.alpha = 1f;
            }
            else
            {
                Debug.LogWarning($"[{GetType().Name}] Отсутствует ссылка на _blackScreen");
            }
        }
        
        // Плавное появление черного экрана (от прозрачного к непрозрачному)
        private IEnumerator FadeIn()
        {
            if (_blackScreen == null || _blackScreenCanvasGroup == null)
                yield break;
                
            _blackScreen.SetActive(true);
            _blackScreenCanvasGroup.alpha = 0f;
            
            while (_blackScreenCanvasGroup.alpha < 1f)
            {
                _blackScreenCanvasGroup.alpha += Time.deltaTime * _fadeSpeed;
                yield return null;
            }
            
            _blackScreenCanvasGroup.alpha = 1f;
        }
        
        // Плавное исчезновение черного экрана (от непрозрачного к прозрачному)
        private IEnumerator FadeOut()
        {
            if (_blackScreen == null || _blackScreenCanvasGroup == null)
                yield break;
                
            _blackScreenCanvasGroup.alpha = 1f;
            
            while (_blackScreenCanvasGroup.alpha > 0f)
            {
                _blackScreenCanvasGroup.alpha -= Time.deltaTime * _fadeSpeed;
                yield return null;
            }
            
            _blackScreenCanvasGroup.alpha = 0f;
            _blackScreen.SetActive(false);
        }
        
        // Безопасное добавление всех обработчиков событий
        private void SafeAddButtonListeners()
        {
            if (_mainGameMenuButtonNewGame != null)
                _mainGameMenuButtonNewGame.onClick.AddListener(OnMainGameMenuButtonNewGamePressed);
            else
                Debug.LogWarning("[MainMenuManager] Отсутствует ссылка на _mainGameMenuButtonNewGame");
                
            if (_mainGameMenuButtonSettings != null)
                _mainGameMenuButtonSettings.onClick.AddListener(OnMainGameMenuButtonSettingsPressed);
            else
                Debug.LogWarning("[MainMenuManager] Отсутствует ссылка на _mainGameMenuButtonSettings");
                
            if (_mainGameMenuButtonQuitGame != null)
                _mainGameMenuButtonQuitGame.onClick.AddListener(OnMainGameMenuButtonQuitGamePressed);
            else
                Debug.LogWarning("[MainMenuManager] Отсутствует ссылка на _mainGameMenuButtonQuitGame");
                
            if (_mainSettingsMenuButtonGraphicsSettings != null)
                _mainSettingsMenuButtonGraphicsSettings.onClick.AddListener(OnMainSettingsMenuButtonGraphicsSettingsPressed);
            else
                Debug.LogWarning("[MainMenuManager] Отсутствует ссылка на _mainSettingsMenuButtonGraphicsSettings");
                
            if (_mainSettingsMenuButtonAudioSettings != null)
                _mainSettingsMenuButtonAudioSettings.onClick.AddListener(OnMainSettingsMenuButtonAudioSettingsPressed);
            else
                Debug.LogWarning("[MainMenuManager] Отсутствует ссылка на _mainSettingsMenuButtonAudioSettings");
                
            if (_mainSettingsMenuButtonGameplaySettings != null)
                _mainSettingsMenuButtonGameplaySettings.onClick.AddListener(OnMainSettingsMenuButtonGameplaySettingsPressed);
            else
                Debug.LogWarning("[MainMenuManager] Отсутствует ссылка на _mainSettingsMenuButtonGameplaySettings");
                
            if (_mainSettingsMenuButtonBackToMainGameMenu != null)
                _mainSettingsMenuButtonBackToMainGameMenu.onClick.AddListener(OnMainSettingsMenuButtonBackToMainGameMenuPressed);
            else
                Debug.LogWarning("[MainMenuManager] Отсутствует ссылка на _mainSettingsMenuButtonBackToMainGameMenu");
                
            if (_graphicsSettingsMenuButtonBackToMainSettingsMenu != null)
                _graphicsSettingsMenuButtonBackToMainSettingsMenu.onClick.AddListener(OnGraphicsSettingsMenuButtonBackToMainSettingsMenuPressed);
            else
                Debug.LogWarning("[MainMenuManager] Отсутствует ссылка на _graphicsSettingsMenuButtonBackToMainSettingsMenu");
                
            if (_audioSettingsMenuButtonBackToMainSettingsMenu != null)
                _audioSettingsMenuButtonBackToMainSettingsMenu.onClick.AddListener(OnAudioSettingsMenuButtonBackToMainSettingsMenuPressed);
            else
                Debug.LogWarning("[MainMenuManager] Отсутствует ссылка на _audioSettingsMenuButtonBackToMainSettingsMenu");
                
            if (_gameplaySettingsMenuButtonBackToMainSettingsMenu != null)
                _gameplaySettingsMenuButtonBackToMainSettingsMenu.onClick.AddListener(OnGameplaySettingsMenuButtonBackToMainSettingsMenuPressed);
            else
                Debug.LogWarning("[MainMenuManager] Отсутствует ссылка на _gameplaySettingsMenuButtonBackToMainSettingsMenu");
        }

        private void SafeShowAllMenuWindows()
        {
            if (_mainGameMenuWindow != null) _mainGameMenuWindow.SetActive(true);
            if (_mainSettingsMenuWindow != null) _mainSettingsMenuWindow.SetActive(true);
            if (_graphicsSettingsMenuWindow != null) _graphicsSettingsMenuWindow.SetActive(true);
            if (_audioSettingsMenuWindow != null) _audioSettingsMenuWindow.SetActive(true);
            if (_gameplaySettingsMenuWindow != null) _gameplaySettingsMenuWindow.SetActive(true);
        }

        private void SafeHideAllMenuWindows()
        {
            if (_mainGameMenuWindow != null) _mainGameMenuWindow.SetActive(false);
            if (_mainSettingsMenuWindow != null) _mainSettingsMenuWindow.SetActive(false);
            if (_graphicsSettingsMenuWindow != null) _graphicsSettingsMenuWindow.SetActive(false);
            if (_audioSettingsMenuWindow != null) _audioSettingsMenuWindow.SetActive(false);
            if (_gameplaySettingsMenuWindow != null) _gameplaySettingsMenuWindow.SetActive(false);
        }

        public IEnumerator NewGame()
        {
            yield return StartCoroutine(FadeIn());
            SceneManager.LoadScene(1);
        }

        private void OnMainGameMenuButtonNewGamePressed()
        {
            StartCoroutine(NewGame());
        }

        private void OnMainGameMenuButtonSettingsPressed()
        {
            if (_mainGameMenuWindow != null) _mainGameMenuWindow.SetActive(false);
            if (_mainSettingsMenuWindow != null) _mainSettingsMenuWindow.SetActive(true);
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
            if (_mainGameMenuWindow != null) _mainGameMenuWindow.SetActive(true);
            if (_mainSettingsMenuWindow != null) _mainSettingsMenuWindow.SetActive(false);
        }

        private void OnMainSettingsMenuButtonGraphicsSettingsPressed()
        {
            if (_mainSettingsMenuWindow != null) _mainSettingsMenuWindow.SetActive(false);
            if (_graphicsSettingsMenuWindow != null) _graphicsSettingsMenuWindow.SetActive(true);
        }

        private void OnMainSettingsMenuButtonAudioSettingsPressed()
        {
            if (_mainSettingsMenuWindow != null) _mainSettingsMenuWindow.SetActive(false);
            if (_audioSettingsMenuWindow != null) _audioSettingsMenuWindow.SetActive(true);
        }

        private void OnMainSettingsMenuButtonGameplaySettingsPressed()
        {
            if (_mainSettingsMenuWindow != null) _mainSettingsMenuWindow.SetActive(false);
            if (_gameplaySettingsMenuWindow != null) _gameplaySettingsMenuWindow.SetActive(true);
        }

        private void OnGraphicsSettingsMenuButtonBackToMainSettingsMenuPressed()
        {
            if (_mainSettingsMenuWindow != null) _mainSettingsMenuWindow.SetActive(true);
            if (_graphicsSettingsMenuWindow != null) _graphicsSettingsMenuWindow.SetActive(false);
        }

        private void OnAudioSettingsMenuButtonBackToMainSettingsMenuPressed()
        {
            if (_mainSettingsMenuWindow != null) _mainSettingsMenuWindow.SetActive(true);
            if (_audioSettingsMenuWindow != null) _audioSettingsMenuWindow.SetActive(false);
        }

        private void OnGameplaySettingsMenuButtonBackToMainSettingsMenuPressed()
        {
            if (_mainSettingsMenuWindow != null) _mainSettingsMenuWindow.SetActive(true);
            if (_gameplaySettingsMenuWindow != null) _gameplaySettingsMenuWindow.SetActive(false);
        }
    }
}