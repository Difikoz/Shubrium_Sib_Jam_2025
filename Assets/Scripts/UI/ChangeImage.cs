using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Seedon
{
    public class ChangeImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [Header("Режим работы")]
        [SerializeField] private bool _changeSprite = false;  // Флаг для переключения режима работы
        
        [Header("Спрайты (если включен режим changeSprite)")]
        public Sprite spriteOn;
        public Sprite spriteOff;
        public Image image;
        
        [Header("Управление объектом")]
        [SerializeField] private GameObject _targetObject;  // Объект, который будем включать/выключать
        
        void Start()
        {
            if (_changeSprite && image != null)
            {
                image.sprite = spriteOff;
            }
            
            // Устанавливаем начальное состояние объекта
            if (_targetObject != null)
            {
                _targetObject.SetActive(false);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Включаем объект
            if (_targetObject != null)
            {
                _targetObject.SetActive(true);
            }
            
            // Если включен режим смены спрайта, меняем спрайт
            if (_changeSprite && image != null)
            {
                image.sprite = spriteOn;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // Выключаем объект
            if (_targetObject != null)
            {
                _targetObject.SetActive(false);
            }
            
            // Если включен режим смены спрайта, меняем спрайт обратно
            if (_changeSprite && image != null)
            {
                image.sprite = spriteOff;
            }
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            // Выключаем объект при уходе курсора
            if (_targetObject != null)
            {
                _targetObject.SetActive(false);
            }
            
            // Если включен режим смены спрайта, меняем спрайт обратно
            if (_changeSprite && image != null)
            {
                image.sprite = spriteOff;
            }
        }
    }
}