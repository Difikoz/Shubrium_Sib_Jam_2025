using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnHealthComponent : PawnComponent
    {
        public float Current { get; private set; }
        public float Max => _pawn.GameplayComponent.GetGameplayStat("Health").CurrentValue;

        public void ApplyDamages(List<DamageType> damageTypes, Pawn source)
        {
            foreach (DamageType damageType in damageTypes)
            {
                TakeDamage(damageType.Damage, damageType.Type, source);
            }
        }

        public void TakeDamage(float value, DamageTypeConfig type, Pawn source)
        {
            if (_pawn.GameplayComponent.HasGameplayStat("Is Dead") || value <= 0f)
            {
                return;
            }
            _pawn.PerformTrigger("On Taken Damage");
            Current = Mathf.Clamp(Current - value, 0f, Max);
            if(Current == 0f)
            {
                Die(source);
            }
            else
            {

            }
        }

        public void Die(Pawn source)
        {

        }
    }
}