using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class ElevatorManager : BasicComponent
    {
        private bool _isInElevator = false;

        [SerializeField] private Animator _doorAnimator;

        public void OpenDoors()
        {
            _doorAnimator.SetBool("Switched", true);
        }

        public void CloseDoors()
        {
            _doorAnimator.SetBool("Switched", false);
        }

        private void OnPlayerEntered()
        {
            if (_isInElevator)
            {
                return;
            }
            _isInElevator = true;
            AudioManager.StaticInstance.ChangeBackgroundMusic(2);
            StartCoroutine(WaitForImplantSelection());
        }

        private IEnumerator WaitForImplantSelection()
        {
            WaitForSeconds delay = new(0.1f);
            CloseDoors();
            yield return new WaitForSeconds(1f);
            GameManager.StaticInstance.SetInputMode(InputMode.UI);
            if (GameManager.StaticInstance.StageManager.LastStage)
            {
                yield return GameManager.StaticInstance.UIManager.FadeScreen(1f);
                GameManager.StaticInstance.GameComplete();
            }
            else
            {
                yield return GameManager.StaticInstance.ImplantManager.ShowImplantSelection();
                while (GameManager.StaticInstance.ImplantManager.IsSelectingImplant)
                {
                    yield return delay;
                }
                yield return GameManager.StaticInstance.UIManager.FadeScreen(1f);
                GameManager.StaticInstance.Player.DeactivateComponent();
                yield return delay;
                GameManager.StaticInstance.StageManager.DisableCurrentStage();
                yield return delay;
                GameManager.StaticInstance.StageManager.StartNextStage();
                yield return delay;
                GameManager.StaticInstance.StageManager.CurrentStage.TeleportPlayerToStartPoint();
                yield return delay;
                GameManager.StaticInstance.Player.ActivateComponent();
                yield return delay;
                yield return GameManager.StaticInstance.UIManager.FadeScreen(0f);
                GameManager.StaticInstance.DialogueManager.ShowDialogue(GameManager.StaticInstance.StageManager.CurrentStage.DialogueBeforeBattle);
                while (GameManager.StaticInstance.DialogueManager.IsShowingDialogue)
                {
                    yield return delay;
                }
                AudioManager.StaticInstance.ChangeBackgroundMusic(1);
                GameManager.StaticInstance.SetInputMode(InputMode.Game);
                _isInElevator = false;
            }
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