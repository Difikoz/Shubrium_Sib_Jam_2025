using UnityEngine;

namespace WinterUniverse
{
    public class UIManager : BasicComponentHolder
    {
        public HealthUIController HealthUIController { get; private set; }
        public PlayerImplantsUI PlayerImplantsUI { get; private set; }

        public override void FillComponents()
        {
            HealthUIController = GetComponentInChildren<HealthUIController>();
            PlayerImplantsUI = GetComponentInChildren<PlayerImplantsUI>();
            _components.Add(HealthUIController);
            _components.Add(PlayerImplantsUI);
        }
    }
}