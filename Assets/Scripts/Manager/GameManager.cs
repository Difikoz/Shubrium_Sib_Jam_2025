using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        public bool Initialized { get; private set; }
        [field: SerializeField] public PlayerController Player { get; private set; }
        [field: SerializeField] public CameraManager CameraManager { get; private set; }
        [field: SerializeField] public DialogueManager DialogueManager { get; private set; }
        [field: SerializeField] public ElevatorManager ElevatorManager { get; private set; }
        [field: SerializeField] public ImplantManager ImplantManager { get; private set; }
        [field: SerializeField] public InputActionsManager InputActionsManager { get; private set; }
        [field: SerializeField] public LayersManager LayersManager { get; private set; }
        [field: SerializeField] public SpawnManager SpawnManager { get; private set; }
        [field: SerializeField] public StageManager StageManager { get; private set; }
        [field: SerializeField] public UIManager UIManager { get; private set; }
        public InputMode InputMode { get; private set; }

        protected override void OnAwake()
        {
            StartCoroutine(Initialization());
        }

        public override void FillComponents()
        {
            Player = FindFirstObjectByType<PlayerController>();
            CameraManager = FindFirstObjectByType<CameraManager>();
            DialogueManager = FindFirstObjectByType<DialogueManager>();
            ElevatorManager = FindFirstObjectByType<ElevatorManager>();
            ImplantManager = FindFirstObjectByType<ImplantManager>();
            InputActionsManager = FindFirstObjectByType<InputActionsManager>();
            LayersManager = FindFirstObjectByType<LayersManager>();
            SpawnManager = FindFirstObjectByType<SpawnManager>();
            StageManager = FindFirstObjectByType<StageManager>();
            UIManager = FindFirstObjectByType<UIManager>();
            _components.Add(Player);
            _components.Add(CameraManager);
            _components.Add(DialogueManager);
            _components.Add(ElevatorManager);
            _components.Add(ImplantManager);
            _components.Add(InputActionsManager);
            _components.Add(LayersManager);
            _components.Add(SpawnManager);
            _components.Add(StageManager);
            _components.Add(UIManager);
        }

        private IEnumerator Initialization()
        {
            WaitForSeconds delay = new(0.1f);
            AudioManager.StaticInstance.ChangeBackgroundMusic(1);
            yield return new WaitForSeconds(1f);
            InitializeComponent();
            //_components = new();
            //FillComponents();
            //foreach (BasicComponent compoonent in _components)
            //{
            //    Debug.Log($"Try Initialize {compoonent.gameObject.name}");
            //    compoonent.InitializeComponent();
            //    Debug.Log($"{compoonent.gameObject.name} Initialized");
            //}
            yield return delay;
            ActivateComponent();
            yield return delay;
            EnableComponent();
            yield return delay;
            Player.DeactivateComponent();
            StageManager.StartNextStage();
            yield return delay;
            StageManager.CurrentStage.TeleportPlayerToStartPoint();
            yield return delay;
            Player.ActivateComponent();
            Player.Health.Revive(Player);
            yield return UIManager.FadeScreen(0f);
            DialogueManager.ShowDialogue(StageManager.CurrentStage.DialogueBeforeBattle);
            while (DialogueManager.IsShowingDialogue)
            {
                yield return delay;
            }
            Initialized = true;
            SetInputMode(InputMode.Game);
        }

        private void Update()
        {
            if (!Initialized)
            {
                return;
            }
            UpdateComponent();
        }

        private void FixedUpdate()
        {
            if (!Initialized)
            {
                return;
            }
            FixedUpdateComponent();
        }

        private void LateUpdate()
        {
            if (!Initialized)
            {
                return;
            }
            LateUpdateComponent();
        }

        public void SetInputMode(InputMode mode)
        {
            InputMode = mode;
            if (InputMode == InputMode.Game)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }

        public void GameComplete()
        {
            StartCoroutine(LoadOutroScene());
        }

        public void GameOver()
        {
            AudioManager.StaticInstance.ChangeBackgroundMusic(4);
            SetInputMode(InputMode.UI);
            StartCoroutine(UIManager.DeathScreenUI.ShowDeathScreen(OnDeathScreenButtonPressed));
        }

        private IEnumerator LoadOutroScene()
        {
            SetInputMode(InputMode.UI);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(2);
        }

        private void OnDeathScreenButtonPressed()
        {
            StartCoroutine(LoadMainMenu());
        }

        private IEnumerator LoadMainMenu()
        {
            yield return UIManager.FadeScreen(1f);
            AudioManager.StaticInstance.ChangeBackgroundMusic(0);
            SceneManager.LoadScene(0);
        }
    }
}