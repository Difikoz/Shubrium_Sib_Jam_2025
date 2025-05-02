using UnityEngine;

namespace WinterUniverse
{
    public abstract class GameplayEffect : BasicInfoConfig
    {
        public abstract void OnApply(Pawn owner, Pawn source);
    }
}