using UnityEngine;

namespace WinterUniverse
{
    public class AnimatorEvents : MonoBehaviour
    {
        private Pawn _pawn;

        private void Awake()
        {
            _pawn = GetComponentInParent<Pawn>();
        }

        public void PerformAbilityCast()
        {
            _pawn.Combat.PerformAbilityCast();
        }
    }
}