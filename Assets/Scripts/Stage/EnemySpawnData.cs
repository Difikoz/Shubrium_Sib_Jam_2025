using System;
using UnityEngine;

namespace WinterUniverse
{
    [Serializable]
    public class EnemySpawnData
    {
        [field: SerializeField] public EnemyConfig Config { get; private set; }
        [field: SerializeField, Range(0, 10)] public int Count { get; private set; }
    }
}