using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class ElevatorManager : BasicComponent
    {
        private bool _isInElevator = false;

        [SerializeField] private GameObject _testDoors;

        public void OpenDoors()
        {
            _testDoors.SetActive(false);
        }

        public void CloseDoors()
        {
            _testDoors.SetActive(true);
        }

        private void OnPlayerEntered()
        {
            if (_isInElevator)
            {
                return;
            }
            _isInElevator = true;
            StartCoroutine(WaitForImplantSelection());
        }

        private IEnumerator WaitForImplantSelection()
        {
            WaitForSeconds delay = new(0.1f);
            CloseDoors();
            yield return delay;
            GameManager.StaticInstance.SetInputMode(InputMode.UI);
            yield return GameManager.StaticInstance.UIManager.FadeScreen(1f);
            GameManager.StaticInstance.ImplantManager.ShowImplantSelection();
            yield return delay;
            GameManager.StaticInstance.Player.DeactivateComponent();
            GameManager.StaticInstance.StageManager.DisableCurrentStage();
            while (GameManager.StaticInstance.ImplantManager.IsSelectingImplant)
            {
                yield return delay;
            }
            GameManager.StaticInstance.StageManager.StartNextStage();
            yield return delay;
            GameManager.StaticInstance.StageManager.CurrentStage.TeleportPlayerToStartPoint();
            yield return delay;
            GameManager.StaticInstance.Player.ActivateComponent();
            yield return delay;
            yield return GameManager.StaticInstance.UIManager.FadeScreen(0f);
            GameManager.StaticInstance.SetInputMode(InputMode.Game);
            _isInElevator = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                OnPlayerEntered();
            }
        }
    }
}