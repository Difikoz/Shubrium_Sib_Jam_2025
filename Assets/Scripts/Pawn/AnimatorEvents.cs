using FMODUnity;
using UnityEngine;

namespace WinterUniverse
{
    public class AnimatorEvents : MonoBehaviour
    {
        private Pawn _pawn;

        [SerializeField] private EventReference _attackRef;
        [SerializeField] private EventReference _footstepRef;

        private void Awake()
        {
            _pawn = GetComponentInParent<Pawn>();
        }

        public void PerformAbilityCast()
        {
            _pawn.Combat.PerformAbilityCast();
        }

        public void PlayAttackSound()
        {
            if(_attackRef.ToString() == string.Empty)
            {
                return;
            }
            RuntimeManager.PlayOneShotAttached(_attackRef, gameObject);
        }

        public void PlayFootstepSound()
        {
            if (_footstepRef.ToString() == string.Empty)
            {
                return;
            }
            RuntimeManager.PlayOneShotAttached(_footstepRef, gameObject);
        }
    }
}