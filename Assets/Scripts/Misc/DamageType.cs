using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class DamageType
    {
        [field: SerializeField, Range(0, 999999)] public int Damage { get; private set; }
        [field: SerializeField] public DamageTypeConfig Type { get; private set; }
    }
}