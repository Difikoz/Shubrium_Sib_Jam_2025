using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class HealthPickup : MonoBehaviour
    {
        [Header("Настройки")]
        [SerializeField] private int _healAmount = 10;
        [SerializeField] private GameObject _pickupEffect;
        [SerializeField] private AudioClip _pickupSound;

        [Header("Анимация")]
        [SerializeField] private float _bobHeight = 0.2f;
        [SerializeField] private float _bobSpeed = 2f;
        [SerializeField] private float _rotateSpeed = 90f;

        private Vector3 _startPosition;
        private Transform _visual;

        private void Awake()
        {
            // Если есть дочерний объект с визуалом, используем его для анимации
            if (transform.childCount > 0)
            {
                _visual = transform.GetChild(0);
            }
            else
            {
                _visual = transform;
            }

            _startPosition = _visual.localPosition;
        }

        private void Start()
        {
            // Сохраняем начальную позицию для анимации покачивания
            _startPosition = _visual.localPosition;
        }

        private void Update()
        {
            // Анимация покачивания
            if (_visual != null)
            {
                // Покачивание вверх-вниз
                _visual.localPosition = _startPosition + Vector3.up * Mathf.Sin(Time.time * _bobSpeed) * _bobHeight;

                // Вращение вокруг оси Y
                _visual.Rotate(0, _rotateSpeed * Time.deltaTime, 0);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                // Восстанавливаем здоровье
                HealPlayer(player);

                // Воспроизводим эффект
                PlayPickupEffect();

                // Деактивируем объект
                LeanPool.Despawn(gameObject);
            }
        }

        private void HealPlayer(PlayerController player)
        {
            // Восстанавливаем здоровье игрока
            Debug.Log($"[{GetType().Name}] Игрок подобрал сердечко: +{_healAmount} здоровья");

            // Используем метод Restore из PawnHealthComponent
            if (player.Health != null)
            {
                player.Health.Restore(_healAmount, player);
            }
        }

        private void PlayPickupEffect()
        {
            // Проигрываем звук, если он настроен
            if (_pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(_pickupSound, transform.position, 0.8f);
            }

            // Создаем визуальный эффект, если он настроен
            if (_pickupEffect != null)
            {
                LeanPool.Despawn(LeanPool.Spawn(_pickupEffect, transform.position, Quaternion.identity), 10f);
            }
        }
    }
}