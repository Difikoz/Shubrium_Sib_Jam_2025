using UnityEngine;

namespace WinterUniverse
{
    public class ResetActionFlags : StateMachineBehaviour
    {
        private Pawn _pawn;

        [SerializeField] private bool _toggleIsPerfomingAction = true;
        [SerializeField] private bool _isPerfomingActionState = false;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _pawn = animator.GetComponent<Pawn>();
            if (_toggleIsPerfomingAction)
            {
                if (_isPerfomingActionState)
                {
                    _pawn.GameplayComponent.AddGameplayTag("Is Perfoming Action");
                }
                else
                {
                    _pawn.GameplayComponent.RemoveGameplayTag("Is Perfoming Action");
                }
            }
        }
    }
}