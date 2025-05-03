using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        public bool Initialized { get; private set; }
        public PlayerController Player { get; private set; }
        public CameraManager CameraManager { get; private set; }
        public DialogueManager DialogueManager { get; private set; }
        public ElevatorManager ElevatorManager { get; private set; }
        public ImplantManager ImplantManager { get; private set; }
        public InputActionsManager InputActionsManager { get; private set; }
        public LayersManager LayersManager { get; private set; }
        public SpawnManager SpawnManager { get; private set; }
        public StageManager StageManager { get; private set; }
        public UIManager UIManager { get; private set; }
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
            yield return new WaitForSeconds(1f);
            //_components = new();
            InitializeComponent();
            //foreach (BasicComponent component in _components)
            //{
            //    Debug.LogError($"Initialize {component.gameObject.name}");
            //    component.InitializeComponent();
            //    Debug.LogError($"{component.gameObject.name} Initialized");
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
            Debug.Log("Game Completed");
            StartCoroutine(LeaveGame(2, 1f));
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            StartCoroutine(LeaveGame(0, 5f));
        }

        private IEnumerator LeaveGame(int scene, float delay)
        {
            SetInputMode(InputMode.UI);
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(scene);
        }
    }
}