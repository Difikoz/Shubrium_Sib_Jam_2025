using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class HeartUI : MonoBehaviour
    {
        [SerializeField] private Image _heartImage;
        
        [SerializeField] private Sprite _fullHeartSprite;
        [SerializeField] private Sprite _halfHeartSprite;
        [SerializeField] private Sprite _emptyHeartSprite;
        
        public enum HeartState
        {
            Empty,  // Пустое сердечко
            Half,   // Половина сердечка
            Full    // Полное сердечко
        }
        
        // Обновляет вид сердечка в зависимости от состояния
        public void SetState(HeartState state)
        {
            switch (state)
            {
                case HeartState.Empty:
                    _heartImage.sprite = _emptyHeartSprite;
                    break;
                case HeartState.Half:
                    _heartImage.sprite = _halfHeartSprite;
                    break;
                case HeartState.Full:
                    _heartImage.sprite = _fullHeartSprite;
                    break;
            }
        }
    }
} 