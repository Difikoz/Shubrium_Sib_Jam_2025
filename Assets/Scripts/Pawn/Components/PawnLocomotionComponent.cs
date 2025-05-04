using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnLocomotionComponent : PawnComponent
    {
        // Единственное событие для обновления UI рывка
        public event Action<float> OnDashCooldownUpdate;

        public Vector3 MoveDirection;
        public Vector3 GroundVelocity { get; private set; }
        public Vector3 KnockbackVelocity { get; private set; }
        public Vector3 DashVelocity { get; private set; }
        [field: SerializeField, Range(10f, 1440f)] public float RotateSpeed { get; private set; }
        [field: SerializeField, Range(1f, 100f)] public float Mass { get; private set; }
        [field: SerializeField, Range(0.1f, 0.5f)] public float TimeToDash { get; private set; }

        private float _maxSpeed;
        private Coroutine _dashCoroutine;

        public Vector3 TotalVelocity => GroundVelocity * _maxSpeed + KnockbackVelocity + DashVelocity;

        public override void EnableComponent()
        {
            base.EnableComponent();
            _pawn.GameplayComponent.OnStatsChanged += OnStatsChanged;
        }

        public override void DisableComponent()
        {
            _pawn.GameplayComponent.OnStatsChanged -= OnStatsChanged;
            base.DisableComponent();
        }

        private void OnStatsChanged(Dictionary<string, GameplayStat> stats)
        {
            _maxSpeed = _pawn.GameplayComponent.GetGameplayStat("Move Speed").CurrentValue;
            _pawn.Animator.SetFloat("Move Speed", _maxSpeed / 4f);
        }

        public override void ActivateComponent()
        {
            base.ActivateComponent();
            _dashCoroutine = null;
            _pawn.Animator.SetBool("Is Dashing", false);
            _maxSpeed = _pawn.GameplayComponent.GetGameplayStat("Move Speed").CurrentValue;
            _pawn.Animator.SetFloat("Move Speed", _maxSpeed / 4f);
            // Рывок доступен сразу
            OnDashCooldownUpdate?.Invoke(1f);
        }

        public override void UpdateComponent()
        {
            if (_pawn.GameplayComponent.HasGameplayTag("Is Moving"))
            {
                if (MoveDirection == Vector3.zero || DashVelocity != Vector3.zero || _pawn.GameplayComponent.HasGameplayTag("Is Perfoming Action") || GameManager.StaticInstance.InputMode != InputMode.Game)
                {
                    _pawn.GameplayComponent.RemoveGameplayTag("Is Moving");
                    _pawn.Animator.SetBool("Is Moving", false);
                }
                else
                {
                    GroundVelocity = Vector3.MoveTowards(GroundVelocity, MoveDirection, 2f * Time.deltaTime);
                }
            }
            else
            {
                if (MoveDirection != Vector3.zero && DashVelocity == Vector3.zero && !_pawn.GameplayComponent.HasGameplayTag("Is Perfoming Action") && GameManager.StaticInstance.InputMode == InputMode.Game)
                {
                    _pawn.GameplayComponent.AddGameplayTag("Is Moving");
                    _pawn.Animator.SetBool("Is Moving", true);
                }
                else
                {
                    GroundVelocity = Vector3.MoveTowards(GroundVelocity, Vector3.zero, 4f * Time.deltaTime);
                    if (KnockbackVelocity != Vector3.zero)
                    {
                        KnockbackVelocity = Vector3.MoveTowards(KnockbackVelocity, Vector3.zero, Mass * Time.deltaTime);
                    }
                }
            }
        }

        public void AddKnockback(Vector3 direction, float force)
        {
            if (DashVelocity != Vector3.zero)
            {
                return;
            }
            KnockbackVelocity += direction.normalized * force;
        }

        public void PerformDash()
        {
            if (_dashCoroutine != null || _pawn.GameplayComponent.HasGameplayTag("Is Perfoming Action"))
            {
                return;
            }
            if (MoveDirection != Vector3.zero)
            {
                DashVelocity = MoveDirection.normalized * _pawn.GameplayComponent.GetGameplayStat("Dash Force").CurrentValue / TimeToDash;
            }
            else
            {
                DashVelocity = transform.forward * _pawn.GameplayComponent.GetGameplayStat("Dash Force").CurrentValue / TimeToDash;
            }
            KnockbackVelocity = Vector3.zero;
            GroundVelocity = Vector3.zero;
            _pawn.Animator.SetBool("Is Dashing", true);

            // Сообщаем UI, что рывок использован
            OnDashCooldownUpdate?.Invoke(0f);
            AudioManager.StaticInstance.PlaySoundAttached($"event:/player/player_dash", gameObject);
            _dashCoroutine = StartCoroutine(DashCoroutine());
        }

        private IEnumerator DashCoroutine()
        {
            yield return new WaitForSeconds(TimeToDash);
            DashVelocity = Vector3.zero;
            _pawn.Animator.SetBool("Is Dashing", false);

            // Получаем время кулдауна
            float dashCooldown = _pawn.GameplayComponent.GetGameplayStat("Dash Cooldown").CurrentValue;

            // Постепенно обновляем UI во время кулдауна
            float startTime = Time.time;
            float endTime = startTime + dashCooldown;

            while (Time.time < endTime)
            {
                float progress = (Time.time - startTime) / dashCooldown;
                OnDashCooldownUpdate?.Invoke(progress);
                yield return null; // Ждем следующий кадр
            }

            // Финальное обновление - рывок полностью готов
            OnDashCooldownUpdate?.Invoke(1f);

            _dashCoroutine = null;
        }
    }
}