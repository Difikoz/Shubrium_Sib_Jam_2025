using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        public bool Initialized { get; private set; }
        public PlayerController Player { get; private set; }
        public ImplantManager ImplantManager { get; private set; }
        public UIManager UIManager { get; private set; }

        protected override void OnAwake()
        {
            StartCoroutine(Initialization());
        }

        public override void FillComponents()
        {
            Player = FindFirstObjectByType<PlayerController>();
            ImplantManager = FindFirstObjectByType<ImplantManager>();
            UIManager = FindFirstObjectByType<UIManager>();
            _components.Add(Player);
            _components.Add(ImplantManager);
            _components.Add(UIManager);
        }

        private IEnumerator Initialization()
        {
            yield return new WaitForSeconds(1f);
            InitializeComponent();
            yield return new WaitForSeconds(0.1f);
            ActivateComponent();
            yield return new WaitForSeconds(0.1f);
            EnableComponent();
            yield return new WaitForSeconds(0.1f);
            Initialized = true;
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
    }
}