using UnityEngine;

namespace WinterUniverse
{
    public class PawnAnimatorComponent : PawnComponent
    {
        public Animator Animator { get; private set; }
        public bool UsingRootMotion { get; private set; }

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            Animator = GetComponentInChildren<Animator>();
        }

        public void PlayAction(string name, float fadeTime = 0.1f, bool changeActionState = true, bool isPerfomingAction = true)
        {
            if (changeActionState)
            {
                if (isPerfomingAction)
                {
                    _pawn.GameplayComponent.AddGameplayTag("Is Perfoming Action");
                }
                else
                {
                    _pawn.GameplayComponent.RemoveGameplayTag("Is Perfoming Action");
                }
            }
            Animator.CrossFade(name, fadeTime);
        }

        public void SetFloat(string name, float value)
        {
            Animator.SetFloat(name, value);
        }

        public void SetBool(string name, bool value)
        {
            Animator.SetBool(name, value);
        }

        private void OnAnimatorMove()
        {
            if (Animator != null && UsingRootMotion)
            {

            }
        }
    }
}