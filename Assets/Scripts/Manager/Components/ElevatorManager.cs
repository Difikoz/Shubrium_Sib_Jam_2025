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
            CloseDoors();
            GameManager.StaticInstance.ImplantManager.ShowImplantSelection();
            StartCoroutine(WaitForImplantSelection());
        }

        private IEnumerator WaitForImplantSelection()
        {
            WaitForSeconds delay = new(0.1f);
            yield return delay;
            GameManager.StaticInstance.Player.DeactivateComponent();
            GameManager.StaticInstance.StageManager.DisableCurrentStage();
            yield return delay;
            GameManager.StaticInstance.StageManager.CurrentStage.TeleportPlayerToStartPoint();
            while (GameManager.StaticInstance.ImplantManager.IsSelectingImplant)
            {
                yield return delay;
            }
            GameManager.StaticInstance.StageManager.StartNextStage();
            GameManager.StaticInstance.Player.ActivateComponent();
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