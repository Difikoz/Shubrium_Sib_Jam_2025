using UnityEngine;

namespace WinterUniverse
{
    public abstract class AbilityCastTypeConfig : ScriptableObject
    {
        public abstract void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 direction /*, AbilityHitTypeConfig hitType*/);
    }
}