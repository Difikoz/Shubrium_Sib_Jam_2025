using UnityEngine;

namespace WinterUniverse
{
    public class PawnComponent : BasicComponent
    {
        protected Pawn _pawn;

        public override void InitializeComponent()
        {
            _pawn = GetComponent<Pawn>();
        }
    }
}