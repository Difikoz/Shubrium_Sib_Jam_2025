using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class DeathScreenUI : BasicComponent, ISubmitHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _button;

        private Action _onPressed;

        public IEnumerator ShowDeathScreen(Action onPressed)
        {
            _button.Select();
            _onPressed = onPressed;
            _canvasGroup.gameObject.SetActive(true);
            while (_canvasGroup.alpha != 1f)
            {
                _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, 1f, 2f * Time.deltaTime);
                yield return null;
            }
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (_canvasGroup.alpha != 1f)
            {
                return;
            }
            _onPressed?.Invoke();
        }
    }
}