using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnHealthComponent : PawnComponent
    {
        public Action<int, int> OnValueChanged;

        [field: SerializeField] public GameObject InvulnerableEffect { get; private set; }

        public int Current { get; private set; }
        public int Max { get; private set; }

        private Coroutine _invulnerableCoroutine;

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
            int lastMax = Max;
            Max = Mathf.RoundToInt(_pawn.GameplayComponent.GetGameplayStat("Health").CurrentValue);
            if (Max > lastMax)
            {
                Current += Max - lastMax;
            }
            OnValueChanged?.Invoke(Current, Max);
        }

        public void ApplyDamages(List<DamageType> damageTypes, Pawn source)
        {
            foreach (DamageType damageType in damageTypes)
            {
                Reduce(damageType.Damage, damageType.Type, source);
            }
        }

        public void Reduce(int value, DamageTypeConfig type, Pawn source)
        {
            if (_pawn.GameplayComponent.HasGameplayTag("Is Dead") || value <= 0 || _pawn.GameplayComponent.HasGameplayTag("Is Invulnerable"))
            {
                return;
            }
            _pawn.PerformTrigger("On Health Reduced", source);
            Current = Mathf.Clamp(Current - value, 0, Max);
            if (Current == 0f)
            {
                Die(source);
            }
            else
            {
                OnValueChanged?.Invoke(Current, Max);
            }
        }

        public void Restore(int value, Pawn source)
        {
            if (_pawn.GameplayComponent.HasGameplayTag("Is Dead") || value <= 0)
            {
                return;
            }
            _pawn.PerformTrigger("On Health Restored", source);
            Current = Mathf.Clamp(Current + value, 0, Max);
            OnValueChanged?.Invoke(Current, Max);
        }

        public void Die(Pawn source)
        {
            if (_pawn.GameplayComponent.HasGameplayTag("Is Dead"))
            {
                return;
            }
            _pawn.PerformTrigger("On Died", source);
            if (Current > 0f)
            {
                return;
            }
            Current = 0;
            OnValueChanged?.Invoke(Current, Max);
            _pawn.GameplayComponent.AddGameplayTag("Is Dead");
            _pawn.Animator.PlayAction("Death");
            StartCoroutine(_pawn.PerformDeath());
        }

        public void Revive(Pawn source)
        {
            _pawn.GameplayComponent.RemoveGameplayTag("Is Dead");
            _pawn.PerformTrigger("On Revived", source);
            Restore(Max, source);
        }

        public void AddInvulnerable(float duration = -1f)
        {
            RemoveInvulnerable();
            _invulnerableCoroutine = StartCoroutine(InvulnerableTimer(duration));
        }

        public void RemoveInvulnerable()
        {
            if (_invulnerableCoroutine != null)
            {
                StopCoroutine(_invulnerableCoroutine);
                StopInvulnerable();
                _invulnerableCoroutine = null;
            }
        }

        private IEnumerator InvulnerableTimer(float duration)
        {
            float currentTime = 0f;
            StartInvulnerable();
            while (true)
            {
                currentTime += Time.deltaTime;
                if (duration != -1f && currentTime >= duration)
                {
                    break;
                }
                yield return null;
            }
            StopInvulnerable();
            _invulnerableCoroutine = null;
        }

        private void StartInvulnerable()
        {
            _pawn.GameplayComponent.AddGameplayTag("Is Invulnerable");
            InvulnerableEffect.SetActive(true);
        }

        private void StopInvulnerable()
        {
            InvulnerableEffect.SetActive(false);
            _pawn.GameplayComponent.RemoveGameplayTag("Is Invulnerable");
        }
    }
}