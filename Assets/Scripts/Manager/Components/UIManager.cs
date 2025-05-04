using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class UIManager : BasicComponentHolder
    {
        [field: SerializeField] public Image FadeImage { get; private set; }
        public HealthUIController HealthUIController { get; private set; }
        public PlayerImplantsUI PlayerImplantsUI { get; private set; }
        public ImplantSelectionUI ImplantSelectionUI { get; private set; }
        public DialogueUI DialogueUI { get; private set; }
        public DashUI DashUI { get; private set; }
        public DeathScreenUI DeathScreenUI { get; private set; }

        public override void FillComponents()
        {
            HealthUIController = GetComponentInChildren<HealthUIController>();
            PlayerImplantsUI = GetComponentInChildren<PlayerImplantsUI>();
            ImplantSelectionUI = GetComponentInChildren<ImplantSelectionUI>();
            DialogueUI = GetComponentInChildren<DialogueUI>();
            DashUI = GetComponentInChildren<DashUI>();
            DeathScreenUI = GetComponentInChildren<DeathScreenUI>();
            _components.Add(HealthUIController);
            _components.Add(PlayerImplantsUI);
            _components.Add(ImplantSelectionUI);
            _components.Add(DialogueUI);
            _components.Add(DashUI);
            _components.Add(DeathScreenUI);
        }

        public IEnumerator FadeScreen(float value)
        {
            Color c = FadeImage.color;
            while (c.a != value)
            {
                c.a = Mathf.MoveTowards(c.a, value, 0.5f * Time.deltaTime);
                FadeImage.color = c;
                yield return null;
            }
        }
    }
}