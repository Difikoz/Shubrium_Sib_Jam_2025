using UnityEngine;
using System;
using System.IO;
using WinterUniverse;

namespace Seedon
{
    /// <summary>
    /// Класс для отображения консольного вывода в GUI и сохранения логов.
    /// </summary>
    public class ConsoleToGUI : Singleton<ConsoleToGUI>
    {
        private string _logContent = "*begin log";
        private string _logFilePath = "";
        private Vector2 _scrollPosition = Vector2.zero; // Добавляем переменную для хранения позиции скролла

        [Header("Настройки")]
        public bool showConsole = true;          // Показывать консоль
        public bool saveToFile = true;           // Сохранять в файл
        public bool allowKeyboardToggle = true;  // РАЗРЕШИТЬ переключение по клавише (выключать для релизного билда)
        public GameObject consolePanel;          // Панель консоли

        [Header("Параметры GUI")]
        [Range(0.1f, 1f)]
        public float consoleWidthPercent = 0.3f;    // Ширина консоли в процентах от экрана

        private string LogsFolderPath => Path.Combine(Application.streamingAssetsPath, "Logs");

        protected override void OnAwake()
        {
            // Создаем папку для логов если её нет
            if (!Directory.Exists(LogsFolderPath))
            {
                Directory.CreateDirectory(LogsFolderPath);
            }

            // Создаем новый файл лога с датой и временем
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            _logFilePath = Path.Combine(LogsFolderPath, $"log_{timestamp}.txt");

            _logContent += $"\nLog file: {_logFilePath}\n";

            // Очищаем старые логи (оставляем только за последние 7 дней)
            CleanupOldLogs();

            // Устанавливаем начальное состояние панели
            if (consolePanel != null)
                consolePanel.SetActive(showConsole);
        }

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        // Публичный метод для переключения консоли
        public void ToggleConsole()
        {
            if(!allowKeyboardToggle)
            {
                return;
            }
            showConsole = !showConsole;
            if (consolePanel != null)
                consolePanel.SetActive(showConsole);

            Debug.Log($"[{GetType().Name}] Консоль {(showConsole ? "включена" : "выключена")}");
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string logEntry = $"[{timestamp}] [{type}] {logString}";

            if (type == LogType.Error || type == LogType.Exception)
            {
                logEntry += $"\nStack Trace:\n{stackTrace}";
            }

            // Добавляем в консоль
            _logContent = $"{_logContent}\n{logEntry}\n";

            // Сохраняем в файл
            if (saveToFile && !string.IsNullOrEmpty(_logFilePath))
            {
                try
                {
                    File.AppendAllText(_logFilePath, $"{logEntry}\n");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to write to log file: {ex.Message}");
                }
            }
        }

        private void OnGUI()
        {
            if (!showConsole) return;

            float consoleWidth = Screen.width * consoleWidthPercent;
            float consoleHeight = Screen.height;

            // Расчет высоты контента для скролла
            float contentHeight = GUI.skin.textArea.CalcHeight(new GUIContent(_logContent), consoleWidth - 40);

            // Создаём скроллируемую область для текста и сохраняем позицию скролла
            _scrollPosition = GUI.BeginScrollView(
                new Rect(10, 10, consoleWidth, consoleHeight - 20),
                _scrollPosition,
                new Rect(0, 0, consoleWidth - 40, contentHeight)
            );

            GUI.TextArea(new Rect(0, 0, consoleWidth - 40, contentHeight), _logContent);

            GUI.EndScrollView();
        }

        private void CleanupOldLogs()
        {
            try
            {
                var directory = new DirectoryInfo(LogsFolderPath);
                var files = directory.GetFiles("log_*.txt");
                var cutoffDate = DateTime.Now.AddDays(-7);

                foreach (var file in files)
                {
                    if (file.CreationTime < cutoffDate)
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to cleanup old logs: {ex.Message}");
            }
        }
    }
}