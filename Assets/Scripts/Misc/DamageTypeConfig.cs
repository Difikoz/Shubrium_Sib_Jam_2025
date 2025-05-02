using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Damage Type", menuName = "Winter Universe/Misc/New Damage Type")]
    public class DamageTypeConfig : BasicInfoConfig
    {
        [field: SerializeField] public GameplayStatConfig ResistanceStat { get; private set; }
        [field: SerializeField] public List<GameObject> HitVFX { get; private set; }
        [field: SerializeField] public List<AudioClip> HitClips { get; private set; }
    }
}