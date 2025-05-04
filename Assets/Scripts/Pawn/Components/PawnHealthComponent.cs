using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnHealthComponent : PawnComponent
    {
        public Action<int, int> OnValueChanged;

        public int Current { get; private set; }
        public int Max => Mathf.RoundToInt(_pawn.GameplayComponent.GetGameplayStat("Health").CurrentValue);

        public void ApplyDamages(List<DamageType> damageTypes, Pawn source)
        {
            foreach (DamageType damageType in damageTypes)
            {
                Reduce(damageType.Damage, damageType.Type, source);
            }
        }

        public void Reduce(int value, DamageTypeConfig type, Pawn source)
        {
            if (_pawn.GameplayComponent.HasGameplayStat("Is Dead") || value <= 0)
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
            if (_pawn.GameplayComponent.HasGameplayStat("Is Dead") || value <= 0)
            {
                return;
            }
            _pawn.PerformTrigger("On Health Restored", source);
            Current = Mathf.Clamp(Current + value, 0, Max);
            OnValueChanged?.Invoke(Current, Max);
        }

        public void Die(Pawn source)
        {
            if (_pawn.GameplayComponent.HasGameplayStat("Is Dead"))
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
    }
}